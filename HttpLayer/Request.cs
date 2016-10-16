using System;
using System.Collections.Specialized;
using System.Net.Http;

namespace HttpLayer
{
    public class Request
    {
        private readonly Uri _relativeUri;

        public Request(Uri relativeUri)
        {
            _relativeUri = relativeUri;
        }

        public Request(string relativeUri)
        {
            _relativeUri = new Uri(relativeUri, UriKind.Relative);
        }

        public HttpMethod Method { get; set; } = HttpMethod.Get;
        public Uri RelativeUri => _relativeUri;
        public ISession Session { get; set; } = new NullSession();
        public NameValueCollection Headers { get; set; } = new NameValueCollection();
        public IRequestData Body { get; set; } = new NullRequestData();
    }
}
