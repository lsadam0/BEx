// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.Bitfinex.JSON.ResponseIntermediates
{
    internal class UserTransactionIntermediate : IExchangeResponse<UserTransaction>
    {
        [JsonProperty("amount", Required = Required.Always)]
        public string Amount { get; set; }

        [JsonProperty("exchange", Required = Required.Always)]
        public string Exchange { get; set; }

        [JsonProperty("fee_amount", Required = Required.Always)]
        public string FeeAmount { get; set; }

        [JsonProperty("fee_currency", Required = Required.Always)]
        public string FeeCurrency { get; set; }

        [JsonProperty("price", Required = Required.Always)]
        public string Price { get; set; }

        [JsonProperty("tid", Required = Required.Always)]
        public int Tid { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public string Timestamp { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }

        public UserTransaction Convert(TradingPair pair)
        {
            if (string.IsNullOrWhiteSpace(FeeCurrency))
            {
                // Buy Base
                FeeCurrency = Type == "Buy" ? pair.BaseCurrency.ToString() : pair.CounterCurrency.ToString();

                // Sell Counter
            }

            return new UserTransaction(Timestamp.ToDateTimeUTC(), ExchangeType.Bitfinex)
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