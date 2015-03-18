using BEx.CommandProcessing;
using System;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitstampTickJSON : IExchangeResponse
    {
        public string high { get; set; }

        public string last { get; set; }

        public string timestamp { get; set; }

        public string bid { get; set; }

        public string vwap { get; set; }

        public string volume { get; set; }

        public string low { get; set; }

        public string ask { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            Tick res = new Tick(UnixTime.UnixTimeStampToDateTime(timestamp), ExchangeType.BitStamp);

            res.Ask = Convert.ToDecimal(ask);
            res.Bid = Convert.ToDecimal(bid);
            res.High = Convert.ToDecimal(high);
            res.Last = Convert.ToDecimal(last);
            res.Low = Convert.ToDecimal(low);

            res.Volume = Convert.ToDecimal(volume);

            res.Pair = pair;

            return res;
        }
    }
}