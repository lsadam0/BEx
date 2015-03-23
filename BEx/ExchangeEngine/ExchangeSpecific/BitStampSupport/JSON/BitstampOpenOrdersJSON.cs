// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;
using BEx.ExchangeEngine.Utilities;

namespace BEx.ExchangeEngine.BitStampSupport
{
    internal class BitStampOpenOrdersJSON : IExchangeResponse
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("datetime")]
        public string Datetime { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {
            return new Order(Conversion.ToDateTimeInvariant(Datetime), sourceExchange)
            {
                Amount = Conversion.ToDecimalInvariant(Amount),
                Price = Conversion.ToDecimalInvariant(Price),
                TradeType = Type == 0 ? OrderType.Buy : OrderType.Sell,
                Id = Id,
                Pair = pair
            };
        }
    }
}