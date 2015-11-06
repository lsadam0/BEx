// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// Interface to support conversion from Exchange-specific
    /// intermediate types, to the standard BEx.ApiResult Sub-Types
    /// </summary>
    internal interface IExchangeResponse
    {
        /// <summary>
        /// Return the corresponding BEx.ApiResult Sub-Type
        /// corresponding with the type implementing this interface
        /// </summary>
        /// <param name="pair">Trading Pair</param>
        /// <returns>Specific ApiResult Sub-Type</returns>
        BExResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange);
    }
}