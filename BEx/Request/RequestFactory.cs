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

            return result;
        }

        private RestRequest CreateRequest(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            //command.GetResolvedRelativeURI()
            var request = new RestRequest(command.GetResolvedRelativeURI(baseCurrency, counterCurrency), command.HttpMethod);

            request.RequestFormat = DataFormat.Json;
            request.Method = command.HttpMethod;

            /* Bad
            foreach (KeyValuePair<string, string> param in command.Parameters)
            {
                Parameter p = new Parameter();
                p.Name = param.Key;
                p.Value = param.Value;
                p.Type = ParameterType.QueryString;

                request.Parameters.Add(p);
            }*/

            return request;
        }

        private void AuthenticateRequest(RestRequest request, APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            if (GetSignature != null)
            {
                //GetSignature()

                GetSignature(request, command, baseCurrency, counterCurrency);

                //request.AddParameter("key", result.Item1);
                //request.AddParameter("nonce", result.Item2);
                //request.AddParameter("signature", result.Item3);

            }
        }

    }
}
