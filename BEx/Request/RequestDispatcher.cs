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
    internal class RequestDispatcher<J>
    {
        private RestClient apiClient
        {
            get;
            set;
        }

        private RequestFactory apiRequestFactory
        {
            get;
            set;
        }

        internal RequestDispatcher(RestClient client, RequestFactory factory)
        {
            apiClient = client;
            apiRequestFactory = factory;
        }

        internal APIResult ExecuteCommand(APICommand toExecute)
        {
            RestRequest request = apiRequestFactory.GetRequest(toExecute);

            IRestResponse response = apiClient.Execute(request);

            return (APIResult)DeserializeObject(response.Content, toExecute);
        }

        private APIResult DeserializeObject(string content, APICommand toExecute)
        {
            APIResult res = default(APIResult);

            object deserialized = JsonConvert.DeserializeObject<J>(content);

            MethodInfo conversionMethod = deserialized.GetType().GetMethod("ConvertToStandard");

            if (conversionMethod == null)
            {
                conversionMethod = ListOfWhat(deserialized).GetMethod("ConvertListToStandard");
                res = (APIResult)conversionMethod.Invoke(null, new object[] { deserialized, toExecute.BaseCurrency, toExecute.CounterCurrency });
            }
            else
                res = (APIResult)conversionMethod.Invoke(deserialized, new object[] { toExecute.BaseCurrency, toExecute.CounterCurrency });

            return res;
        }

        private string HandleResponseError(string content, APICommand toExecute)
        {

            APIError error = default(APIError);
            return null;
            //error = JsonConvert.DeserializeObject<
            /*
            WebException toThrow;

            if (response.ErrorException != null)
                toThrow = new WebException(response.StatusCode.ToString(), response.ErrorException);
            else
                toThrow = new WebException(response.StatusCode.ToString());

            throw toThrow;

            return "";*/
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
