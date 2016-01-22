// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using BEx.ExchangeEngine.Commands;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine
{
    internal class ResultTranslation
    {
        private readonly ExchangeType _sourceExchange;

        internal ResultTranslation(ExchangeType sourceExchange)
        {
            _sourceExchange = sourceExchange;
        }

        internal T Translate<T>(string source, IExchangeCommand executedCommand, TradingPair pair)
            where T : IExchangeResult
        {
            if (executedCommand.ReturnsValueType)
            {
                return GetValueType<T>(source, executedCommand, pair);
            }
            return DeserializeObject<T>(source, executedCommand, pair);
        }

        private T DeserializeObject<T>(string content, IExchangeCommand commandReference, TradingPair pair)
            where T : IExchangeResult
        {
            if (commandReference.ReturnsCollection)
            {
                var responseCollection = JsonConvert.DeserializeObject(content, commandReference.IntermediateType);

                return (T)Activator.CreateInstance(
                    commandReference.ApiResultSubType,
                    BindingFlags.NonPublic | BindingFlags.Instance,
                    null,
                    new[] { responseCollection, pair, _sourceExchange },
                    null);
            }
            var deserialized =
                JsonConvert.DeserializeObject(content, commandReference.IntermediateType) as IExchangeResponse<T>;

            return deserialized.Convert(pair);
        }

        private T GetValueType<T>(string content, IExchangeCommand command, TradingPair pair)
            where T : IExchangeResult
        {
            var res = default(T);

            // boxing
            var deserialized = JsonConvert.DeserializeObject(content, command.IntermediateType);

            if (deserialized.GetType() != command.ApiResultSubType)
            {
                res = (T)Activator.CreateInstance(
                    command.ApiResultSubType,
                    BindingFlags.NonPublic | BindingFlags.Instance,
                    null,
                    new[] { deserialized, pair, _sourceExchange },
                    null);
            }

            return res;
        }
    }
}