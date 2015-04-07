// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Newtonsoft.Json;


namespace BEx.ExchangeEngine.Bitfinex.JSON
{
    internal class DepositAddressIntermediate : IExchangeResponse
    {
        [JsonProperty("result", Required = Required.Always)]
        public string Result { get; set; }

        [JsonProperty("method", Required = Required.Always)]
        public string Method { get; set; }

        [JsonProperty("currency", Required = Required.Always)]
        public string Currency { get; set; }

        [JsonProperty("address", Required = Required.Always)]
        public string Address { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {
            return new DepositAddress(Address, DateTime.UtcNow, pair.BaseCurrency, sourceExchange);
        }
    }
}