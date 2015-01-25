using BEx.BitStampSupport;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BEx
{
    public class BitStamp : Exchange
    {
        private Regex errorId;

        public BitStamp(string apiKey, string secretKey, string clientId)
            : base("Bitstamp.xml")
        {
            VerifyCredentials(apiKey, secretKey, clientId);

            errorId = new Regex("^{\"error\":");// \"API key not found\"}");

            this.dispatcher.IsError += IsError;
            //this.dispatcher.ExtractErrorMessage += ExtractMessage;
            this.dispatcher.DetermineErrorCondition += DetermineErrorCondition;
        }

        private void VerifyCredentials(string apiKey, string secretKey, string clientId)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ExchangeAuthorizationException("Invalid APIKey specified.");
            else
                this.APIKey = apiKey;

            if (string.IsNullOrEmpty(secretKey))
                throw new ExchangeAuthorizationException("Invalid SecretKey specified.");
            else
                this.SecretKey = secretKey;

            if (string.IsNullOrEmpty(clientId))
                throw new ExchangeAuthorizationException("Invalid ClientId specified.");
            else
                this.ClientID = clientId;
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
                    if (error["error"] is JValue)
                    {
                        JValue v = (JValue)error["error"];

                        res.Append(v.Value.ToString());
                    }
                    else
                    {
                        IDictionary<string, JToken> errors = (JObject)error["error"];

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

            if (errorId.IsMatch(content))
            {
                res = true;
            }

            return res;
        }

        internal APIError DetermineErrorCondition(string message)
        {
            if (IsError(message))
            {
                APIError error = null;

                string errorMessage = ExtractMessage(message);

                string loweredMessage = errorMessage.ToLower();
                if (loweredMessage.Contains("check your account balance for details"))
                {
                    error = new APIError(errorMessage, BExErrorCode.InsufficientFunds);
                }
                else if (loweredMessage.Contains("api key not found") || loweredMessage.Contains("invalid signature"))
                {
                    error = new APIError(errorMessage, BExErrorCode.Authorization);
                }


                if (error == null)
                {
                    error = new APIError(message, BExErrorCode.Unknown);
                }

                return error;
            }
            else return null;

        }

        #region Authorization

        protected override void CreateSignature(RestRequest request, APICommand command, Currency baseCurrency, Currency counterCurrency, Dictionary<string, string> parameters = null)
        {

            /*Signature is a HMAC-SHA256 encoded message containing: nonce, client ID and API key. The HMAC-SHA256 code must be generated using a secret key that was generated with your API key. This code must be converted to it's hexadecimal representation (64 uppercase characters).

                Example (Python):
                message = nonce + client_id + api_key
                signature = hmac.new(API_SECRET, msg=message, digestmod=hashlib.sha256).hexdigest().upper()*/


            long _nonce = Nonce;

            string msg = string.Format("{0}{1}{2}", _nonce, ClientID, APIKey);

            string signature = "";//ByteArrayToString(SignHMACSHA256(SecretKey, StringToByteArray(msg))).ToUpper();

            using (HMACSHA256 hasher = new HMACSHA256(Encoding.ASCII.GetBytes(SecretKey)))
            {
                byte[] dta = Encoding.ASCII.GetBytes(msg);
                signature = BitConverter.ToString(hasher.ComputeHash(dta)).Replace("-", "").ToUpper();
            }

            request.AddParameter("key", Uri.EscapeUriString(APIKey));
            request.AddParameter("signature", Uri.EscapeUriString(signature));
            request.AddParameter("nonce", Uri.EscapeUriString(_nonce.ToString()));

        }

        public static byte[] SignHMACSHA256(String key, byte[] data)
        {
            HMACSHA256 hashMaker = new HMACSHA256(Encoding.ASCII.GetBytes(key));
            return hashMaker.ComputeHash(data);
        }

        #endregion

        #region Command Execution

        protected override Tick ExecuteTickCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (Tick)SendCommandToDispatcher<BitstampTickJSON, Tick>(command, baseCurrency, counterCurrency);
        }

        protected override OrderBook ExecuteOrderBookCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (OrderBook)SendCommandToDispatcher<BitstampOrderBookJSON, OrderBook>(command, baseCurrency, counterCurrency);
        }

        protected override Transactions ExecuteTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (Transactions)SendCommandToDispatcher<List<BitstampTransactionJSON>, Transactions>(command, baseCurrency, counterCurrency);
        }

        protected override AccountBalances ExecuteAccountBalanceCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (AccountBalances)SendCommandToDispatcher<BitStampAccountBalanceJSON, AccountBalances>(command, baseCurrency, counterCurrency);
        }

        protected override Order ExecuteOrderCommand(APICommand command, Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("price", price.ToString());

            return (Order)SendCommandToDispatcher<BitStampOrderConfirmationJSON, Order>(command, baseCurrency, counterCurrency, parameters);
        }

        protected override OpenOrders ExecuteGetOpenOrdersCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (OpenOrders)SendCommandToDispatcher<List<BitStampOrderConfirmationJSON>, OpenOrders>(command, baseCurrency, counterCurrency);
        }

        protected override UserTransactions ExecuteGetUserTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (UserTransactions)SendCommandToDispatcher<List<BitStampUserTransactionJSON>, UserTransactions>(command, baseCurrency, counterCurrency);
        }

        protected override bool ExecuteCancelOrderCommand(APICommand command, int id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("id", id.ToString());

            return (bool)SendCommandToDispatcher<bool, bool>(command, Currency.BTC, Currency.USD, parameters);
        }

        
        protected override DepositAddress ExecuteGetDepositAddressCommand(APICommand command, Currency toDeposit)
        {
            return (DepositAddress)SendCommandToDispatcher<string, DepositAddress>(command, Currency.BTC, Currency.USD);
        }

        protected override object ExecuteWithdrawCommand(APICommand command, Currency toWithdraw, string address, decimal amount)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("amount", amount.ToString());
            parameters.Add("address", address);

            return (string)SendCommandToDispatcher<string, string>(command, toWithdraw, Currency.None, parameters);
        }

        protected override PendingDeposits ExecutePendingDepositsCommand(APICommand command)
        {
            return (PendingDeposits)SendCommandToDispatcher<List<BitStampPendingDepositJSON>, PendingDeposits>(command, Currency.BTC, Currency.USD);
        }

        protected override PendingWithdrawals ExecutePendingWithdrawalsCommand(APICommand command)
        {
            return (PendingWithdrawals)SendCommandToDispatcher<List<BitStampPendingWithdrawalJSON>, PendingWithdrawals>(command, Currency.BTC, Currency.USD);
        }
        
        #endregion
    }
}
