using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BEx.Request
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
            return DeserializeObject(source, executedCommand, pair);
        }

        /// <summary>
        /// boxing
        /// </summary>
        public object TranslateValueType(string source, ExchangeCommand executedCommand)
        {
            return GetValueType(source, executedCommand);
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

        private object GetValueType(string content, ExchangeCommand command)
        {
            object deserialized = JsonConvert.DeserializeObject(content, command.IntermediateType);//JsonConvert.DeserializeObject<J>(content);

            if (deserialized.GetType() != command.ReturnType)
            {
                object result = Activator.CreateInstance(command.ReturnType,
                                              BindingFlags.NonPublic | BindingFlags.Instance,
                                              null,
                                              new object[] { deserialized, SourceExchange },
                                              null); // Culture?

                return result;
            }
            else
            {
                return deserialized;
            }
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