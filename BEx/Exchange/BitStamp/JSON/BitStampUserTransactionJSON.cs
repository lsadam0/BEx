using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace BEx.BitStampSupport
{

    public class BitStampUserTransactionJSON : ExchangeResponse<UserTransaction>
    {

        [JsonProperty("usd")]
        public string Usd { get; set; }

        [JsonProperty("btc")]
        public string Btc { get; set; }

        [JsonProperty("btc_usd")]
        public string BtcUsd { get; set; }

        [JsonProperty("order_id")]
        public int? OrderId { get; set; }

        [JsonProperty("fee")]
        public string Fee { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("datetime")]
        public string Datetime { get; set; }

        public override UserTransaction ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            UserTransaction res = new UserTransaction();

            res.BaseCurrencyAmount = Convert.ToDecimal(Btc);
            res.CounterCurrencyAmount = Convert.ToDecimal(Usd);
            res.ID = Id;

            //- transaction type (0 - deposit; 1 - withdrawal; 2 - market trade)

            if (Type == 0)
                res.Type = UserTransactionType.Deposit;
            else if (Type == 1)
                res.Type = UserTransactionType.Withdrawal;
            else if (Type == 2)
                res.Type = UserTransactionType.Trade;

            res.Fee = Convert.ToDecimal(Fee);

            if (OrderId == null)
                res.OrderID = null;
            else
                res.OrderID = OrderId;

            res.ExchangeRate = Convert.ToDecimal(BtcUsd);

            return res;
        }

    }

}