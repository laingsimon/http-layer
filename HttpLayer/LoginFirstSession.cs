using System;
using System.Net;

namespace HttpLayer
{
    public class LoginFirstSession : ISession
    {
        private readonly Application _application;
        private readonly HttpRequest _loginRequest;
        private readonly CookieRetainingSession _underlyingSession;
        private bool _loggedIn = false;

        public LoginFirstSession(HttpRequest loginRequest, Application application)
        {
            _loginRequest = loginRequest;
            _application = application;
            _underlyingSession = new CookieRetainingSession();

            _loginRequest.Session = new _ProxyingSession(_loginRequest.Session, _underlyingSession);
        }

        public void AfterResponse(HttpWebResponse response)
        {
            _underlyingSession.AfterResponse(response);
        }

        public void BeforeRequest(HttpWebRequest request)
        {
            if (!_loggedIn)
                _Login(request);

            _underlyingSession.BeforeRequest(request);
        }

        private void _Login(HttpWebRequest request)
        {
            var requester = new HttpRequester();
            requester.MakeRequest(_loginRequest, _application).Wait();
            _loggedIn = true;
        }

        private class _ProxyingSession : ISession
        {
            private readonly ISession[] _sessions;

            public _ProxyingSession(params ISession[] sessions)
            {
                _sessions = sessions;
            }

            public void AfterResponse(HttpWebResponse response)
            {
                foreach (var session in _sessions)
                    session.AfterResponse(response);
            }

            public void BeforeRequest(HttpWebRequest request)
            {
                foreach (var session in _sessions)
                    session.BeforeRequest(request);
            }
        }
    }
}
