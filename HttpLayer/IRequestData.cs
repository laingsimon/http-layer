using System.IO;
using System.Net;

namespace HttpLayer
{
    public interface IRequestData
    {
        void PrepareRequest(HttpWebRequest request);
        void WriteToRequestStream(Stream requestStream);
    }
}
