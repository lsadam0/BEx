// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Text;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.BitStamp.JSON
{
    internal class ErrorIntermediate : IExchangeResponse
    {
       
        public Error error { get; set; }

        public class Error
        {
            public string[] __all__ { get; set; }
        }

        public BExResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {

            StringBuilder sb = new StringBuilder();

            foreach (string line in error.__all__)
                sb.Append(line);

            return new BExError(ExchangeType.BitStamp)
            {
                Message = sb.ToString()
            };
        }
    }

    


}