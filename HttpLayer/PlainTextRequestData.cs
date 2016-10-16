using System;
using System.IO;
using System.Net;

namespace HttpLayer
{
    public class PlainTextRequestData : IRequestData
    {
        private readonly string _body;
        private readonly string _contentType;

        public PlainTextRequestData(string body, string contentType)
        {
            _body = body;
            _contentType = contentType;
        }

        public void PrepareRequest(HttpWebRequest request)
        {
            request.ContentType = _contentType;
            request.ContentLength = _body.Length;
        }

        public void WriteToRequestStream(Stream requestStream)
        {
            using (var writer = new StreamWriter(requestStream))
                writer.Write(_body);
        }
    }
}
