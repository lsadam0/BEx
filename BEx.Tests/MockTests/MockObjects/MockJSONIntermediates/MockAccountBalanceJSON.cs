﻿// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace BEx.UnitTests.MockTests.MockObjects.MockJSONIntermediates
{
    internal class MockAccountBalanceJSON : IExchangeResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("available")]
        public string Available { get; set; }

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
        }
    }
}