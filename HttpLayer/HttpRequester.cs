using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HttpLayer.Response;
using HttpLayer.Log;

namespace HttpLayer
{
    public class HttpRequester
    {
        private readonly IResponseDataFactory _responseDataFactory;
        private readonly ILog _log;

        public HttpRequester(IResponseDataFactory responseDataFactory = null, ILog log = null)
        {
            _responseDataFactory = responseDataFactory ?? new ResponseDataFactory();
            _log = log ?? new NullLog();
        }

        public async Task<HttpResponse> MakeRequest(HttpRequest request, Application application)
        {
            var fullUri = application.GetFullUri(request);

            var httpRequest = (HttpWebRequest)WebRequest.Create(fullUri);
            httpRequest.Method = request.Method.ToString();
            httpRequest.AllowAutoRedirect = false;

            if (request.Method == HttpMethod.Put || request.Method == HttpMethod.Post)
                request.Body.PrepareRequest(httpRequest);

            request.Authentication.BeforeRequest(httpRequest);
            request.Session.BeforeRequest(httpRequest);

            foreach (var header in request.Headers.AllKeys)
                httpRequest.Headers.Add(header, request.Headers[header]);

            _log.WriteInformation($"Making {request.Method} request to {fullUri}", request);
            if (request.Method == HttpMethod.Put || request.Method == HttpMethod.Post)
            {
                using (var requestStream = await httpRequest.GetRequestStreamAsync())
                {
                    _log.WriteInformation($"Writing {request.Body.GetType().Name} to request {fullUri}", request);
                    request.Body.WriteToRequestStream(requestStream);
                }
            }

            try
            {
                var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();
                return _HandleResponse(request, httpResponse, fullUri);
            }
            catch (WebException exc)
            {
                _log.WriteWarning($"Error getting response from {fullUri}", request, exc);

                var errorHttpResponse = (HttpWebResponse)exc.Response;
                return _HandleResponse(request, errorHttpResponse, fullUri);
            }
        }

        private HttpResponse _HandleResponse(HttpRequest request, HttpWebResponse httpResponse, System.Uri fullUri)
        {
            _log.WriteInformation($"Got response from {fullUri} ({httpResponse.StatusCode}: {httpResponse.ContentType})", request);

            request.Session.AfterResponse(httpResponse);
            request.Authentication.AfterResponse(httpResponse);

            return new HttpResponse
            {
                StatusCode = httpResponse.StatusCode,
                Headers = httpResponse.Headers,
                Body = _GetBodyFromResponse(httpResponse),
                Cookies = httpResponse.Cookies.Cast<Cookie>().ToArray()
            };
        }

        private IResponseData _GetBodyFromResponse(HttpWebResponse httpResponse)
        {
            var charSetMatch = Regex.Match(httpResponse.ContentType, @"; charset=(?<charset>.+)\s*");
            var charset = charSetMatch.Success ? charSetMatch.Groups["charset"].Value : null;
            var stream = httpResponse.GetResponseStream();
            var reader = string.IsNullOrEmpty(charset) || _GetEncoding(charset) == null
                ? new StreamReader(stream)
                : new StreamReader(stream, _GetEncoding(charset));

            using (reader)
                return _responseDataFactory.GetResponseData(reader, httpResponse.ContentType);
        }

        private Encoding _GetEncoding(string charset)
        {
            if (charset == "utf-8")
                return Encoding.UTF8;

            return Encoding.GetEncoding(charset);
        }
    }
}
