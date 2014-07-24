using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using BEx.Common;

namespace BEx.BitStampSupport
{
    public class BitStampOrderConfirmationJSON
    {

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("datetime")]
        public string Datetime { get; set; }

        public OrderConfirmation ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            OrderConfirmation res = new OrderConfirmation();

            res.Amount = Convert.ToDecimal(Amount);
            res.Price = Convert.ToDecimal(Price);

            if (Type == 0)
                res.Type = OrderType.Buy;
            else
                res.Type = OrderType.Sell;

            res.ID = Id;
            res.Timestamp = Convert.ToDateTime(Datetime);

            res.BaseCurrency = baseCurrency;
            res.CounterCurrency = counterCurrency;

            return res;
        }
    }
}
