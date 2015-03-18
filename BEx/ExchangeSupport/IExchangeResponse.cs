using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx.ExchangeSupport
{
    internal interface IExchangeResponse
    {
        APIResult ConvertToStandard(CurrencyTradingPair pair);
    }
}