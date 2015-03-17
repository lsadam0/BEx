using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BEx.CommandProcessing
{
    internal class ResultTranslation
    {
        private ExchangeType SourceExchange;

        internal ResultTranslation(ExchangeType sourceExchange)
        {
            SourceExchange = sourceExchange;
        }

        internal APIResult Translate(string source, ExchangeCommand executedCommand, CurrencyTradingPair pair)
        {
            if (executedCommand.ReturnsValueType)
                return GetValueType(source, executedCommand, pair);
            else
                return DeserializeObject(source, executedCommand, pair);
        }

        private APIResult DeserializeObject(string content, ExchangeCommand commandReference, CurrencyTradingPair pair)
        {
            APIResult res = default(APIResult);

            dynamic deserialized = JsonConvert.DeserializeObject(content, commandReference.IntermediateType);//JsonConvert.DeserializeObject<J>(content);

            MethodInfo conversionMethod = deserialized.GetType().GetMethod("ConvertToStandard");

            if (conversionMethod == null)
            {
                // Json Type
                Type baseT = ListOfWhat(deserialized);

                // Get Method from ExchangeResponse
                conversionMethod = baseT.BaseType.GetMethod("ConvertListToStandard");

                MethodInfo singleConversionMethod = baseT.GetMethod("ConvertToStandard");

                Type entryType = singleConversionMethod.ReturnType;

                MethodInfo generic = conversionMethod.MakeGenericMethod(baseT, entryType);

                dynamic collection = generic.Invoke(this, new object[] { deserialized, pair });

                res = (APIResult)Activator.CreateInstance(commandReference.ReturnType,
                                                            BindingFlags.NonPublic | BindingFlags.Instance,
                                                            null,
                                                            new object[] { collection, pair, SourceExchange },
                                                            null); // Culture?
            }
            else
                res = (APIResult)conversionMethod.Invoke(deserialized, new object[] { pair });

            return res;
        }

        private APIResult GetValueType(string content, ExchangeCommand command, CurrencyTradingPair pair)
        {
            APIResult res = null;
            object deserialized = JsonConvert.DeserializeObject(content, command.IntermediateType);//JsonConvert.DeserializeObject<J>(content);

            if (deserialized.GetType() != command.ReturnType)
            {
                res = (APIResult)Activator.CreateInstance(command.ReturnType,
                                              BindingFlags.NonPublic | BindingFlags.Instance,
                                              null,
                                              new object[] { deserialized, SourceExchange, pair },
                                              null); // Culture?
            }

            return res;
        }

        private Type ListOfWhat(Object list)
        {
            return ListOfWhat2((dynamic)list);
        }

        private Type ListOfWhat2<T>(IList<T> list)
        {
            return typeof(T);
        }
    }
}