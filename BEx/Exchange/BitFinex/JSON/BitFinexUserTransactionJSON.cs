﻿using BEx.Common;
using Newtonsoft.Json;
using System;

namespace BEx.BitFinexSupport
{
    public class BitFinexUserTransactionJSON : ExchangeResponse<UserTransaction>
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

        public override UserTransaction ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            UserTransaction u = new UserTransaction(UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(Timestamp)), ExchangeType.BitFinex);

            u.ID = Tid;
            u.Type = UserTransactionType.Trade;
            u.OrderID = Tid;

            return u;
        }
    }
}