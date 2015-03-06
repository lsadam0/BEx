using System.Collections.Generic;
using System.Reflection;

namespace BEx
{
    public abstract class ExchangeResponse<T>
    {
        public abstract T ConvertToStandard(CurrencyTradingPair pair);

        public static List<R> ConvertListToStandard<C, R>(List<C> responseCollection, CurrencyTradingPair pair)
        {
            List<R> res = new List<R>();

            MethodInfo conversionMethod = typeof(C).GetMethod("ConvertToStandard");

            foreach (C response in responseCollection)
            {
                R iteration = (R)conversionMethod.Invoke(response, new object[] { pair });

                if (iteration != null)
                    res.Add((R)iteration);
            }

            return res;
        }
    }
}