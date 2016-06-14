// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BEx.ExchangeEngine.API
{
    /// <summary>
    ///     Interface to support conversion from Exchange-specific
    ///     intermediate types, to the standard BEx.ApiResult Sub-Types
    /// </summary>
    internal interface IExchangeResponseIntermediate<out T> where T : IExchangeResult
    {
        T Convert(TradingPair pair);
    }
}