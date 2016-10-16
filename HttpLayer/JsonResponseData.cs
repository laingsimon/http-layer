using Newtonsoft.Json;
using System.IO;

namespace HttpLayer
{
    public class JsonResponseData : IResponseData
    {
        private readonly string _jsonContent;
        private readonly JsonSerializer _serialiser;

        internal JsonResponseData(
            StreamReader content,
            JsonSerializer serialiser = null)
        {
            _jsonContent = content.ReadToEnd();
            _serialiser = serialiser ?? new JsonSerializer();
        }

        public T ReadAs<T>(JsonSerializer serialiser = null)
        {
            var serialiserToUse = serialiser ?? _serialiser;

            using (var reader = new JsonTextReader(new StringReader(_jsonContent)))
                return serialiserToUse.Deserialize<T>(reader);
        }
    }
}
