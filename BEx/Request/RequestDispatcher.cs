using BEx.Request;
using RestSharp;
using System.Text.RegularExpressions;

namespace BEx
{
    internal delegate APIError DetermineErrorConditionDelegate(string content);

    internal delegate bool IsErrorDelegate(string content);

    internal class RequestDispatcher
    {
        internal DetermineErrorConditionDelegate DetermineErrorCondition;
        internal IsErrorDelegate IsError;
        private Regex ErrorMessageRegex;
        private ExchangeType SourceExchangeType;

        internal RequestDispatcher(Exchange sourceExchange)
        {
            apiClient = new RestClient(sourceExchange.BaseURI.ToString());
            SourceExchangeType = sourceExchange.ExchangeSourceType;
            ErrorMessageRegex = new Regex("\"error\"");
        }

        private RestClient apiClient
        {
            get;
            set;
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
        internal IRestResponse ExecuteCommand(RestRequest request, ExchangeCommand commandReference, CurrencyTradingPair pair)
        {
            return apiClient.Execute(request);

            /*
            bool responseIsError = false;
            if (IsError != null)
            {
                responseIsError = IsError(response.Content);
            }

            if (response.ErrorException != null || response.StatusCode != HttpStatusCode.OK || responseIsError)
            {
                return HandlerErrorResponse(response, request, response.ErrorException);
            }
            else
            {
                try
                {
                    if (!commandReference.ReturnsValueType)
                    {
                        result = (APIResult)DeserializeObject(response.Content, commandReference, pair);
                    }
                    else
                        result = GetValueType(response.Content, commandReference);
                }
                catch (Exception ex)
                {
                    HandlerErrorResponse(response, request, ex);
                }
            }

            return result;*/
        }
    }
}