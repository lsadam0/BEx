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

        public Order ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            Order res = new Order();

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

        public static OpenOrders ConvertListToStandard(List<BitStampOrderConfirmationJSON> orders, Currency baseCurrency, Currency counterCurrency)
        {
            OpenOrders res = new OpenOrders();

            foreach (BitStampOrderConfirmationJSON source in orders)
            {
                res.Orders.Add(source.ConvertToStandard(baseCurrency, counterCurrency));
            }

            return res;
        }
    }
}
