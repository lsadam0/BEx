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
            var request = new RestRequest((new Uri(this.BaseURI, command.ResolvedRelativeURI)).ToString(), command.HttpMethod);

            return request;
        }
    }
}
