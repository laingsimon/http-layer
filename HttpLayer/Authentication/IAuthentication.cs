using System.Net;

namespace HttpLayer.Authentication
{
    public interface IAuthentication
    {
        void BeforeRequest(HttpWebRequest request);
        void AfterResponse(HttpWebResponse response);
    }
}
