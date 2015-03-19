using Newtonsoft.Json;
using System;
using System.Globalization;

namespace BEx.ExchangeSupport.BitStampSupport
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

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            Order res = new Order(Conversion.ToDateTimeInvariant(Datetime), ExchangeType.BitStamp);

            res.Amount = Conversion.ToDecimalInvariant(Amount);
            res.Price = Conversion.ToDecimalInvariant(Price);

            if (Type == 0)
                res.TradeType = OrderType.Buy;
            else
                res.TradeType = OrderType.Sell;

            res.Id = Id;
            res.ExchangeTimestamp = Conversion.ToDateTimeInvariant(Datetime);

            res.Pair = pair;

            return res;
        }
    }
}