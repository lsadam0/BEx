using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BEx.CommandProcessing
{
    internal class RequestFactory
    {
        internal RequestFactory()
        {
        }

        public static RestRequest GetRequest(ExchangeCommand command,
                                        CurrencyTradingPair pair,
                                        Dictionary<StandardParameterType, string> parameters = null)
        {
            RestRequest result = CreateRequest(command, pair);

            parameters = PopulateCommandParameters(command, pair, parameters);

            SetParameters(result, command, pair, parameters);
            return result;
        }

        private static void SetParameters(RestRequest request, ExchangeCommand command, CurrencyTradingPair pair, Dictionary<StandardParameterType, string> parameters)
        {
            foreach (KeyValuePair<StandardParameterType, string> param in parameters)
            {
                string exchangeParamName = command.DependentParameters[param.Key].ExchangeParameterName;
                request.AddParameter(exchangeParamName, Uri.EscapeUriString(param.Value));
            }

            foreach (KeyValuePair<string, ExchangeParameter> param in command.DefaultParameters)
            {
                request.AddParameter(param.Value.ExchangeParameterName, param.Value.DefaultValue);
            }
        }

        private static RestRequest CreateRequest(ExchangeCommand command, CurrencyTradingPair pair)
        {
            var request = new RestRequest(command.GetResolvedRelativeUri(pair), command.HttpMethod);

            request.RequestFormat = DataFormat.Json;
            request.Method = command.HttpMethod;

            return request;
        }

        private static Dictionary<StandardParameterType, string> PopulateCommandParameters(ExchangeCommand command, CurrencyTradingPair pair, Dictionary<StandardParameterType, string> values)
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

                        case (StandardParameterType.Timestamp):
                            throw new NotImplementedException();

                        case (StandardParameterType.UnixTimestamp):
                            value = UnixTime.DateTimeToUnixTimestamp(DateTime.Now.AddHours(-2)).ToString(CultureInfo.InvariantCulture);
                            break;
                    }

                    if (param.Value.IsLowercase)
                        res.Add(param.Key, value.ToLowerInvariant());
                    else
                        res.Add(param.Key, value);
                }
            }

            return res;
        }
    }
}