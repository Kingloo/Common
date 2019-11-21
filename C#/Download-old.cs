using System;
using System.IO;
using System.IO.Pipelines;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace .Common
{
    public enum DownloadResult
    {
        None,
        Success,
        Failure,
        Canceled,
        FileAlreadyExists,
        InitiationFailed,
        Interrupted
    }

    public class HeaderException : Exception
    {
        public string UnaddableHeader { get; } = string.Empty;

        public HeaderException()
            : this(string.Empty, null)
        { }

        public HeaderException(string header)
            : this(header, null)
        { }

        public HeaderException(string header, Exception innerException)
            : base(header, innerException)
        {
            UnaddableHeader = header;
        }
    }

    public class DownloadProgress
    {
        public Int64 BytesRead { get; } = 0L;
        public Int64 TotalBytesReceived { get; } = 0L;
        public Int64? ContentLength { get; } = null;
        public Uri Uri { get; } = null;
        public string FilePath { get; } = string.Empty;

        public DownloadProgress(Uri uri, string filePath, Int64 bytesRead, Int64 totalBytesReceived)
            : this(uri, filePath, bytesRead, totalBytesReceived, null)
        { }

        public DownloadProgress(Uri uri, string filePath, Int64 bytesRead, Int64 totalBytesReceived, Int64? contentLength)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            FilePath = filePath;
            BytesRead = bytesRead;
            TotalBytesReceived = totalBytesReceived;
            ContentLength = contentLength;
        }
    }

    public class Download
    {
        private const string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x66; rv:64.0) Gecko/20100101 Firefox/69.0";

        private Uri uri = null;
        private string path = string.Empty;

        public Download(Uri uri, string path)
        {
            this.uri = uri ?? throw new ArgumentNullException(nameof(uri));
            this.path = path ?? throw new ArgumentNullException(nameof(path));
        }

        private static HttpClient EnsureClientCreated()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                MaxAutomaticRedirections = 3,
                SslProtocols = SslProtocols.Tls12
            };

            HttpClient client = new HttpClient(handler, true)
            {
                Timeout = TimeSpan.FromSeconds(5d)
            };

            if (!client.DefaultRequestHeaders.UserAgent.TryParseAdd(userAgent))
            {
                throw new HeaderException(userAgent);
            }

            return client;
        }

        public Task<DownloadResult> ToFileAsync() => ToFileAsync(null, CancellationToken.None);

        public Task<DownloadResult> ToFileAsync(IProgress<DownloadProgress> progress)
        {
            if (progress is null) { throw new ArgumentNullException(nameof(progress)); }

            return ToFileAsync(progress, CancellationToken.None);
        }

        public Task<DownloadResult> ToFileAsync(CancellationToken token) => ToFileAsync(null, token);

        public async Task<DownloadResult> ToFileAsync(IProgress<DownloadProgress> progress, CancellationToken token)
        {
            if (File.Exists(path))
            {
                return DownloadResult.FileAlreadyExists;
            }

            using (HttpClient client = EnsureClientCreated())
            {
                (Stream download, Int64? contentLength) = await SetupStreamAsync(client, uri).ConfigureAwait(false);

                if (download is null)
                {
                    return DownloadResult.InitiationFailed;
                }

                Pipe pipe = new Pipe();
                
                Task<DownloadResult> downloadFromStream = WriteFromDownloadStreamToPipeAsync(download, pipe.Writer, token);
                Task writeToDisk = ReadFromPipeAndWriteToDiskAsync(path, pipe.Reader, progress, contentLength, token);
                
                await Task.WhenAll(downloadFromStream, writeToDisk).ConfigureAwait(false);

                return downloadFromStream.IsCompleted ? downloadFromStream.Result : DownloadResult.Failure;
            }
        }

        private static async Task<(Stream, Int64?)> SetupStreamAsync(HttpClient client, Uri uri)
        {
            HttpRequestMessage request = null;
            HttpResponseMessage response = null;

            try
            {
                request = new HttpRequestMessage(HttpMethod.Get, uri);
                response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                    return (stream, response.Content.Headers.ContentLength);
                }
                else
                {
                    return (null, null);
                }
            }
            catch (HttpRequestException)
            {
                return (null, null);
            }
            catch (TaskCanceledException)
            {
                return (null, null);
            }
            finally
            {
                request?.Dispose();
                
                // don't dispose response here, otherwise the content stream will close as well
            }
        }

        private static async Task<DownloadResult> WriteFromDownloadStreamToPipeAsync(Stream stream, PipeWriter writer, CancellationToken token)
        {
            int BUFSIZE = 1024 * 50;
            int bytesRead = 0;

            try
            {
                while (true)
                {
                    Memory<byte> memory = writer.GetMemory(BUFSIZE);

                    bytesRead = await stream.ReadAsync(memory, token).ConfigureAwait(false);

                    if (bytesRead < 1)
                    {
                        break;
                    }

                    writer.Advance(bytesRead);

                    FlushResult flushResult = await writer.FlushAsync().ConfigureAwait(false);

                    if (flushResult.IsCompleted)
                    {
                        break;
                    }
                }
                
                return DownloadResult.Success;
            }
            catch (HttpRequestException)
            {
                return DownloadResult.Interrupted;
            }
            finally
            {
                writer.Complete();
            }
        }

        private async Task ReadFromPipeAndWriteToDiskAsync(string path, PipeReader reader, IProgress<DownloadProgress> progress, Int64? contentLength, CancellationToken token)
        {
            FileStream fsAsync = null;

            Int64 previousBytesReceived = 0;
            Int64 totalBytesReceived = 0;

            Int64 threshold = 1024 * 100;

            try
            {
                fsAsync = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous);

                while (true)
                {
                    ReadResult readResult = await reader.ReadAsync(token).ConfigureAwait(false);

                    foreach (ReadOnlyMemory<byte> segment in readResult.Buffer)
                    {
                        await fsAsync.WriteAsync(segment, token).ConfigureAwait(false);

                        totalBytesReceived += segment.Length;
                    }

                    reader.AdvanceTo(readResult.Buffer.End);

                    if ((totalBytesReceived - previousBytesReceived) > threshold)
                    {
                        if (progress != null)
                        {
                            var downloadProgress = new DownloadProgress(uri, path, readResult.Buffer.Length, totalBytesReceived, contentLength);

                            progress.Report(downloadProgress);
                        }
                        
                        previousBytesReceived = totalBytesReceived;
                    }
                    
                    if (readResult.IsCompleted)
                    {
                        break;
                    }
                }

                await fsAsync.FlushAsync().ConfigureAwait(false);
            }
            catch (FileNotFoundException)
            {
                return;
            }
            catch (IOException)
            {
                return;
            }
            finally
            {
                if (fsAsync != null)
                {
                    fsAsync.Dispose();
                }
                
                reader.Complete();
            }
        }
    }
}