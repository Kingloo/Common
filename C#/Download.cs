using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
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
        InternetError
    }

    public class HeaderException : Exception
    {
        public string UnaddableHeader { get; } = string.Empty;

        public HeaderException(string header)
            : base($"unaddable header: {header}")
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
        private const string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x66; rv:64.0) Gecko/20100101 Firefox/67.0";
        
        private static HttpClient client = null;

        private CancellationTokenSource cts = null;

        private static void InitClient()
        {
            if (client != null) { return; }

            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                MaxAutomaticRedirections = 3
            };

            client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(6d)
            };

            if (!client.DefaultRequestHeaders.UserAgent.TryParseAdd(userAgent))
            {
                throw new HeaderException(userAgent);
            }
        }

        #region Properties
        public Uri Uri { get; } = null;
        public FileInfo File { get; } = null;
        #endregion

        public Download(Uri uri, FileInfo file)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            File = file ?? throw new ArgumentNullException(nameof(file));
        }

        public Task<DownloadResult> ToFileAsync()
            => ToFileAsync(null);

        public async Task<DownloadResult> ToFileAsync(IProgress<DownloadProgress> progress)
        {
            if (File.Exists) { return DownloadResult.FileAlreadyExists; }

            InitClient();

            cts = new CancellationTokenSource();

            int bytesRead = 0;
            Int64 totalBytesReceived = 0L;
            Int64 prevTotalBytesReceived = 0L;
            Int64 reportingThreshold = 1024L * 333L; // 333 KiB

            byte[] buffer = new byte[1024 * 1024]; // 1 MiB - but bytesRead below is only ever 16384 bytes

            HttpRequestMessage request = null;
            HttpResponseMessage response = null;

            Stream receive = null;
            Stream save = null;

            try
            {
                request = new HttpRequestMessage(HttpMethod.Get, Uri);

                var httpOption = HttpCompletionOption.ResponseHeadersRead;

                response = await client.SendAsync(request, httpOption, cts.Token).ConfigureAwait(false);

                receive = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                save = new FileStream(
                    File.FullName,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    1024 * 1024 * 15, // 15 MiB
                    FileOptions.Asynchronous);

                Int64? contentLength = response.Content.Headers.ContentLength;

                while ((bytesRead = await receive.ReadAsync(buffer, 0, buffer.Length, cts.Token).ConfigureAwait(false)) > 0)
                {
                    totalBytesReceived += bytesRead;

                    if ((totalBytesReceived - prevTotalBytesReceived) > reportingThreshold)
                    {
                        var dlProgress = new DownloadProgress(
                            request.RequestUri,
                            File.FullName,
                            bytesRead,
                            totalBytesReceived,
                            contentLength);

                        progress.Report(dlProgress);

                        prevTotalBytesReceived = totalBytesReceived;
                    }

                    await save.WriteAsync(buffer, 0, bytesRead).ConfigureAwait(false);
                }

                await save.FlushAsync().ConfigureAwait(false);
            }
            catch (HttpRequestException)
            {
                return DownloadResult.InternetError;
            }
            catch (IOException)
            {
                return DownloadResult.Failure;
            }
            catch (TaskCanceledException)
            {
                return DownloadResult.Canceled;
            }
            finally
            {
                request?.Dispose();
                response?.Dispose();
                receive?.Dispose();
                save?.Dispose();
                cts?.Dispose();

                cts = null;
            }

            return DownloadResult.Success;
        }

        public void Cancel()
        {
            cts?.Cancel();
        }
    }
}
