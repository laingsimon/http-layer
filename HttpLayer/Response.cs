using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;

namespace HttpLayer
{
    public class Response
    {
        public NameValueCollection Headers { get; internal set; }
        public IResponseData Body { get; internal set; }

        public HttpStatusCode StatusCode { get; internal set; }

        public IReadOnlyCollection<Cookie> Cookies { get; internal set; }

        public bool IsRedirection
        {
            get
            {
                var redirectionCodes = new[]
                {
                    HttpStatusCode.Created,
                    HttpStatusCode.Found,
                    HttpStatusCode.Gone,
                    HttpStatusCode.Redirect,
                    HttpStatusCode.TemporaryRedirect,
                    HttpStatusCode.SeeOther
                };

                return redirectionCodes.Contains(StatusCode);
            }
        }

        public string ContentType
        {
            get { return Headers["Content-Type"]; }
        }

        public Uri Location
        {
            get
            {
                var location = Headers["Location"];
                if (string.IsNullOrEmpty(location))
                    return null;

                return new Uri(location, UriKind.Absolute);
            }
        }

        public long? ContentLength
        {
            get
            {
                var contentLength = Headers["Content-Length"];
                long length;

                if (long.TryParse(contentLength, out length))
                    return length;

                return default(long?);
            }
        }
    }
}
