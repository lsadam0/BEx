// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// Responsible for translating the JSON response string into
    /// the Intermediate IExchangeResponse Type for the command for the specific Exchange,
    /// and then invoking the transformation from IExchangeResponse into the specific
    /// BEx.ApiResult sub-type object
    /// </summary>
    internal class ResultTranslation
    {
        private readonly Exchange _sourceExchange;

        internal ResultTranslation(Exchange source)
        {
            _sourceExchange = source;
        }

        /// <summary>
        /// Consume JSON response and return the specific BEx.ApiResponse sub-type
        /// object
        /// </summary>
        /// <param name="source">JSON Response</param>
        /// <param name="executedCommand">Reference Command</param>
        /// <param name="pair">Trading Pair</param>
        /// <returns>Specific ApiResult Sub-Type</returns>
        internal BExResult Translate(string source, IExchangeCommand executedCommand, CurrencyTradingPair pair)
        {
            if (executedCommand.ReturnsValueType)
                return GetValueType(source, executedCommand, pair);
            else
                return DeserializeObject(source, executedCommand, pair);
        }

        /// <summary>
        /// Deserialize JSON responses that deserialize to reference types,
        /// including Collections
        /// </summary>
        /// <param name="content">JSON Response</param>
        /// <param name="commandReference">Reference ExchangeCommand</param>
        /// <param name="pair">Trading Pair</param>

        private BExResult DeserializeObject(string content, IExchangeCommand commandReference, CurrencyTradingPair pair)
        {
            if (commandReference.ReturnsCollection)
            {
                IEnumerable<IExchangeResponse> responseCollection = JsonConvert.DeserializeObject(content, commandReference.IntermediateType) as IEnumerable<IExchangeResponse>;

                return (BExResult)Activator.CreateInstance(
                                                    commandReference.ApiResultSubType,
                                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                                    null,
                                                    new object[] { responseCollection, pair, _sourceExchange },
                                                    null);
            }
            else
            {
                IExchangeResponse deserialized = JsonConvert.DeserializeObject(content, commandReference.IntermediateType) as IExchangeResponse;

                return deserialized.ConvertToStandard(pair, _sourceExchange);
            }
        }

        /// <summary>
        /// Deserialize Value-type (and string!) JSON responses into a specific
        /// ApiResult Sub-Type
        /// </summary>
        /// <param name="content">JSON Response</param>
        /// <param name="command">Reference Command</param>
        /// <param name="pair">Trading Pair</param>
        /// <returns>Specific ApiResult Sub-Type</returns>
        private BExResult GetValueType(string content, IExchangeCommand command, CurrencyTradingPair pair)
        {
            BExResult res = null;

            // boxing
            object deserialized = JsonConvert.DeserializeObject(content, command.IntermediateType);

            if (deserialized.GetType() != command.ApiResultSubType)
            {
                res = (BExResult)Activator.CreateInstance(
                                                    command.ApiResultSubType,
                                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                                    null,
                                                    new[] { deserialized, _sourceExchange, pair },
                                                    null);
            }

            return res;
        }
    }
}