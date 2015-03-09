using RestSharp;
using System;
using System.Collections.Generic;

namespace BEx.Request
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

            Dictionary<string, string> reconciledParams = new Dictionary<string, string>();

            if (parameters != null && parameters.Count > 0)
                reconciledParams = SetParameters(result, command, pair, parameters);

            if (command.IsAuthenticated)
                AuthenticateRequest(result, command, pair, reconciledParams);

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
    }
}