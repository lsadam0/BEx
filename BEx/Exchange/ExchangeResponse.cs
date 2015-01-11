using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BEx
{
    public abstract class ExchangeResponse
    {
        public abstract APIResult ConvertToStandard(Currency baseCurrency, Currency counterCurrency);

        
        public static List<APIResult> ConvertListToStandard<C>(List<C> responseCollection, Currency baseCurrency, Currency counterCurrency)
        {
            List<APIResult> res = new List<APIResult>();

            MethodInfo conversionMethod = typeof(C).GetMethod("ConvertToStandard");
            
            foreach (C response in responseCollection)
            {
                res.Add((APIResult)conversionMethod.Invoke(response, new object[] { baseCurrency, counterCurrency }));
            }

            return res;
        }

    }
}
