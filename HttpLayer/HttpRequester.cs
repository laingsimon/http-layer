using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpLayer
{
    public class HttpRequester
    {
        private readonly IResponseDataFactory _responseDataFactory;

        public HttpRequester(IResponseDataFactory responseDataFactory = null)
        {
            _responseDataFactory = responseDataFactory ?? new ResponseDataFactory();
        }

        public async Task<Response> MakeRequest(Request request, Application application)
        {
            var fullUrl = application.GetFullUri(request);

            var httpRequest = (HttpWebRequest)WebRequest.Create(fullUrl);
            httpRequest.Method = request.Method.ToString();
            httpRequest.AllowAutoRedirect = false;

            if (request.Method == HttpMethod.Put || request.Method == HttpMethod.Post)
                request.Body.PrepareRequest(httpRequest);

            request.Session.BeforeRequest(httpRequest);

            foreach (var header in request.Headers.AllKeys)
                httpRequest.Headers.Add(header, request.Headers[header]);

            if (request.Method == HttpMethod.Put || request.Method == HttpMethod.Post)
            {
                using (var requestStream = await httpRequest.GetRequestStreamAsync())
                    request.Body.WriteToRequestStream(requestStream);
            }

            var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();

            request.Session.AfterResponse(httpResponse);

            return new Response
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
