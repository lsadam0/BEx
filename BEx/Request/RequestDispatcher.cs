using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BEx
{

    internal delegate bool IsErrorDelegate(string content);
    //internal delegate string ExtractErrorMessageDelegate(string content);
    internal delegate APIError DetermineErrorConditionDelegate(string content);

    internal class RequestDispatcher
    {

        internal IsErrorDelegate IsError;
        //internal ExtractErrorMessageDelegate ExtractErrorMessage;
        internal DetermineErrorConditionDelegate DetermineErrorCondition;

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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="J">Intermediate type</typeparam>
        /// <typeparam name="E">Expected return type</typeparam>
        /// <param name="request"></param>
        /// <param name="commandReference"></param>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        internal object ExecuteCommand<J, E>(RestRequest request, APICommand commandReference, Currency baseCurrency, Currency counterCurrency)
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
                return HandlerErrorResponse(response, request, commandReference, response.ErrorException);
            }
            else
            {
                try
                {
                    if (!commandReference.ReturnsValueType)
                    {
                        result = (APIResult)DeserializeObject<J, E>(response.Content, commandReference, baseCurrency, counterCurrency);
                    }
                    else
                        result = GetValueType<J, E>(response.Content);
                }
                catch (Exception ex)
                {
                    HandlerErrorResponse(response, request, commandReference, ex);
                }
            }

            return result;
        }

  

        private void ThrowException<E>(APIError source) where E : Exception
        {
            E exception = (E)Activator.CreateInstance(typeof(E),
                                                    BindingFlags.Public | BindingFlags.Instance,
                                                    null,
                                                    new object[] { source.Message },
                                                    null);

            ((Exception)exception).Data.Add("BExError", source);

            throw exception;
        }


        private APIError HandlerErrorResponse(IRestResponse response, RestRequest request, APICommand executedCommand, Exception inner = null)
        {
            APIError error = null;
            if (DetermineErrorCondition != null)
            {
                error = DetermineErrorCondition(response.Content);
            }

            if (error == null)
            {
                error = new APIError();
                error.Message = response.Content;
            }

            error.HttpStatus = (HttpResponseCode)(int)response.StatusCode;

            if (error.ErrorCode == BExErrorCode.InsufficientFunds)
                ThrowException<InsufficientFundsException>(error);
            else if (error.ErrorCode == BExErrorCode.Authorization)
                ThrowException<ExchangeAuthorizationException>(error);

            return error;
            /*
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            else
            {*/
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
            /*
                Exception newException = null;

                newException = CreateException(response.Content, response.Content, executedCommand, inner);

                if (newException == null)
                    newException = new Exception(response.Content, inner);

                newException.Data.Add("ResponseCode", response.StatusCode);
                newException.Data.Add("ResponseErrorMessage", response.ErrorMessage ?? "");
                newException.Data.Add("ResponseHeaders", response.Headers);

                throw newException;*/

        }

        private object GetValueType<J, E>(string content)
        {


            object deserialized = JsonConvert.DeserializeObject<J>(content);

            if (deserialized.GetType() != typeof(E))
            {
                E result = (E)Activator.CreateInstance(typeof(E),
                                              BindingFlags.NonPublic | BindingFlags.Instance,
                                              null,
                                              new object[] { deserialized },
                                              null); // Culture?

                return result;
            }
            else
            {
                return deserialized;

            }

        }

        private APIResult DeserializeObject<J, E>(string content, APICommand commandReference, Currency baseCurrency, Currency counterCurrency)
        {
            APIResult res = default(APIResult);

            dynamic deserialized = JsonConvert.DeserializeObject<J>(content);

            MethodInfo conversionMethod = deserialized.GetType().GetMethod("ConvertToStandard");

            if (conversionMethod == null)
            {
                // Json Type
                Type baseT = ListOfWhat(deserialized);

                // Get Method from ExchangeResponse
                conversionMethod = baseT.BaseType.GetMethod("ConvertListToStandard");

                MethodInfo singleConversionMethod = baseT.GetMethod("ConvertToStandard");

                Type entryType = singleConversionMethod.ReturnType;

                MethodInfo generic = conversionMethod.MakeGenericMethod(baseT, entryType);

                dynamic collection = generic.Invoke(this, new object[] { deserialized, baseCurrency, counterCurrency });


                res = (APIResult)Activator.CreateInstance(typeof(E),
                                                            BindingFlags.NonPublic | BindingFlags.Instance,
                                                            null,
                                                            new object[] { collection },
                                                            null); // Culture?


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
