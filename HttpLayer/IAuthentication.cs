using System.Net;

namespace HttpLayer
{
    public interface IAuthentication
    {
        void BeforeRequest(HttpWebRequest request);
        void AfterResponse(HttpWebResponse response);
    }
}
