// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;
using System;
using System.Text;

namespace BEx.ExchangeEngine.BitStamp.JSON
{
    internal class ErrorIntermediate : IExchangeResponse<BExError>
    {
        [JsonConverter(typeof(SingleValueArrayConverter))]
        public Error error { get; set; }

        public class Error
        {
            public string[] __all__ { get; set; }
        }

        public BExError Convert(TradingPair pair)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string line in error.__all__)
                sb.Append(line);

            return new BExError(ExchangeType.BitStamp)
            {
                Message = sb.ToString()
            };
        }

        public class SingleValueArrayConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.String)
                {
                    string[] all = new string[1];
                    all[0] = reader.Value.ToString();

                    return new Error()
                    {
                        __all__ = all
                    };
                }
                else if (reader.TokenType == JsonToken.StartObject)
                {
                    return serializer.Deserialize<Error>(reader);
                }
                else
                    throw new JsonSerializationException("Unable to deserialize BitStamp error messages");
            }

            public override bool CanConvert(Type objectType)
            {
                return false;
            }
        }
    }
}