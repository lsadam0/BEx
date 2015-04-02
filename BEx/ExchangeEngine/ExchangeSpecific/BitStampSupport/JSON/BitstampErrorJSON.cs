// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Text;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.BitStampSupport
{
    internal class BitStampErrorJSON : IExchangeResponse
    {
        [JsonProperty("error")]
        public Error error { get; set; }

        public class Error
        {
            public string[] __all__ { get; set; }
        }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {

            StringBuilder sb = new StringBuilder();

            foreach (string line in error.__all__)
                sb.AppendLine(line);

            return new ApiError(ExchangeType.BitStamp)
            {
                Message = sb.ToString()
            };
        }
    }







}