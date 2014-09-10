using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using BEx.Common;

namespace BEx.BitStampSupport
{

    public class BitStampErrorJSON
    {

        [JsonProperty("error")]
        public string Error { get; set; }

        public APIError ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            APIError error = new APIError();

            error.Message = Error;

            return error;
        }
    }

}
