using System.IO;
using System.Net;

namespace HttpLayer.Request
{
    internal class NullRequestData : IRequestData
    {
        public void PrepareRequest(HttpWebRequest request)
        { }

        public void WriteToRequestStream(Stream requestStream)
        { }
    }
}
