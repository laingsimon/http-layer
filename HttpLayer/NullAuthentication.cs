using System;
using System.Net;

namespace HttpLayer
{
    internal class NullAuthentication : IAuthentication
    {
        public void AfterResponse(HttpWebResponse response)
        { }

        public void BeforeRequest(HttpWebRequest request)
        { }
    }
}
