using System.Net;

namespace HttpLayer.Authentication
{
    internal class NullAuthentication : IAuthentication
    {
        public void AfterResponse(HttpWebResponse response)
        { }

        public void BeforeRequest(HttpWebRequest request)
        { }
    }
}
