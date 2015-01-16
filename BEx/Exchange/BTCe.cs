using BEx.BTCeSupport;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;

namespace BEx
{
    public class BTCe : Exchange
    {

        private Regex errorId;
        /// <summary>
        /// Nonce computation
        /// </summary>
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public BTCe(string apiKey, string secretKey)
            : base("BTCe.xml")
        {
            VerifyCredentials(apiKey, secretKey);

            errorId = new Regex("^{\"success\":0");
            foreach (APICommand command in APICommandCollection.Values)
            {
                command.CurrencyFormatter += FormatCurrency;
            }

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
                else if (loweredMessage.Contains("invalid sign") || loweredMessage.Contains("invalid signature"))
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


        /// <summary>
        /// BTCe expects lower case currency abbreviations
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        private string FormatCurrency(string currency)
        {
            return currency.ToLower();
        }

        private string FormatPair(Currency baseC, Currency counterC)
        {
            return string.Format("{0}_{1}", baseC.ToString().ToLower(), counterC.ToString().ToLower());
        }
        #region Authorization

        protected override void CreateSignature(RestRequest request, APICommand command, Currency baseCurrneyc, Currency counterCurrency, Dictionary<string, string> parameters = null)
        {
            long _nonce = BTCeNonce;

            StringBuilder dataBuilder = new StringBuilder();

            // pair type rate amount
            request.AddParameter("method", "Trade");
            request.AddParameter("pair", "btc_usd");
            request.AddParameter("type", "sell");
            request.AddParameter("rate", "300");
            request.AddParameter("amount", "1");
            request.AddParameter("nonce", _nonce);

            //string postString = "method=getInfo&nonce=" + _nonce.ToString();

            string postString = "?";// = "?method=Trade";

            
            StringBuilder s = new StringBuilder();

    

            foreach (var item in request.Parameters)
            {

                s.AppendFormat("{0}={1}", item.Name, HttpUtility.UrlEncode(item.Value.ToString()));
                s.Append("&");
            }
            if (s.Length > 0) s.Remove(s.Length - 1, 1);

            postString += s.ToString();


            // postString += ("nonce=" + _nonce.ToString());

            string signature;
            using (HMACSHA512 hasher = new HMACSHA512(Encoding.ASCII.GetBytes(SecretKey)))
            {
                byte[] hashBytes = hasher.ComputeHash(Encoding.ASCII.GetBytes(postString));
                signature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
            // Header
            // Key

            request.AddHeader("Key", APIKey);
            // Sign
            request.AddHeader("Sign", signature);

            // Parameters
           // request.AddParameter("method", "getInfo");
            //request.AddParameter("nonce", _nonce.ToString());
        }

        private UInt32 BTCeNonce
        {
            get
            {
                return (UInt32)(DateTime.Now - epoch).TotalSeconds;
            }
        }

        #endregion

        #region Command Execution

        protected override Tick ExecuteTickCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (Tick)SendCommandToDispatcher<BTCeTickJSON, Tick>(command, baseCurrency, counterCurrency);
        }

        protected override OrderBook ExecuteOrderBookCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (OrderBook)SendCommandToDispatcher<BTCeOrderBookJSON, OrderBook>(command, baseCurrency, counterCurrency);
        }

        protected override Transactions ExecuteTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (Transactions)SendCommandToDispatcher<List<BTCeTransactionsJSON>, Transactions>(command, baseCurrency, counterCurrency);
        }

        protected override AccountBalances ExecuteAccountBalanceCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (AccountBalances)SendCommandToDispatcher<BTCeAccountBalanceJSON, AccountBalances>(command, baseCurrency, counterCurrency);
        }

        protected override Order ExecuteOrderCommand(APICommand command, Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price)
        {


            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("rate", amount.ToString());
            parameters.Add("pair", FormatPair(baseCurrency, counterCurrency));
            parameters.Add("type", command.Parameters["type"]);


            /*        <arg ID="pair" type="parameter" />
        <arg ID="type" type="parameter" />
        <arg ID="rate" type="parameter" />
        <arg ID="amount" type="parameter" />
             */
            return (Order)SendCommandToDispatcher<object, Order>(command, baseCurrency, counterCurrency, parameters);
        }

        protected override OpenOrders ExecuteGetOpenOrdersCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            throw new NotImplementedException("BTCe cannot retrieve open orders");
        }

        protected override UserTransactions ExecuteGetUserTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            throw new NotImplementedException("BTCe cannot retrieve user transactions");
        }

        protected override bool ExecuteCancelOrderCommand(APICommand command, int id)
        {
            throw new NotImplementedException("BTCe cannot cancel orders");
        }

        protected override DepositAddress ExecuteGetDepositAddressCommand(APICommand command, Currency toDeposit)
        {
            throw new NotImplementedException("BTCe cannot retrieve deposit address");
        }


        protected override object ExecuteWithdrawCommand(APICommand command, Currency toWithdraw, string address, decimal amount)
        {
            throw new NotImplementedException("BTCe cannot execute withdrawals");

            /*Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("amount", amount.ToString());
            parameters.Add("address", address);

            return (string)SendCommandToDispatcher<string, string>(command, toWithdraw, Currency.None, parameters);
        
             */
        }

        protected override PendingDeposits ExecutePendingDepositsCommand(APICommand command)
        {
            throw new NotImplementedException("Get Pending Deposits is not implemented");
        }

        protected override PendingWithdrawals ExecutePendingWithdrawalsCommand(APICommand command)
        {
            throw new NotImplementedException("Get Pending Withdrawals is not implemented");
        }

        #endregion
    }
}
