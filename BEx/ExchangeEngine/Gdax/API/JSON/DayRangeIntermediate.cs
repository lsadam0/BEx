using System;
using BEx.ExchangeEngine.Utilities;
using BEx.Response;

namespace BEx.ExchangeEngine.Gdax.API.JSON
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
                ExchangeType.Gdax);
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