using BEx.Common;
using System;

namespace BEx.BitStampSupport
{
    public class BitstampTickJSON : ExchangeResponse<Tick>
    {
        public string high { get; set; }

        public string last { get; set; }

        public string timestamp { get; set; }

        public string bid { get; set; }

        public string vwap { get; set; }

        public string volume { get; set; }

        public string low { get; set; }

        public string ask { get; set; }

        public override Tick ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res = new Tick(UnixTime.UnixTimeStampToDateTime(timestamp));

            res.Ask = Convert.ToDecimal(ask);
            res.Bid = Convert.ToDecimal(bid);
            res.High = Convert.ToDecimal(high);
            res.Last = Convert.ToDecimal(last);
            res.Low = Convert.ToDecimal(low);

            res.Volume = Convert.ToDecimal(volume);

            res.BaseCurrency = baseCurrency;
            res.CounterCurrency = counterCurrency;

            return res;
        }
    }
}