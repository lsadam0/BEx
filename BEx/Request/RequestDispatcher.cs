using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;


using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

using RestSharp;
using BEx.BitFinexSupport;

using BEx.BitStampSupport;

namespace BEx
{
 
    internal delegate bool IsErrorDelegate(string content);
    internal delegate Exception CreateExceptionDelegate(string message, string bareResponse, APICommand executedCommand, Exception inner = null);

    internal class RequestDispatcher
    {

 
        internal IsErrorDelegate IsError;
        internal CreateExceptionDelegate CreateException;

        private Regex ErrorMessageRegex;


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

            ErrorMessageRegex = new Regex("\"error\"");
        }

        internal object ExecuteCommand<J>(RestRequest request, APICommand commandReference, Currency baseCurrency, Currency counterCurrency)
        {
            IRestResponse response;
            object result = null;


            if (commandReference.RequiresAuthentication && authenticatedClient != null)
                response = authenticatedClient.Execute(request);
            else
                response = apiClient.Execute(request);


            bool responseIsError = false;
            if (IsError != null)
            {
                responseIsError = IsError(response.Content);
            }

            if (response.ErrorException != null || response.StatusCode != HttpStatusCode.OK || responseIsError)
            {
                HandlerErrorResponse(response, request, commandReference);
            }
            else
            {
                try
                {
                    if (!commandReference.ReturnsValueType)
                    {
                        result = (APIResult)DeserializeObject<J>(response.Content, commandReference, baseCurrency, counterCurrency);
                    }
                    else
                        result = GetValueType<J>(response.Content);
                }
                catch (Exception ex)
                {
                    HandlerErrorResponse(response, request, commandReference, ex);
                }
            }

            return result;
        }
        /*
        private Exception CreateException(string message, string bareResponse, APICommand executedCommand, Exception inner = null)
        {
            Exception res;

            switch (executedCommand.ID)
            {
                case ("BuyOrder"):
                case ("SellOrder"):

                    Regex isInsufficient = new Regex("\"You have only");

                    if (isInsufficient.IsMatch(bareResponse))
                    {
                        res = new InsufficientFundsException(message, inner);
                    }
                    else
                        res = new OrderRejectedException(message, inner);
                    break;
                default:
                    res = new Exception(message, inner);
                    break;
            }


            return res;
        }*/

        private void HandlerErrorResponse(IRestResponse response, RestRequest request, APICommand executedCommand, Exception inner = null)
        {

            string exceptionMessage = "";
            switch (response.StatusCode)
            {
                case (HttpStatusCode.BadRequest):
                    exceptionMessage = String.Format(ErrorMessages.RESTBadRequest, executedCommand.ID);
                    break;
                case (HttpStatusCode.Unauthorized):
                    exceptionMessage = String.Format(ErrorMessages.RESTUnauthorized, executedCommand.ID);
                    break;
                case (HttpStatusCode.Forbidden):
                    exceptionMessage = String.Format(ErrorMessages.RESTForbidden, executedCommand.ID);
                    break;
                case (HttpStatusCode.MethodNotAllowed):
                    exceptionMessage = String.Format(ErrorMessages.RESTMethodNotAllowed, executedCommand.ID, request.Method.ToString());
                    break;
                case (HttpStatusCode.RequestTimeout):
                    exceptionMessage = String.Format(ErrorMessages.RESTRequestTimeout, executedCommand.ID);
                    break;
                case (HttpStatusCode.RequestUriTooLong):
                    exceptionMessage = String.Format(ErrorMessages.RESTURITooLong, executedCommand.ID);
                    break;
                case (HttpStatusCode.InternalServerError):
                    exceptionMessage = String.Format(ErrorMessages.RESTInternalServerError, executedCommand.ID);
                    break;
                case (HttpStatusCode.ServiceUnavailable):
                    exceptionMessage = String.Format(ErrorMessages.RESTServiceUnavailable, executedCommand.ID);
                    break;
                case (HttpStatusCode.OK):
                    exceptionMessage = String.Format(ErrorMessages.RESTSuccessButHasException, executedCommand.ID);
                    break;
                case (HttpStatusCode.NotFound):
                    exceptionMessage = String.Format(ErrorMessages.RESTInvalidURL, request.Resource, executedCommand.ID);
                    break;
                default:
                    exceptionMessage = String.Format(ErrorMessages.RESTUnhandledStatus, response.StatusCode.ToString());
                    break;
            }

            // append server response

            exceptionMessage += String.Format(ErrorMessages.RESTErrorResponseContent, response.Content);

            Exception newException = null;

            if (CreateException != null)
            {
                if (response.ErrorException != null)
                {
                    newException = CreateException(exceptionMessage, response.Content, executedCommand, response.ErrorException);  //new Exception(exceptionMessage + " " + ErrorMessages.RESTCheckInnerException, response.ErrorException);
                }
                else
                {
                    newException = CreateException(exceptionMessage, response.Content, executedCommand);
                }
            }
            

            if (newException == null)
            {
                if (response.ErrorException != null)
                {
                    newException = new Exception(exceptionMessage, response.ErrorException);
                }
                else
                {
                    newException = new Exception(exceptionMessage);
                }
            }

            throw newException;
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
