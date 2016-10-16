using System;
using System.IO;
using System.Net;

namespace HttpLayer
{
    internal class NullRequestData : IRequestData
    {
        public void PrepareRequest(HttpWebRequest request)
        { }

        public void WriteToRequestStream(Stream requestStream)
        { }
    }
}
