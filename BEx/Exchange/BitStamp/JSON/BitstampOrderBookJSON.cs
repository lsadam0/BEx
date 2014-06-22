using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using BEx.Common;

namespace BEx.BitStampSupport
{

    internal class BitstampOrderBookJSON
    {

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("bids")]
        public string[][] Bids { get; set; }

        [JsonProperty("asks")]
        public string[][] Asks { get; set; }

        public OrderBook ToOrderbook(Currency baseCurrency, Currency counterCurrency)
        {
            OrderBook res = new OrderBook();

            res.BaseCurrency = baseCurrency;
            res.CounterCurrency = counterCurrency;


            for (int x = 0; x < Bids.Length; ++x)
            {
                string[] values = Bids[x];

                Decimal price = Convert.ToDecimal(values[0]);
                Decimal amount = Convert.ToDecimal(values[1]);

                res.BidsByPrice.Add(price, amount);

            }


            for (int x = 0; x < Asks.Length; ++x)
            {
                string[] values = Asks[x];

                Decimal price = Convert.ToDecimal(values[0]);
                Decimal amount = Convert.ToDecimal(values[1]);

                res.AsksByPrice.Add(price, amount);


            }


            res.TimeStamp = UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(Timestamp));
            return res;


        }
    }


}
