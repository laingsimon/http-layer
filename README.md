# HttpLayer

A series of abstractions over the .net HTTP types; facilitating simple HTTP level testing and automation

# Concepts

## Application
This embodies the base url of the application, and can be extended to contain other application-specific rules for when a request is made.

## Session
This option type embodies a session state that can persist between sessions. Commonly this is achieved by logging in and presenting the token provided at login time for all subsequent requests (normally achieved with cookies).

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
* Form data
* Json
* Plain text
* Xml

## Response
* Xml
* Html
* Json
* Plain text