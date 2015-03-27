// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using RestSharp;
using BEx.ExchangeEngine.Utilities;

namespace BEx.ExchangeEngine
{
    /// <summary>
    /// Factory responsible for consuming a reference ExchangeCommand object, TradingPair, and Parameters Collection
    /// and returning a complete IRestRequest object, ready for dispatch to the target exchange.
    /// </summary>
    /// <remarks>
    /// This is a static class because:
    ///     A) There is no explicit need for sharing instance members
    ///     B) A static class is slightly more performant than an instanced class
    /// </remarks>
    internal static class RequestFactory
    {

        /// <summary>
        /// Consume an ExchangeCommand, TradingPair, and Parameters Collection and return
        /// a complete IRestRequest object.
        /// </summary>
        /// <param name="command">Command Information for building the request</param>
        /// <param name="pair">Trading Pair.  If ExchangeCommand only uses a Currency value, we will use pair.BaseCurrency (e.g. GetDepositAddress)</param>
        /// <param name="parameters">Parameter values</param>
        /// <returns>IRestRequest, ready for dispatch</returns>
        public static IRestRequest GetRequest(
                                        IExchangeCommand command,
                                        CurrencyTradingPair pair,
                                        IDictionary<StandardParameter, string> parameters = null)
        {
            IRestRequest result = CreateRequest(command);

            PopulateCommandParameters(result, command, pair, parameters);

            return result;
        }

        /// <summary>
        /// Create the IRestRequest object
        /// </summary>
        /// <param name="command">Reference Command</param>
        /// <returns>Unpopulated request object</returns>
        private static IRestRequest CreateRequest(IExchangeCommand command)
        {
            return new RestRequest(command.RelativeUri, command.HttpMethod)
            {
                RequestFormat = DataFormat.Json,
                Method = command.HttpMethod
            };
        }

        /// <summary>
        /// Reconcile ExchangeCommand parameters with supplied and default values, and populate them into the IRestRequest
        /// </summary>
        /// <param name="request">Unpopulated IRestRequest</param>
        /// <param name="command">Reference Command</param>
        /// <param name="pair">Trading Pair</param>
        /// <param name="values">Explicitly supplied parameter values</param>
        private static void PopulateCommandParameters(IRestRequest request, IExchangeCommand command, CurrencyTradingPair pair, IDictionary<StandardParameter, string> values)
        {
            foreach (var param in command.Parameters)
            {
                ExchangeParameter parameter = param.Value;
                string value;
                switch (parameter.StandardParameterIdentifier)
                {
                    case StandardParameter.Price:
                    case StandardParameter.Amount:
                    case StandardParameter.Id:
                    case StandardParameter.Timestamp:
                    case StandardParameter.Limit:
                        value = values[parameter.StandardParameterIdentifier];
                        break;
                    case StandardParameter.Base:
                        value = pair.BaseCurrency.ToString();
                        break;

                    case StandardParameter.Counter:
                        value = pair.CounterCurrency.ToString();
                        break;

                    case StandardParameter.Currency:
                        value = pair.BaseCurrency.ToString();
                        break;

                    case StandardParameter.CurrencyFullName:
                        value = pair.BaseCurrency.GetDescription();
                        break;
                    case StandardParameter.Pair:
                        value = pair.ToString();
                        break;
                    case StandardParameter.UnixTimestamp:
                        value = DateTime.UtcNow.AddHours(-1).ToUnixTime().ToStringInvariant();
                        break;
                    case StandardParameter.None:
                        value = parameter.DefaultValue;
                        break;
                    default:
                        value = string.Empty;
                        break;
                }
                
                ParameterType pType = ParameterType.GetOrPost;

                if (parameter.ParameterMethod == ParameterMethod.Url)
                    pType = ParameterType.UrlSegment;
                else if (parameter.ParameterMethod == ParameterMethod.QueryString)
                    pType = ParameterType.QueryString;

                request.AddParameter(parameter.ExchangeParameterName, value, pType);
            }

        }
    }
}