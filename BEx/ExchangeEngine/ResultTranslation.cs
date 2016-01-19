// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;
using System;
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

        private ExchangeType sourceExchange;

        internal ResultTranslation(ExchangeType sourceExchange)
        {
            this.sourceExchange = sourceExchange;
        }

        /// <summary>
        /// Consume JSON response and return the specific BEx.ApiResponse sub-type
        /// object
        /// </summary>
        /// <param name="source">JSON Response</param>
        /// <param name="executedCommand">Reference Command</param>
        /// <param name="pair">Trading Pair</param>
        /// <returns>Specific ApiResult Sub-Type</returns>
        internal T Translate<T>(string source, IExchangeCommand<T> executedCommand, TradingPair pair) where T : IExchangeResult
        {
            if (executedCommand.ReturnsValueType)
            {
                return GetValueType(source, executedCommand, pair);
            }
            else
            {
                return DeserializeObject(source, executedCommand, pair);
            }
        }

        /// <summary>
        /// Deserialize JSON responses that deserialize to reference types,
        /// including Collections
        /// </summary>
        /// <param name="content">JSON Response</param>
        /// <param name="commandReference">Reference ExchangeCommand</param>
        /// <param name="pair">Trading Pair</param>

        private T DeserializeObject<T>(string content, IExchangeCommand<T> commandReference, TradingPair pair) where T : IExchangeResult
        {
            if (commandReference.ReturnsCollection)
            {
                var responseCollection = JsonConvert.DeserializeObject(content, commandReference.IntermediateType);// as IEnumerable<IExchangeResponse>;

                return (T)Activator.CreateInstance(
                                                    commandReference.ApiResultSubType,
                                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                                    null,
                                                    new object[] { responseCollection, pair, sourceExchange },
                                                    null);
            }
            else
            {
                var deserialized = JsonConvert.DeserializeObject(content, commandReference.IntermediateType) as IExchangeResponse<T>;

                return deserialized.Convert(pair);
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
        private T GetValueType<T>(string content, IExchangeCommand<T> command, TradingPair pair) where T : IExchangeResult
        {
            T res = default(T);

            // boxing
            object deserialized = JsonConvert.DeserializeObject(content, command.IntermediateType);

            if (deserialized.GetType() != command.ApiResultSubType)
            {
                res = (T)Activator.CreateInstance(
                                                    command.ApiResultSubType,
                                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                                    null,
                                                    new[] { deserialized, pair, sourceExchange },
                                                    null);
            }

            return res;
        }
    }
}