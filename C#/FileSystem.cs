using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace 
{
    public static class FileSystem
    {
        public static Task<bool> TryGetLinesAsync(FileInfo file, out string[] lines)
            => GetLinesAsync(file, FileMode.Open, out lines);

        public static Task<bool> TryGetLinesAsync(FileInfo file, FileMode mode, out string[] lines)
        {
            if (file is null) { throw new ArgumentNullException(nameof(file)); }

            return GetLinesAsyncInternal(file, mode, out lines);
        }

        private static async Task<bool> TryGetLinesAsyncInternal(FileInfo file, FileMode mode, out string[] lines)
        {
            var loaded = new List<string>();

            FileStream fsAsync = null;

            try
            {
                fsAsync = new FileStream(
                    file.FullName,
                    mode,
                    FileAccess.Read,
                    FileShare.None,
                    4096,
                    FileOptions.Asynchronous | FileOptions.SequentialScan);

                using (StreamReader sr = new StreamReader(fsAsync))
                {
                    fsAsync = null;

                    string line = string.Empty;

                    while ((line = await sr.ReadLineAsync().ConfigureAwait(false)) != null)
                    {
                        loaded.Add(line);
                    }
                }
            }
            catch (IOException)
            {
                lines = Array.Empty<string>();
                return false;
            }
            finally
            {
                fsAsync?.Dispose();
            }
            
            lines = loaded.ToArray();
            return true;
        }


        public static Task<bool> WriteLinesAsync(FileInfo file, IEnumerable<string> lines)
            => WriteLinesAsync(file, lines, FileMode.OpenOrCreate);

        public static Task<bool> WriteLinesAsync(FileInfo file, IEnumerable<string> lines, FileMode mode)
        {
            if (file is null) { throw new ArgumentNullException(nameof(file)); }
            if (!lines.Any()) { return Task.CompletedTask; }

            return WriteLinesAsyncInternal(file, lines, mode);
        }

        private static async Task<bool> WriteLinesAsyncInternal(FileInfo file, IEnumerable<string> lines, FileMode mode)
        {
            FileStream fsAsync = null;

            try
            {
                fsAsync = new FileStream(
                    file.FullName,
                    mode,
                    FileAccess.Write,
                    FileShare.None,
                    4096,
                    FileOptions.Asynchronous);

                using (StreamWriter sw = new StreamWriter(fsAsync))
                {
                    fsAsync = null;

                    foreach (string line in lines)
                    {
                        await sw.WriteLineAsync(line).ConfigureAwait(false);
                    }

                    await sw.FlushAsync().ConfigureAwait(false);
                }
            }
            catch (IOException)
            {
                return false;
            }
            finally
            {
                fsAsync?.Close();
            }

            return true;
        }
    }
}
