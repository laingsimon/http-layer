using System.IO;

namespace HttpLayer
{
    public interface IResponseDataFactory
    {
        IResponseData GetResponseData(StreamReader content, string contentType);
    }

    internal class ResponseDataFactory : IResponseDataFactory
    {
        public IResponseData GetResponseData(StreamReader content, string contentType)
        {
            if (contentType.StartsWith("text/html"))
                return new HtmlResponseData(content);

            if (contentType.StartsWith("text/xml"))
                return new XmlResponseData(content);

            if (contentType.StartsWith("application/json"))
                return new JsonResponseData(content);

            return new PlainTextResponseData(content);
        }
    }
}
