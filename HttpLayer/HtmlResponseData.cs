using HtmlAgilityPack;
using System.IO;
using System.Xml.XPath;

namespace HttpLayer
{
    public class HtmlResponseData : IResponseData
    {
        private readonly string _content;

        internal HtmlResponseData(StreamReader reader)
        {
            _content = reader.ReadToEnd();
        }

        public IXPathNavigable GetDocument()
        {
            var document = new HtmlDocument();
            document.LoadHtml(_content);

            return document;
        }
    }
}
