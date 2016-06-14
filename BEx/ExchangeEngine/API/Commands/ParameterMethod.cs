// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BEx.ExchangeEngine.API.Commands
{
    /// <summary>
    ///     Token that describes how a parameter will be delivered to the targeted exchange
    /// </summary>
    public enum ParameterMethod
    {
        /// <summary>
        ///     Parameter is part of the address e.g. /Tick/{Pair}
        /// </summary>
        Url,

        /// <summary>
        ///     POST Parameter
        /// </summary>
        Post,

        /// <summary>
        ///     Parameter is part of the Query String e.g. /GetTick/?pair={Pair}
        /// </summary>
        QueryString
    }
}