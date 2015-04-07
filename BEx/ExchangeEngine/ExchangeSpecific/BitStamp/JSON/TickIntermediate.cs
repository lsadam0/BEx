// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;


namespace BEx.ExchangeEngine.BitStamp.JSON
{
    internal class TickIntermediate : IExchangeResponse
    {
        [JsonProperty("high", Required = Required.Always)]
        public string high
        {
            get;
            set;
        }

        [JsonProperty("last", Required = Required.Always)]
        public string last
        {
            get;
            set;
        }

        [JsonProperty("timestamp", Required = Required.Always)]
        public string timestamp
        {
            get;
            set;
        }

        [JsonProperty("bid", Required = Required.Always)]
        public string bid
        {
            get;
            set;
        }

        [JsonProperty("vwap", Required = Required.Always)]
        public string vwap
        {
            get;
            set;
        }

        [JsonProperty("volume", Required = Required.Always)]
        public string volume
        {
            get;
            set;
        }

        [JsonProperty("low", Required = Required.Always)]
        public string low
        {
            get;
            set;
        }

        [JsonProperty("ask", Required = Required.Always)]
        public string ask
        {
            get;
            set;
        }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {
            return new Tick(timestamp.ToDateTimeUTC(), sourceExchange)
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