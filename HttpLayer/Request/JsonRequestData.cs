﻿using System.IO;
using Newtonsoft.Json;

namespace HttpLayer.Request
{
    public class JsonRequestData : PlainTextRequestData
    {
        public JsonRequestData(
            object body,
            string contentType = "application/json",
            JsonSerializer serialiser = null)
            :base(
                 _SerialiseBody(body, serialiser ?? new JsonSerializer()),
                 contentType: contentType)
        { }

        private static string _SerialiseBody(object body, JsonSerializer serialiser)
        {
            using (var writer = new StringWriter())
            {
                serialiser.Serialize(writer, body);
                writer.Close();

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}