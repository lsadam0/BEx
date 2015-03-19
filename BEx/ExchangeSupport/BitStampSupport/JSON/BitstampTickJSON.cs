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

            res.Ask = Conversion.ToDecimalInvariant(ask);
            res.Bid = Conversion.ToDecimalInvariant(bid);
            res.High = Conversion.ToDecimalInvariant(high);
            res.Last = Conversion.ToDecimalInvariant(last);
            res.Low = Conversion.ToDecimalInvariant(low);

            res.Volume = Conversion.ToDecimalInvariant(volume);

            res.Pair = pair;

            return res;
        }
    }
}