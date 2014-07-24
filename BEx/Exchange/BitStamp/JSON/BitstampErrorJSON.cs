using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using BEx.Common;

namespace BEx.BitStampSupport
{
    internal class Error
    {

        [JsonProperty("__all__")]
        public string[] All { get; set; }
    }

    internal class BitStampErrorJSON
    {

        [JsonProperty("error")]
        public Error Error { get; set; }


        public APIError ConvertToStandard()
        {
            APIError res = new APIError();

            StringBuilder errorMessage = new StringBuilder();

            foreach(string msg in this.Error.All)
            {
                errorMessage.Append(msg + " ");
            }

            res.Message = errorMessage.ToString();

            return res;
        }
    }
}
