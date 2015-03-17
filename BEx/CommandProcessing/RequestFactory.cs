using RestSharp;
using System;
using System.Collections.Generic;

namespace BEx.CommandProcessing
{
    public delegate void GetSignatureDelegate(RestRequest request,
                                                ExchangeCommand command,
                                                CurrencyTradingPair pair,
                                                Dictionary<string, string> parameters = null);

    internal class RequestFactory
    {
        public GetSignatureDelegate GetSignature;

        internal RequestFactory()
        {
        }

        public RestRequest GetRequest(ExchangeCommand command,
                                        CurrencyTradingPair pair,
                                        Dictionary<StandardParameterType, string> parameters = null)
        {
            RestRequest result = CreateRequest(command, pair);

            parameters = PopulateCommandParameters(command, pair, parameters);

            Dictionary<string, string> finalParameters;

            if (parameters.Count > 0)
                finalParameters = SetParameters(result, command, pair, parameters);
            else
                finalParameters = new Dictionary<string, string>();

            /*
            if (command.IsAuthenticated)
            {
                AuthenticateRequest(result, command, pair, finalParameters);
            }*/

            return result;
        }

        private void AuthenticateRequest(RestRequest request, ExchangeCommand command, CurrencyTradingPair pair, Dictionary<string, string> parameters = null)
        {
            if (GetSignature != null)
            {
                GetSignature(request, command, pair, parameters);
            }
        }

        private RestRequest CreateRequest(ExchangeCommand command, CurrencyTradingPair pair)
        {
            var request = new RestRequest(command.GetResolvedRelativeURI(pair), command.HttpMethod);

            request.RequestFormat = DataFormat.Json;
            request.Method = command.HttpMethod;

            return request;
        }

        private Dictionary<string, string> SetParameters(RestRequest request, ExchangeCommand command, CurrencyTradingPair pair, Dictionary<StandardParameterType, string> parameters)
        {
            Dictionary<string, string> reconciled = new Dictionary<string, string>();
            foreach (KeyValuePair<StandardParameterType, string> param in parameters)
            {
                string exchangeParamName = command.DependentParameters[param.Key].ExchangeParameterName;

                request.AddParameter(exchangeParamName, Uri.EscapeUriString(param.Value));
                reconciled.Add(exchangeParamName, param.Value);
            }

            foreach (KeyValuePair<string, ExchangeParameter> param in command.DefaultParameters)
            {
                request.AddParameter(param.Value.ExchangeParameterName, param.Value.DefaultValue);
                reconciled.Add(param.Value.ExchangeParameterName, param.Value.DefaultValue);
            }

            return reconciled;
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
                            //res.Add(param.Key, values[param.Key]);
                            value = values[param.Key];
                            break;

                        case (StandardParameterType.Base):
                            //res.Add(param.Key, pair.BaseCurrency.ToString());
                            value = pair.BaseCurrency.ToString();
                            break;

                        case (StandardParameterType.Counter):
                            //res.Add(param.Key, pair.CounterCurrency.ToString());
                            value = pair.CounterCurrency.ToString();
                            break;

                        case (StandardParameterType.Currency):
                            // res.Add(param.Key, pair.BaseCurrency.ToString());
                            value = pair.BaseCurrency.ToString();
                            break;

                        case (StandardParameterType.CurrencyFullName):
                            //res.Add(param.Key, pair.BaseCurrency.ToString());
                            value = pair.BaseCurrency.GetDescription();
                            break;

                        case (StandardParameterType.Id):
                            //res.Add(param.Key, values[StandardParameterType.Id]);
                            value = values[StandardParameterType.Id];
                            break;

                        case (StandardParameterType.Pair):
                            //res.Add(param.Key, pair.ToString());
                            value = pair.ToString();
                            break;

                        case (StandardParameterType.Price):
                            //res.Add(param.Key, values[param.Key]);
                            value = values[param.Key];
                            break;

                        case (StandardParameterType.TimeStamp):
                            throw new NotImplementedException();

                        case (StandardParameterType.UnixTimeStamp):
                            //res.Add(param.Key, Common.UnixTime.DateTimeToUnixTimestamp(DateTime.Now.AddHours(-1)).ToString());
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