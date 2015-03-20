using System;
using System.Collections.Generic;
using System.Reflection;
using BEx.ExchangeSupport;
using Newtonsoft.Json;


namespace BEx.CommandProcessing
{
    internal class ResultTranslation
    {
        private readonly ExchangeType _sourceExchange;

        internal ResultTranslation(Exchange source)
        {
            _sourceExchange = source.ExchangeSourceType;
        }

        internal ApiResult Translate(string source, ExchangeCommand executedCommand, CurrencyTradingPair pair)
        {
            if (executedCommand.ReturnsValueType)
                return GetValueType(source, executedCommand, pair);
            else
                return DeserializeObject(source, executedCommand, pair);
        }

        private ApiResult DeserializeObject(string content, ExchangeCommand commandReference, CurrencyTradingPair pair)
        {
            if (commandReference.ReturnsCollection)
            {
                IEnumerable<IExchangeResponse> responseCollection = JsonConvert.DeserializeObject(content, commandReference.IntermediateType) as IEnumerable<IExchangeResponse>;

                return (ApiResult)Activator.CreateInstance(
                                                    commandReference.ReturnType,
                                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                                    null,
                                                    new object[] { responseCollection, pair, _sourceExchange },
                                                    null);
            }
            else
            {
                IExchangeResponse deserialized = JsonConvert.DeserializeObject(content, commandReference.IntermediateType) as IExchangeResponse;

                return deserialized.ConvertToStandard(pair);
            }
        }

        private ApiResult GetValueType(string content, ExchangeCommand command, CurrencyTradingPair pair)
        {
            ApiResult res = null;
            object deserialized = JsonConvert.DeserializeObject(content, command.IntermediateType);

            if (deserialized.GetType() != command.ReturnType)
            {
                res = (ApiResult)Activator.CreateInstance(
                                                    command.ReturnType,
                                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                                    null,
                                                    new[] { deserialized, _sourceExchange, pair },
                                                    null);
            }

            return res;
        }
    }
}