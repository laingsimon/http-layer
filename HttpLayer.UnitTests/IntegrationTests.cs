using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HttpLayer.UnitTests
{
    [TestFixture]
    public class IntegrationTests
    {
        [TestCase("http://www.google.co.uk", "/", HttpStatusCode.OK)]
        public async Task ShouldReturnOk(string absolute, string relative, HttpStatusCode expectedStatus)
        {
            var application = new Application(new Uri(absolute, UriKind.Absolute));
            var request = new Request(relative)
            {
                Session = new CookieRetainingSession()
            };
            var requester = new HttpRequester();

            var response = await requester.MakeRequest(request, application);

            Assert.That(response.StatusCode, Is.EqualTo(expectedStatus));
        }

        [TestCase("http://www.google.co.uk", "/", HttpStatusCode.OK)]
        public async Task ShouldReturnCookies(string absolute, string relative, HttpStatusCode expectedStatus)
        {
            var application = new Application(new Uri(absolute, UriKind.Absolute));
            var session = new CookieRetainingSession();
            var request = new Request(relative)
            {
                Session = session
            };
            var requester = new HttpRequester();

            var response = await requester.MakeRequest(request, application);

            Assert.That(session.Cookies, Is.Not.Empty);
            Assert.That(response.Cookies, Is.Not.Empty);
        }

        [TestCase("http://www.google.co.uk", "/", typeof(HtmlResponseData))]
        [TestCase("https://data.bathhacked.org", "/resource/bdxg-pucd.json", typeof(JsonResponseData))]
        public async Task ShouldReturnCorrectContent(string absolute, string relative, Type typeOfResponse)
        {
            var application = new Application(new Uri(absolute, UriKind.Absolute));
            var request = new Request(relative);
            var requester = new HttpRequester();

            var response = await requester.MakeRequest(request, application);

            Assert.That(response.Body, Is.TypeOf(typeOfResponse));
        }
    }
}
