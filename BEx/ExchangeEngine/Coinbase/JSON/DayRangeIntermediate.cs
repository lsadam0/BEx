using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;
using BEx.Response;

namespace BEx.ExchangeEngine.Coinbase.JSON
{
    internal class DayRangeIntermediate : IExchangeResponseIntermediate<DayRange>
    {
        public string open { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string volume { get; set; }

        public DayRange Convert(TradingPair pair)
        {
            return new DayRange(
                Conversion.ToDecimalInvariant(high),
                Conversion.ToDecimalInvariant(low),
                DateTime.UtcNow,
                pair,
                ExchangeType.Coinbase);

        }
    }
}

/*{
    "open": "34.19000000",
    "high": "95.70000000",
    "low": "7.06000000",
    "volume": "2.41000000"
}
*/
