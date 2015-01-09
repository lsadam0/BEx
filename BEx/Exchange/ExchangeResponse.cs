using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public abstract class ExchangeResponse
    {
        public abstract APIResult ConvertToStandard(Currency baseCurrency, Currency counterCurrency);

        public static List<APIResult> ConvertListToStandard<C>( responseCollection, Currency baseCurrency, Currency counterCurrency)
        {
            List<APIResult> res = new List<APIResult>();

            
            foreach (C response in responseCollection)
            {

                res.Add(((ExchangeResponse)response).ConvertToStandard(baseCurrency, counterCurrency));
            }

            return res;
        }
    }
}
