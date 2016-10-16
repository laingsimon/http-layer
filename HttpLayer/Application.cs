using System;

namespace HttpLayer
{
    public class Application
    {
        private readonly Uri _baseUri;

        public Application(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetFullUri(HttpRequest request)
        {
            return new Uri(_baseUri, request.RelativeUri);
        }
    }
}
