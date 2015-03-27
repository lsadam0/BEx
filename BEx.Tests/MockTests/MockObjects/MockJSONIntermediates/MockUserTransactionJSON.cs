// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Newtonsoft.Json;
using BEx.ExchangeEngine.Utilities;
using BEx.ExchangeEngine;

namespace BEx.UnitTests.MockTests.MockObjects.MockJSONIntermediates
{
    internal class MockUserTransactionJSON : IExchangeResponse
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("fee_currency")]
        public string FeeCurrency { get; set; }

        [JsonProperty("fee_amount")]
        public string FeeAmount { get; set; }

        [JsonProperty("tid")]
        public int Tid { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {

            if (string.IsNullOrWhiteSpace(FeeCurrency))
            {

                // Buy Base
                if (Type == "Buy")
                    FeeCurrency = pair.BaseCurrency.ToString();
                else
                    FeeCurrency = pair.CounterCurrency.ToString();

                // Sell Counter
            }

            return new UserTransaction(Timestamp.ToDateTimeUTC(), sourceExchange)
            {
                OrderId = Tid,
                ExchangeRate = Conversion.ToDecimalInvariant(Price),
                BaseCurrencyAmount = Conversion.ToDecimalInvariant(Amount),
                CounterCurrencyAmount = Conversion.ToDecimalInvariant(Price) * Conversion.ToDecimalInvariant(Amount),
                TradeFee = Conversion.ToDecimalInvariant(FeeAmount),
                TradeFeeCurrency = (Currency)Enum.Parse(typeof(Currency), FeeCurrency),
                TransactionType = (OrderType)Enum.Parse(typeof(OrderType), Type),
                Pair = pair,
                CompletedTime = Timestamp.ToDateTimeUTC()
            };
        }
    }
}