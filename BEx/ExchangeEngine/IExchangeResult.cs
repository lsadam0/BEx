using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public interface IExchangeResult
    {

        ExchangeType SourceExchange
        { get; }

        DateTime ExchangeTimeStampUTC
        { get; }

        DateTime LocalTimeStampUTC
        { get; }
    }



}
