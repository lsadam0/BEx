using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RestSharp;

namespace BEx
{
    public class RequestFactory
    {
        private Uri BaseURI;

        public RequestFactory(Uri baseUri)
        {
            BaseURI = baseUri;
        }

        public RestRequest GetRequest(APICommand command)
        {
            if (command.RequiresAuthentication)
            {
                return CreateAuthenticatedRequest();
            }
            else
            {
                return CreateUnauthenticatedRequest(command);
            }

        }

        private RestRequest CreateAuthenticatedRequest()
        {
            return null;
        }

        private RestRequest CreateUnauthenticatedRequest(APICommand command)
        {
            var request = new RestRequest(command.ResolvedRelativeURI, command.HttpMethod);

            request.RequestFormat = DataFormat.Json;

            foreach (KeyValuePair<string, string> param in command.Parameters)
            {
                Parameter p = new Parameter();
                p.Name = param.Key;
                p.Value = param.Value;
                p.Type = ParameterType.QueryString;

                request.Parameters.Add(p);
            }

            return request;
        }


    }
}
