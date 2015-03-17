using RestSharp;

namespace BEx.CommandProcessing
{
    internal delegate APIError DetermineErrorConditionDelegate(string content);

    internal delegate bool IsErrorDelegate(string content);

    internal class RequestDispatcher
    {
        private Exchange SourceExchange;

        internal DetermineErrorConditionDelegate DetermineErrorCondition;
        internal IsErrorDelegate IsError;

        internal RequestDispatcher(Exchange sourceExchange)
        {
            SourceExchange = sourceExchange;
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
            var client = new RestClient(SourceExchange.Configuration.Url);

            if (commandReference.IsAuthenticated)
                client.Authenticator = SourceExchange.Authenticator;

            return client.Execute(request);
        }
    }
}