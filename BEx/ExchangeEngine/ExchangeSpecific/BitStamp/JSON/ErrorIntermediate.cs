// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Text;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.BitStamp.JSON
{
    internal class ErrorIntermediate : IExchangeResponse
    {

        [JsonConverter(typeof(SingleValueArrayConverter))]
        public Error error { get; set; }

        public class Error
        {
            public string[] __all__ { get; set; }
        }

        public BExResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
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
                Error res = null;


                if (reader.TokenType == JsonToken.String)
                {
                    string message = reader.Value.ToString();

                    string[] all = new string[1];
                    all[0] = message;

                    res = new Error()
                    {
                        __all__ = all
                    };
                }
                else if (reader.TokenType == JsonToken.StartObject)
                {
                    res = serializer.Deserialize<Error>(reader);

                }

                return res;
            }

            public override bool CanConvert(Type objectType)
            {
                return false;
            }
        }
    }
}

