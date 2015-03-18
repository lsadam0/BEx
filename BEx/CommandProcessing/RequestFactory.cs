using RestSharp;
using System;
using System.Collections.Generic;

namespace BEx.CommandProcessing
{
    internal class RequestFactory
    {
        internal RequestFactory()
        {
        }

        public RestRequest GetRequest(ExchangeCommand command,
                                        CurrencyTradingPair pair,
                                        Dictionary<StandardParameterType, string> parameters = null)
        {
            RestRequest result = CreateRequest(command, pair);

            parameters = PopulateCommandParameters(command, pair, parameters);

            return result;
        }

        private RestRequest CreateRequest(ExchangeCommand command, CurrencyTradingPair pair)
        {
            var request = new RestRequest(command.GetResolvedRelativeURI(pair), command.HttpMethod);

            request.RequestFormat = DataFormat.Json;
            request.Method = command.HttpMethod;

            return request;
        }

        private Dictionary<StandardParameterType, string> PopulateCommandParameters(ExchangeCommand command, CurrencyTradingPair pair, Dictionary<StandardParameterType, string> values)
        {
            var res = new Dictionary<StandardParameterType, string>();

            if (command.DependentParameters.Count > 0)
            {
                foreach (KeyValuePair<StandardParameterType, ExchangeParameter> param in command.DependentParameters)
                {
                    string value = "";
                    switch (param.Key)
                    {
                        case (StandardParameterType.Amount):
                            value = values[param.Key];
                            break;

                        case (StandardParameterType.Base):
                            value = pair.BaseCurrency.ToString();
                            break;

                        case (StandardParameterType.Counter):
                            value = pair.CounterCurrency.ToString();
                            break;

                        case (StandardParameterType.Currency):
                            value = pair.BaseCurrency.ToString();
                            break;

                        case (StandardParameterType.CurrencyFullName):
                            value = pair.BaseCurrency.GetDescription();
                            break;

                        case (StandardParameterType.Id):
                            value = values[StandardParameterType.Id];
                            break;

                        case (StandardParameterType.Pair):
                            value = pair.ToString();
                            break;

                        case (StandardParameterType.Price):
                            value = values[param.Key];
                            break;

                        case (StandardParameterType.TimeStamp):
                            throw new NotImplementedException();

                        case (StandardParameterType.UnixTimeStamp):
                            value = UnixTime.DateTimeToUnixTimestamp(DateTime.Now.AddHours(-2)).ToString();
                            break;
                    }

                    if (param.Value.IsLowerCase)
                        res.Add(param.Key, value.ToLower());
                    else
                        res.Add(param.Key, value);
                }
            }

            return res;
        }
    }
}