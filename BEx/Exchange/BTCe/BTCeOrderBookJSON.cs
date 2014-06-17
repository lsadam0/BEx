using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace BEx.BTCeSupport
{

    internal class BTCeOrderBookJSON
    {

        [JsonProperty("asks")]
        public double[][] Asks { get; set; }

        [JsonProperty("bids")]
        public double[][] Bids { get; set; }

        public OrderBook ToOrderBook(Currency baseCurrency, Currency counterCurrency)
        {

            OrderBook res = new OrderBook();

            res.BaseCurrency = baseCurrency;
            res.CounterCurrency = counterCurrency;

            for (int x = 0; x < Bids.Length; ++x)
            {
                res.BidsByPrice.Add(Convert.ToDecimal(Bids[x][0]), Convert.ToDecimal(Bids[x][1]));
            }


            for (int x = 0; x < Asks.Length; ++x)
            {

                res.AsksByPrice.Add(Convert.ToDecimal(Asks[x][0]), Convert.ToDecimal(Asks[x][1]));
            }

            return res;

        }
    }

}
