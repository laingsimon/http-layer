using System.IO;
using System.Xml.Linq;

namespace HttpLayer
{
    public class XmlResponseData : IResponseData
    {
        private readonly string _content;

        internal XmlResponseData(StreamReader content)
        {
            _content = content.ReadToEnd();
        }

        public XDocument ReadXml()
        {
            return XDocument.Parse(_content);
        }
    }
}
