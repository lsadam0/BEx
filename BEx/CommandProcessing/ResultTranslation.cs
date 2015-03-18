using BEx.ExchangeSupport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BEx.CommandProcessing
{
    internal class ResultTranslation
    {
        private ExchangeType SourceExchange;

        internal ResultTranslation(Exchange source)
        {
            SourceExchange = source.ExchangeSourceType;
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
            ApiResult res = default(ApiResult);

            if (commandReference.ReturnsCollection)
            {
                IEnumerable<IExchangeResponse> responseCollection = JsonConvert.DeserializeObject(content, commandReference.IntermediateType) as IEnumerable<IExchangeResponse>;

                res = (ApiResult)Activator.CreateInstance(commandReference.ReturnType,
                                              BindingFlags.NonPublic | BindingFlags.Instance,
                                              null,
                                              new object[] { responseCollection, pair, SourceExchange },
                                              null);
            }
            else
            {
                IExchangeResponse deserialized = JsonConvert.DeserializeObject(content, commandReference.IntermediateType) as IExchangeResponse;

                res = deserialized.ConvertToStandard(pair);
            }

            return res;
        }

        private ApiResult GetValueType(string content, ExchangeCommand command, CurrencyTradingPair pair)
        {
            ApiResult res = null;
            object deserialized = JsonConvert.DeserializeObject(content, command.IntermediateType);

            if (deserialized.GetType() != command.ReturnType)
            {
                res = (ApiResult)Activator.CreateInstance(command.ReturnType,
                                              BindingFlags.NonPublic | BindingFlags.Instance,
                                              null,
                                              new object[] { deserialized, SourceExchange, pair },
                                              null);
            }

            return res;
        }
    }
}