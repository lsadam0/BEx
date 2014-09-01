using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

using RestSharp;

namespace BEx
{

    public delegate void GetSignatureDelegate(RestRequest request, APICommand command, Currency baseCurrency, Currency counterCurrency, Dictionary<string, string> parameters = null);

    public class RequestFactory
    {
        public GetSignatureDelegate GetSignature;

        public RequestFactory()
        {
        }

        public RestRequest GetRequest(APICommand command, Currency baseCurrency, Currency counterCurrency, Dictionary<string, string> parameters = null)
        {

            RestRequest result = CreateRequest(command, baseCurrency, counterCurrency);

            if (command.RequiresAuthentication)
                AuthenticateRequest(result, command, baseCurrency, counterCurrency);

            if (parameters != null && parameters.Count > 0)
                SetParameters(result, command, baseCurrency, counterCurrency, parameters);

            return result;
        }

        private RestRequest CreateRequest(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            var request = new RestRequest(command.GetResolvedRelativeURI(baseCurrency, counterCurrency), command.HttpMethod);

            request.RequestFormat = DataFormat.Json;
            request.Method = command.HttpMethod;

            return request;
        }

        private void AuthenticateRequest(RestRequest request, APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            if (GetSignature != null)
            {
                GetSignature(request, command, baseCurrency, counterCurrency);
            }
        }

        private void SetParameters(RestRequest request, APICommand command, Currency baseCurrency, Currency counterCurrency, Dictionary<string, string> parameters)
        {
            // BitStamp
            foreach (KeyValuePair<string, string> param in parameters)
            {
                request.AddParameter(param.Key, Uri.EscapeUriString(param.Value.ToString()));
            }
        }

    }
}
