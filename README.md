# HttpLayer

A series of abstractions over the .net HTTP types; facilitating simple HTTP level testing and automation

# Concepts

## Application
This embodies the base url of the application, and can be extended to contain other application-specific rules for when a request is made.

## Session
This optional type embodies a session state that can persist between sessions. Commonly this is achieved by logging in and presenting the token (that was provided at login time) for all subsequent requests (normally achieved with cookies).

## Request
This embodies all the properties of a request, i.e.
* Its relative url under the application
* Any headers to send with the request
* The HTTP method
* The body and its content type (for PUT and POST requests)

## Response
This embodies all the properties of a response, i.e.
* The response status code (successful or erroneous)
* The body and its content type (deserialised into a concrete type where possible, to aid use)
* The response headers

# Content types
Natively the library can handle the following body (request or response) content types

## Request
* Form data (`FormRequestData`)
* Json (`JsonRequestData`)
* Plain text (`PlainTextRequestData`)
* Xml (`XmlRequestData`)

## Response
* Xml (`XmlResponseData`)*
* Html (`HtmlResponseData`)*
* Json (`JsonResponseData`)
* Plain text (`PlainTextResponseData`)

`*` Thes response types support creating a `XPathNavigator` which can help select Html/Xml nodes via XPath.

## Examples

### Make a GET request

```c#
var application = new Application(new Uri("http://my-application", UriKind.Absolute));
var request = new HttpRequest("/relative-url/to/resource");
var requester = new HttpRequester();

var response = await requester.MakeRequest(request, application);
```
	
### Make a POST request

```c#
var application = new Application(new Uri("http://my-application", UriKind.Absolute));
var request = new HttpRequest("/relative-url/to/resource")
{
	Method = HttpMethod.Post,
	Body = new PlainTextRequestData("some-body", "text/plain")
};
var requester = new HttpRequester();

var response = await requester.MakeRequest(request, application);
```
	
### Using response data

```c#
var application = new Application(new Uri("http://my-application", UriKind.Absolute));
var request = new HttpRequest("/relative-url/to/resource");
var requester = new HttpRequester();

var response = await requester.MakeRequest(request, application);

var json = response.Body as JsonResponseData;
var result = json.ReadAs<MyObject>();
```
	
For more examples see `HttpLayer.UnitTests/IntegrationTests.cs`