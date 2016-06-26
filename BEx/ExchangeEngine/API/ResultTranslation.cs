// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using BEx.ExchangeEngine.API.Commands;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.API
{
    internal class ResultTranslation
    {

        private readonly IExchangeConfiguration _configuration;

        internal ResultTranslation(IExchangeConfiguration configuration)
        {

            _configuration = configuration;
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
                    new[] { responseCollection, pair, _configuration },
                    null);
            }
            var deserialized =
                JsonConvert.DeserializeObject(content, commandReference.IntermediateType) as
                    IExchangeResponseIntermediate<T>;

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
                    new[] { deserialized, pair, _configuration.ExchangeSourceType },
                    null);
            }

            return res;
        }
    }
}