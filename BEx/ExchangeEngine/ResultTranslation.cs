// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine
{
    internal class ResultTranslation
    {
        private readonly ExchangeType sourceExchange;

        internal ResultTranslation(ExchangeType sourceExchange)
        {
            this.sourceExchange = sourceExchange;
        }

        internal T Translate<T>(string source, IExchangeCommand<T> executedCommand, TradingPair pair)
            where T : IExchangeResult
        {
            if (executedCommand.ReturnsValueType)
            {
                return GetValueType(source, executedCommand, pair);
            }
            return DeserializeObject(source, executedCommand, pair);
        }

        private T DeserializeObject<T>(string content, IExchangeCommand<T> commandReference, TradingPair pair)
            where T : IExchangeResult
        {
            if (commandReference.ReturnsCollection)
            {
                var responseCollection = JsonConvert.DeserializeObject(content, commandReference.IntermediateType);
                // as IEnumerable<IExchangeResponse>;

                return (T) Activator.CreateInstance(
                    commandReference.ApiResultSubType,
                    BindingFlags.NonPublic | BindingFlags.Instance,
                    null,
                    new[] {responseCollection, pair, sourceExchange},
                    null);
            }
            var deserialized =
                JsonConvert.DeserializeObject(content, commandReference.IntermediateType) as IExchangeResponse<T>;

            return deserialized.Convert(pair);
        }

        private T GetValueType<T>(string content, IExchangeCommand<T> command, TradingPair pair)
            where T : IExchangeResult
        {
            var res = default(T);

            // boxing
            var deserialized = JsonConvert.DeserializeObject(content, command.IntermediateType);

            if (deserialized.GetType() != command.ApiResultSubType)
            {
                res = (T) Activator.CreateInstance(
                    command.ApiResultSubType,
                    BindingFlags.NonPublic | BindingFlags.Instance,
                    null,
                    new[] {deserialized, pair, sourceExchange},
                    null);
            }

            return res;
        }
    }
}