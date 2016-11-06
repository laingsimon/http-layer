using System;
using System.Collections.Specialized;
using System.Net.Http;
using HttpLayer.Request;
using HttpLayer.Authentication;

namespace HttpLayer
{
    public class HttpRequest
    {
        private readonly Uri _relativeUri;

        public HttpRequest(Uri relativeUri)
        {
            if (relativeUri == null)
                throw new ArgumentNullException(nameof(relativeUri));

            _relativeUri = relativeUri;
        }

        public HttpRequest(string relativeUri)
        {
            if (relativeUri == null) //empty string should be allowed, incase the request is for the root of the site
                throw new ArgumentNullException(nameof(relativeUri));

            _relativeUri = new Uri(relativeUri, UriKind.Relative);
        }

        public HttpMethod Method { get; set; } = HttpMethod.Get;
        public Uri RelativeUri => _relativeUri;
        public ISession Session { get; set; } = new NullSession();
        public NameValueCollection Headers { get; set; } = new NameValueCollection();
        public IRequestData Body { get; set; } = new NullRequestData();
        public IAuthentication Authentication { get; set; } = new NullAuthentication();
    }
}
