using BEx.BitFinexSupport;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BEx
{
    public class Bitfinex : Exchange
    {
        public Bitfinex(string apiKey, string secret)
            : base("BitFinex.xml")
        {
            VerifyCredentials(apiKey, secret);

            this.dispatcher.DetermineErrorCondition += DetermineErrorCondition;
        }

        private void VerifyCredentials(string apiKey, string secretKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ExchangeAuthorizationException("Invalid APIKey specified.");
            else
                this.APIKey = apiKey;

            if (string.IsNullOrEmpty(secretKey))
                throw new ExchangeAuthorizationException("Invalid SecretKey specified.");
            else
                this.SecretKey = secretKey;
        }

        internal string ExtractMessage(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                StringBuilder res = new StringBuilder();
                // this works for auth errors
                JObject error = JObject.Parse(content);

                // for other errors

                try
                {
                    if (error["message"] is JValue)
                    {
                        JValue v = (JValue)error["message"];

                        res.Append(v.Value.ToString());
                    }
                    else
                    {
                        IDictionary<string, JToken> errors = (JObject)error["message"];

                        foreach (KeyValuePair<string, JToken> er in errors)
                        {
                            foreach (JToken token in er.Value.Values())
                            {
                                res.Append(((JValue)token).Value.ToString());
                            }

                            //res.Append(er.Value.ToString().Replace("{\"error\":", "").Replace("{\"__all__\": [\"", "").Replace("\"]}}", "").Replace("[", "").Replace("]", "").Replace("\"", "").Trim());
                        }
                    }
                }
                catch (Exception)
                {
                    res.Append(error.ToString());
                }

                return res.ToString();//Regex.Replace(res.ToString(), @"\t|\n|\r", "");
            }
            else
                return "The Error response was empty";
        }

        internal bool IsError(string content)
        {
            bool res = false;

            //if (errorId.IsMatch(content))
            //{
            //  res = true;
            //}

            return res;
        }

        internal APIError DetermineErrorCondition(string message)
        {
            APIError error = null;

            string errorMessage = ExtractMessage(message);

            string loweredMessage = errorMessage.ToLower();
            if (loweredMessage.Contains("not enough balance"))
            {
                error = new APIError(errorMessage, BExErrorCode.InsufficientFunds);
            }
            else if (loweredMessage.Contains("the given x-bfx-apikey") || loweredMessage.Contains("invalid x-bfx-signature"))
            {
                error = new APIError(errorMessage, BExErrorCode.Authorization);
            }

            if (error == null)
            {
                error = new APIError(message, BExErrorCode.Unknown);
            }

            return error;
        }

        #region Authorization

        protected override void CreateSignature(RestRequest request, APICommand command, Currency baseCurrency, Currency counterCurrency, Dictionary<string, string> parameters = null)
        {
            /*POST https://api.bitfinex.com/v1/order/new
                With a payload of
                {
                "request": "/v1/order/new",
                "nonce": "1234",
                "option1": ...
                }
                The nonce provided must be strictly increasing.

                To authenticate a request, use the following:

                payload = parameters-dictionary -> JSON encode -> base64
                signature = HMAC-SHA384(payload, api-secret) as hexadecimal
                send (api-key, payload, signature)
                These are encoded as HTTP headers named:
                X-BFX-APIKEY
                X-BFX-PAYLOAD
                X-BFX-SIGNATURE*/
            request.AddHeader("X-BFX-APIKEY", APIKey);

            StringBuilder payload = new StringBuilder();

            payload.Append("{");
            payload.Append("\"request\": \"" + command.GetResolvedRelativeURI(baseCurrency, counterCurrency) + "\",");
            payload.Append("\"nonce\": \"" + Nonce + "\"");

            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> pair in parameters)
                {
                    payload.Append(",");
                    payload.Append("\"" + pair.Key + "\": \"" + pair.Value + "\"");
                }
            }

            payload.Append("}");

            string payload64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload.ToString()));

            request.AddHeader("X-BFX-PAYLOAD", payload64);

            using (HMACSHA384 hasher = new HMACSHA384(Encoding.UTF8.GetBytes(SecretKey)))
            {
                byte[] hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(payload64));
                request.AddHeader("X-BFX-SIGNATURE", BitConverter.ToString(hashBytes).Replace("-", "").ToLower());
            }
        }

        #endregion Authorization

        #region Command Execution

        protected override Tick ExecuteTickCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (Tick)SendCommandToDispatcher<BitfinexTickJSON, Tick>(command, baseCurrency, counterCurrency);
        }

        protected override OrderBook ExecuteOrderBookCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (OrderBook)SendCommandToDispatcher<BitFinexOrderBookJSON, OrderBook>(command, baseCurrency, counterCurrency);
        }

        protected override Transactions ExecuteTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (Transactions)SendCommandToDispatcher<List<BitFinexTransactionJSON>, Transactions>(command, baseCurrency, counterCurrency);
        }

        protected override AccountBalances ExecuteAccountBalanceCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (AccountBalances)SendCommandToDispatcher<List<BitFinexAccountBalanceJSON>, AccountBalances>(command, baseCurrency, counterCurrency);
        }

        protected override Order ExecuteOrderCommand(APICommand command, Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("symbol", baseCurrency.ToString() + counterCurrency.ToString());
            parameters.Add("amount", amount.ToString());
            parameters.Add("price", price.ToString());
            parameters.Add("exchange", "bitfinex");

            if (command.ID == "BuyOrder")
                parameters.Add("side", "buy");
            else
                parameters.Add("side", "sell");

            parameters.Add("type", "exchange limit");
            //parameters.Add("is_hidden", "0");

            return (Order)SendCommandToDispatcher<BitFinexOrderResponseJSON, Order>(command, baseCurrency, counterCurrency, parameters);
        }

        protected override OpenOrders ExecuteGetOpenOrdersCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (OpenOrders)SendCommandToDispatcher<List<BitFinexOrderResponseJSON>, OpenOrders>(command, baseCurrency, counterCurrency);
        }

        protected override UserTransactions ExecuteGetUserTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("symbol", baseCurrency.ToString().ToUpper() + counterCurrency.ToString().ToUpper());

            return (UserTransactions)SendCommandToDispatcher<List<BitFinexUserTransactionJSON>, UserTransactions>(command, baseCurrency, counterCurrency, parameters);
        }

        protected override bool ExecuteCancelOrderCommand(APICommand command, int id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("order_id", id.ToString());

            return (bool)SendCommandToDispatcher<object, bool>(command, Currency.BTC, Currency.USD, parameters);
        }

        protected override DepositAddress ExecuteGetDepositAddressCommand(APICommand command, Currency toDeposit)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("currency", toDeposit.ToString());
            parameters.Add("method", EnumExtensions.GetDescription(toDeposit).ToLower());
            parameters.Add("wallet_name", "exchange");

            return (DepositAddress)SendCommandToDispatcher<BitFinexDepositAddressJSON, DepositAddress>(command, toDeposit, toDeposit, parameters);
        }

        protected override object ExecuteWithdrawCommand(APICommand command, Currency toWithdraw, string address, decimal amount)
        {
            throw new NotImplementedException("BitFinx cannot execute withdrawals");
        }

        protected override PendingDeposits ExecutePendingDepositsCommand(APICommand command)
        {
            throw new NotImplementedException("Get Pending Deposits is not implemented");
        }

        protected override PendingWithdrawals ExecutePendingWithdrawalsCommand(APICommand command)
        {
            throw new NotImplementedException("Get Pending Withdrawals is not implemented");
        }

        #endregion Command Execution
    }
}