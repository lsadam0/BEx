using RestSharp;
using System;
using System.Collections.Generic;

namespace BEx.Request
{
    public delegate void GetSignatureDelegate(RestRequest request, ExchangeCommand command, CurrencyTradingPair pair, Dictionary<string, string> parameters = null);

    internal class RequestFactory
    {
        public GetSignatureDelegate GetSignature;

        internal RequestFactory()
        {
        }

        public RestRequest GetRequest(ExchangeCommand command)
        {
            return null;
        }

        public RestRequest GetRequest(ExchangeCommand command, CurrencyTradingPair pair, Dictionary<string, string> parameters = null)
        {
            RestRequest result = CreateRequest(command, pair);

            if (parameters != null && parameters.Count > 0)
                SetParameters(result, command, pair, parameters);

            if (command.IsAuthenticated)
                AuthenticateRequest(result, command, pair, parameters);

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

        private void SetParameters(RestRequest request, ExchangeCommand command, CurrencyTradingPair pair, Dictionary<string, string> parameters)
        {
            foreach (KeyValuePair<string, string> param in parameters)
            {
                request.AddParameter(param.Key, Uri.EscapeUriString(param.Value.ToString()));
            }
        }
    }
}