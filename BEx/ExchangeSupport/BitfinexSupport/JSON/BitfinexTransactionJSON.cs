using BEx.CommandProcessing;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace BEx.ExchangeSupport.BitfinexSupport
{
    internal class BitFinexTransactionJSON : IExchangeResponse
    {
        [JsonProperty("timestamp")]
        public int timestamp { get; set; }

        [JsonProperty("tid")]
        public int tid { get; set; }

        [JsonProperty("price")]
        public string price { get; set; }

        [JsonProperty("amount")]
        public string amount { get; set; }

        [JsonProperty("exchange")]
        public string exchange { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            Transaction res = new Transaction(DateTime.Now, ExchangeType.Bitfinex);

            res.Amount = Convert.ToDecimal(amount, CultureInfo.InvariantCulture);
            res.Price = Convert.ToDecimal(price, CultureInfo.InvariantCulture);
            res.TransactionId = Convert.ToInt64(tid, CultureInfo.InvariantCulture);
            res.CompletedTime = UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(timestamp, CultureInfo.InvariantCulture));
            res.Pair = pair;

            return res;
        }
    }
}