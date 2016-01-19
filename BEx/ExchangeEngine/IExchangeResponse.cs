// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// Interface to support conversion from Exchange-specific
    /// intermediate types, to the standard BEx.ApiResult Sub-Types
    /// </summary>
    internal interface IExchangeResponse<T> where T : IExchangeResult
    {
        T Convert(TradingPair pair);
    }
}