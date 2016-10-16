using System;
using System.Net;
using System.Text;

namespace HttpLayer
{
    public class BasicAuthentication : IAuthentication
    {
        private readonly string _password;
        private readonly string _username;

        public BasicAuthentication(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public void AfterResponse(HttpWebResponse response)
        { }

        public void BeforeRequest(HttpWebRequest request)
        {
            var data = $"{_username}:{_password}";
            var bas64Encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(data));

            request.Headers.Add("Authentication", $"Basic {data}");
        }
    }
}
