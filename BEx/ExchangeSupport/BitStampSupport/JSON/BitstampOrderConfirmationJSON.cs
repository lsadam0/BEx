using Newtonsoft.Json;
using System;
using System.Globalization;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitStampOrderConfirmationJSON : IExchangeResponse
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("datetime")]
        public string Datetime { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            Order res = new Order(Convert.ToDateTime(Datetime, CultureInfo.InvariantCulture), ExchangeType.BitStamp);

            res.Amount = Convert.ToDecimal(Amount, CultureInfo.InvariantCulture);
            res.Price = Convert.ToDecimal(Price, CultureInfo.InvariantCulture);

            if (Type == 0)
                res.TradeType = OrderType.Buy;
            else
                res.TradeType = OrderType.Sell;

            res.Id = Id;
            res.ExchangeTimestamp = Convert.ToDateTime(Datetime, CultureInfo.InvariantCulture);

            res.Pair = pair;

            return res;
        }
    }
}