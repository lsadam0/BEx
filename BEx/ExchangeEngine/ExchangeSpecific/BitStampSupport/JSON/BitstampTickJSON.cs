using BEx.ExchangeEngine.Utilities;



namespace BEx.ExchangeEngine.BitStampSupport
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
            return new Tick(UnixTime.UnixTimeStampToDateTime(timestamp), ExchangeType.BitStamp)
            {
                Ask = Conversion.ToDecimalInvariant(ask),
                Bid = Conversion.ToDecimalInvariant(bid),
                High = Conversion.ToDecimalInvariant(high),
                Last = Conversion.ToDecimalInvariant(last),
                Low = Conversion.ToDecimalInvariant(low),
                Volume = Conversion.ToDecimalInvariant(volume),
                Pair = pair
            };
        }
    }
}