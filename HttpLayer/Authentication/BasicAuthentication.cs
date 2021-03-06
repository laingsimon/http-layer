﻿using System;
using System.Net;
using System.Text;

namespace HttpLayer.Authentication
{
    public class BasicAuthentication : IAuthentication
    {
        private readonly string _password;
        private readonly string _username;

        public BasicAuthentication(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            _username = username;
            _password = password ?? string.Empty;
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
