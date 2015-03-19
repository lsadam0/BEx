using BEx.CommandProcessing;
using System;
using System.Globalization;

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

            res.Ask = Convert.ToDecimal(ask, CultureInfo.InvariantCulture);
            res.Bid = Convert.ToDecimal(bid, CultureInfo.InvariantCulture);
            res.High = Convert.ToDecimal(high, CultureInfo.InvariantCulture);
            res.Last = Convert.ToDecimal(last, CultureInfo.InvariantCulture);
            res.Low = Convert.ToDecimal(low, CultureInfo.InvariantCulture);

            res.Volume = Convert.ToDecimal(volume, CultureInfo.InvariantCulture);

            res.Pair = pair;

            return res;
        }
    }
}