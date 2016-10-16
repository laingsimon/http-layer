using System;
using System.Net;

namespace HttpLayer
{
    internal class NullSession : ISession
    {
        public void AfterResponse(HttpWebResponse response)
        { }

        public void BeforeRequest(HttpWebRequest request)
        { }
    }
}
