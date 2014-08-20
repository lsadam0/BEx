using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx.BitStampSupport
{
    public class BitstampTickJSON : JSONBase
    {
        public string high { get; set; }
        public string last { get; set; }
        public string timestamp { get; set; }
        public string bid { get; set; }
        public string vwap { get; set; }
        public string volume { get; set; }
        public string low { get; set; }
        public string ask { get; set; }

        public Tick ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res = new Tick();

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

        public BitstampTick ToBitStampTick(Currency baseCurrency, Currency counterCurrency)
        {
            BitstampTick res = new BitstampTick();

            res.Ask = Convert.ToDecimal(ask);
            res.Bid = Convert.ToDecimal(bid);
            res.High = Convert.ToDecimal(high);
            res.Last = Convert.ToDecimal(last);
            res.Low = Convert.ToDecimal(low);
            res.Volume = Convert.ToDecimal(volume);
            res.VolumeWeightedAveragePrice = Convert.ToDecimal(vwap);

            res.BaseCurrency = baseCurrency;
            res.CounterCurrency = counterCurrency;

            return res;
        }
    }


    public class JSONBase
    {
        public JSONBase()
        {

        }

    }
}
