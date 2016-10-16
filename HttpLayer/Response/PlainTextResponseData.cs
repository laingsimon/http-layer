using System.IO;

namespace HttpLayer.Response
{
    public class PlainTextResponseData : IResponseData
    {
        internal PlainTextResponseData(StreamReader content)
        {
            Content = content.ReadToEnd();
        }

        public string Content { get; }
    }
}
