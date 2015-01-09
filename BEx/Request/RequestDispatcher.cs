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
    internal delegate string ExtractErrorMessageDelegate(string content);

    internal class RequestDispatcher
    {

        internal IsErrorDelegate IsError;
        internal ExtractErrorMessageDelegate ExtractErrorMessage;

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
                HandlerErrorResponse(response, request, commandReference, response.ErrorException);
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

        private Exception CreateException(string message, string bareResponse, APICommand executedCommand, Exception inner)
        {
            Exception res;

            if (ExtractErrorMessage != null)
                bareResponse = ExtractErrorMessage(bareResponse);

            /* We need a way to ID Auth exceptions vs APIExceptions */

            switch (executedCommand.ID)
            {
                case ("BuyOrder"):
                case ("SellOrder"):
                    res = new OrderRejectedException(bareResponse, inner);
                    break;
                case ("Withdraw"):
                    res = new WithdrawalRejectedException(bareResponse, inner);
                    break;
                case ("CancelOrder"):
                    res = new CancelOrderRejectedException(bareResponse, inner);
                    break;
                default:
                    res = new Exception(message, inner);
                    break;
            }

            return res;
        }

        private void HandlerErrorResponse(IRestResponse response, RestRequest request, APICommand executedCommand, Exception inner = null)
        {

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            else
            {
                #region Old
                /*
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
                        exceptionMessage = "";
                        break;
                    case (HttpStatusCode.NotFound):
                        exceptionMessage = String.Format(ErrorMessages.RESTInvalidURL, request.Resource, executedCommand.ID);
                        break;
                    default:
                        exceptionMessage = String.Format(ErrorMessages.RESTUnhandledStatus, response.StatusCode.ToString());
                        break;
                }

                exceptionMessage += String.Format(ErrorMessages.RESTErrorResponseContent, response.Content);

                */
                #endregion

                Exception newException = null;

                newException = CreateException(response.Content, response.Content, executedCommand, inner);

                if (newException == null)
                    newException = new Exception(response.Content, inner);

                newException.Data.Add("ResponseCode", response.StatusCode);
                newException.Data.Add("ResponseErrorMessage", response.ErrorMessage ?? "");
                newException.Data.Add("ResponseHeaders", response.Headers);

                throw newException;
            }
        }

        private object GetValueType<J>(string content)
        {
            object deserialized = JsonConvert.DeserializeObject<J>(content);
            return (J)deserialized;
        }

        private APIResult DeserializeObject<J>(string content, APICommand commandReference, Currency baseCurrency, Currency counterCurrency)
        {
            APIResult res = default(APIResult);

            dynamic deserialized = JsonConvert.DeserializeObject<J>(content);

            MethodInfo conversionMethod = deserialized.GetType().GetMethod("ConvertToStandard");

            if (conversionMethod == null)
            {
                conversionMethod = ListOfWhat(deserialized).BaseType.GetMethod("ConvertListToStandard");
                res = (APIResult)conversionMethod.Invoke(null, new object[] { deserialized.Cast<ExchangeResponse>.ToList(), baseCurrency, counterCurrency });
            }
            else
                res = (APIResult)conversionMethod.Invoke(deserialized, new object[] { baseCurrency, counterCurrency });

            return res;
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
