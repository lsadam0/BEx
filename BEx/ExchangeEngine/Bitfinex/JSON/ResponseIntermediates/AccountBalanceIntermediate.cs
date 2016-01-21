﻿// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.Bitfinex.JSON
{
    internal class AccountBalanceIntermediate : IExchangeResponse<Balance>
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
            Balance res = default(Balance);

            if (Type == "exchange")
            {
                res = new Balance(DateTime.UtcNow, ExchangeType.Bitfinex);
                Currency balanceCurrency;

                if (Enum.TryParse(Currency.ToUpper(CultureInfo.InvariantCulture), out balanceCurrency))
                {
                    res.BalanceCurrency = balanceCurrency;
                    res.AvailableToTrade = Conversion.ToDecimalInvariant(Available);
                    res.TotalBalance = Conversion.ToDecimalInvariant(Amount);
                }
            }

            return res;
        }

        /*
        public BExResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {
            Balance res = null;

            if (Type == "exchange")
            {
                res = new Balance(DateTime.UtcNow, sourceExchange);
                Currency balanceCurrency;

                if (Enum.TryParse(Currency.ToUpper(CultureInfo.InvariantCulture), out balanceCurrency))
                {
                    res.BalanceCurrency = balanceCurrency;
                    res.AvailableToTrade = Conversion.ToDecimalInvariant(Available);
                    res.TotalBalance = Conversion.ToDecimalInvariant(Amount);
                }
            }

            return res;
        }*/
    }
}