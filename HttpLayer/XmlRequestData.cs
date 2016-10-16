using System.Xml.Linq;

namespace HttpLayer
{
    public class XmlRequestData : PlainTextRequestData
    {
        public XmlRequestData(XDocument body, string contentType = "text/xml")
            :base(body.ToString(), contentType: contentType)
        { }
    }
}
