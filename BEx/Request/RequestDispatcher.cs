using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

using RestSharp;
using BEx.BitFinexSupport;

namespace BEx
{
    internal class RequestDispatcher
    {
        private RestClient apiClient
        {
            get;
            set;
        }

        private RestClient authenticatedClient
        {
            get;
            set;
        }

        internal RequestDispatcher(string apiUri, string authenticatedUri = null)
        {
            apiClient = new RestClient(apiUri);

            if (authenticatedUri != null)
                authenticatedClient = new RestClient(authenticatedUri);
            else
                authenticatedClient = null;
        }

        internal object ExecuteCommand<J>(RestRequest request, APICommand commandReference, Currency baseCurrency, Currency counterCurrency)
        {
            IRestResponse response;

            if (commandReference.RequiresAuthentication && authenticatedClient != null)
                response = authenticatedClient.Execute(request);
            else
                response = apiClient.Execute(request);


            if (!commandReference.ReturnsValueType)
                return (APIResult)DeserializeObject<J>(response.Content, commandReference, baseCurrency, counterCurrency);
            else
                return GetValueType<J>(response.Content);
        }

        private object GetValueType<J>(string content)
        {
            object deserialized = JsonConvert.DeserializeObject<J>(content);
            return (J)deserialized;
        }

        private APIResult DeserializeObject<J>(string content, APICommand commandReference, Currency baseCurrency, Currency counterCurrency)
        {
            APIResult res = default(APIResult);

            object deserialized = JsonConvert.DeserializeObject<J>(content);

            MethodInfo conversionMethod = deserialized.GetType().GetMethod("ConvertToStandard");

            if (conversionMethod == null)
            {
                conversionMethod = ListOfWhat(deserialized).GetMethod("ConvertListToStandard");
                res = (APIResult)conversionMethod.Invoke(null, new object[] { deserialized, baseCurrency, counterCurrency });
            }
            else
                res = (APIResult)conversionMethod.Invoke(deserialized, new object[] { baseCurrency, counterCurrency });

            return res;
        }

        private string HandleResponseError(string content, APICommand toExecute)
        {

            APIError error = default(APIError);
            return null;

        }


        // Support finding the Type of List<T>
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
