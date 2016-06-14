using System;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

using BEx.ExchangeEngine.API;

namespace BEx.Exchanges.Gdax.API.Models
{
    internal class TickModel : IExchangeResponseIntermediate<Tick>
    {
        [JsonProperty("trade_id", Required = Required.Always)]
        public int trade_id { get; set; }

        [JsonProperty("price", Required = Required.Always)]
        public string price { get; set; }

        [JsonProperty("size", Required = Required.Always)]
        public string size { get; set; }

        [JsonProperty("bid", Required = Required.Always)]
        public string bid { get; set; }

        [JsonProperty("ask", Required = Required.Always)]
        public string ask { get; set; }

        [JsonProperty("volume", Required = Required.Always)]
        public string volume { get; set; }

        [JsonProperty("time", Required = Required.Always)]
        public DateTime time { get; set; }

        public Tick Convert(TradingPair pair)
        {
            return new Tick(
                Conversion.ToDecimalInvariant(ask),
                Conversion.ToDecimalInvariant(bid),
                Conversion.ToDecimalInvariant(price),
                Conversion.ToDecimalInvariant(volume),
                pair,
                ExchangeType.Gdax,
                (long) time.ToUnixTime());

            /*{"trade_id":6442532,"price":"372.63","size":"0.31379","bid":"372.54","ask":"372.64","volume":"6866.60379037","time":"2016-02-03T01:05:02.047318Z"}*/
        }
    }
}