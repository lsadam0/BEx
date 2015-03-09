using BEx.Request;
using RestSharp;

namespace BEx
{
    internal delegate APIError DetermineErrorConditionDelegate(string content);

    internal delegate bool IsErrorDelegate(string content);

    internal class RequestDispatcher
    {
        internal DetermineErrorConditionDelegate DetermineErrorCondition;
        internal IsErrorDelegate IsError;

        private ExchangeType SourceExchangeType;

        internal RequestDispatcher(Exchange sourceExchange)
        {
            apiClient = new RestClient(sourceExchange.BaseURI.ToString());
            SourceExchangeType = sourceExchange.ExchangeSourceType;
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
        internal IRestResponse Dispatch(RestRequest request, ExchangeCommand commandReference, CurrencyTradingPair pair)
        {
            return apiClient.Execute(request);
        }
    }
}