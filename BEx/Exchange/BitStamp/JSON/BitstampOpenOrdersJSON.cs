using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using BEx.Common;

namespace BEx.BitStampSupport
{
    public class BitStampOpenOrdersJSON : ExchangeResponse<APIResult>
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

        public override APIResult ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            throw new NotImplementedException();
        }

    }
}

