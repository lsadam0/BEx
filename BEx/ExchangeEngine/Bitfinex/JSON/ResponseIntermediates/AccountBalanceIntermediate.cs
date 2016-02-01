// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.Bitfinex.JSON.ResponseIntermediates
{
    internal class AccountBalanceIntermediate : IExchangeResponseIntermediate<Balance>
    {
        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }

        [JsonProperty("currency", Required = Required.Always)]
        public string Currency { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        public string Amount { get; set; }

        [JsonProperty("available", Required = Required.Always)]
        public string Available { get; set; }

        public Balance Convert(TradingPair pair)
        {
            if (Type == "exchange")
            {
                Currency balanceCurrency;

                if (Enum.TryParse(Currency.ToUpper(CultureInfo.InvariantCulture), out balanceCurrency))
                {
                    var avail = Conversion.ToDecimalInvariant(Available);
                    var amount = Conversion.ToDecimalInvariant(Amount);

                    return new Balance(
                        avail,
                        balanceCurrency,
                        amount,
                        DateTime.UtcNow,
                        ExchangeType.Bitfinex,
                        amount - avail);
                }
            }

            return default(Balance);
        }
    }
}