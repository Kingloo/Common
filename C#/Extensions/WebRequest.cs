using System;
using System.Net;
using System.Threading.Tasks;

namespace .Extensions
{
    public static class WebRequestExtensions
    {
        public static WebResponse GetResponseExt(this WebRequest request)
        {
            if (request is null) { throw new ArgumentNullException(nameof(request)); }

            WebResponse webResp = null;

            try
            {
                webResp = request.GetResponse();
            }
            catch (WebException ex)
            {
                webResp = ex?.Response;
            }

            return webResp;
        }

        public static async Task<WebResponse> GetResponseAsyncExt(this WebRequest request)
        {
            if (request is null) { throw new ArgumentNullException(nameof(request)); }

            WebResponse webResp = null;

            try
            {
                webResp = await request.GetResponseAsync().ConfigureAwait(false);
            }
            catch (WebException ex)
            {
                webResp = ex?.Response;
            }

            return webResp;
        }
    }
}
