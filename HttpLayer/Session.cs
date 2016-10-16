using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HttpLayer
{
    public interface ISession
    {
        void BeforeRequest(HttpWebRequest request);
        void AfterResponse(HttpWebResponse response);
    }

    public class CookieRetainingSession : ISession
    {
        private CookieContainer _cookies = new CookieContainer();
        private CookieCollection _lastReceivedCookies;

        public void BeforeRequest(HttpWebRequest request)
        {
            if (request.CookieContainer != null)
                throw new InvalidOperationException($"Cookies exist on request; they must be added/maintained by {GetType().FullName}");

            request.CookieContainer = _cookies;
        }

        public void AfterResponse(HttpWebResponse response)
        {
            _lastReceivedCookies = response.Cookies;
        }

        public IReadOnlyCollection<Cookie> Cookies
        {
            get
            {
                if (_lastReceivedCookies == null)
                    return new Cookie[0];

                return _lastReceivedCookies.Cast<Cookie>().ToArray();
            }
        }
    }
}
