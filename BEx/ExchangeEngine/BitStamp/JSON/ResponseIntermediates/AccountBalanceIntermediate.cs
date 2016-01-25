// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.BitStamp.JSON.ResponseIntermediates
{
    internal class AccountBalanceIntermediate : IExchangeResponse<AccountBalance>
    {
        [JsonProperty("btc_reserved", Required = Required.Always)]
        public string BtcReserved { get; set; }

        [JsonProperty("fee", Required = Required.Always)]
        public string Fee { get; set; }

        [JsonProperty("btc_available", Required = Required.Always)]
        public string BtcAvailable { get; set; }

        [JsonProperty("usd_reserved", Required = Required.Always)]
        public string UsdReserved { get; set; }

        [JsonProperty("btc_balance", Required = Required.Always)]
        public string BtcBalance { get; set; }

        [JsonProperty("usd_balance", Required = Required.Always)]
        public string UsdBalance { get; set; }

        [JsonProperty("usd_available", Required = Required.Always)]
        public string UsdAvailable { get; set; }

        public AccountBalance Convert(TradingPair pair)
        {

            var exchangeDate = DateTime.UtcNow;


            var btc = new Balance(
                Conversion.ToDecimalInvariant(BtcAvailable),
                Currency.BTC,
                Conversion.ToDecimalInvariant(BtcBalance),
                exchangeDate,
                ExchangeType.BitStamp,
                Conversion.ToDecimalInvariant(BtcReserved));

            var usd = new Balance(
                Conversion.ToDecimalInvariant(UsdAvailable),
                Currency.USD,
                Conversion.ToDecimalInvariant(UsdBalance),
                exchangeDate,
                ExchangeType.BitStamp,
                Conversion.ToDecimalInvariant(UsdReserved));


            return new AccountBalance(
                new List<Balance>() { btc, usd },
                pair,
                ExchangeType.BitStamp);
        }
    }
}