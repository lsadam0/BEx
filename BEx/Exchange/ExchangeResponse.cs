using System.Collections.Generic;
using System.Reflection;

namespace BEx
{
    public abstract class ExchangeResponse<T>
    {
        public abstract T ConvertToStandard(Currency baseCurrency, Currency counterCurrency);

        public static List<R> ConvertListToStandard<C, R>(List<C> responseCollection, Currency baseCurrency, Currency counterCurrency)
        {
            List<R> res = new List<R>();

            MethodInfo conversionMethod = typeof(C).GetMethod("ConvertToStandard");

            foreach (C response in responseCollection)
            {
                res.Add((R)conversionMethod.Invoke(response, new object[] { baseCurrency, counterCurrency }));
            }

            return res;
        }
    }
}