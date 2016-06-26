// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using BEx.ExchangeEngine.API;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;
using BEx.Exchanges.BitStamp;

namespace BEx.Exchanges.BitStamp.API.Models
{
    internal class AccountBalanceModel : IExchangeResponseIntermediate<AccountBalance>
    {
        [JsonProperty("btc_available", Required = Required.Always)]
        public string BtcAvailable { get; set; }

        [JsonProperty("btc_balance", Required = Required.Always)]
        public string BtcBalance { get; set; }

        [JsonProperty("btc_reserved", Required = Required.Always)]
        public string BtcReserved { get; set; }

        public string eur_available { get; set; }

        public string eur_balance { get; set; }

        public string eur_reserved { get; set; }

        [JsonProperty("fee", Required = Required.Always)]
        public string Fee { get; set; }

        [JsonProperty("usd_available", Required = Required.Always)]
        public string UsdAvailable { get; set; }

        [JsonProperty("usd_balance", Required = Required.Always)]
        public string UsdBalance { get; set; }

        [JsonProperty("usd_reserved", Required = Required.Always)]
        public string UsdReserved { get; set; }

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

            var eur = new Balance(
                Conversion.ToDecimalInvariant(eur_available),
                Currency.EUR,
                Conversion.ToDecimalInvariant(eur_balance),
                exchangeDate,
                ExchangeType.BitStamp,
                Conversion.ToDecimalInvariant(eur_reserved));

            return new AccountBalance(
                new List<Balance> { btc, usd, eur },
                pair,
                Configuration.Singleton);
        }
    }
}