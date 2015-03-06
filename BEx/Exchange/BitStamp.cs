using BEx.BitStampSupport;
using BEx.Request;
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
            : base(ExchangeType.BitStamp, "https://www.bitstamp.net/api/")
        {
            VerifyCredentials(apiKey, secretKey, clientId);

            errorId = new Regex("^{\"error\":");// \"API key not found\"}");
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
                    error = new APIError(errorMessage, BExErrorCode.InsufficientFunds, this.ExchangeSourceType);
                }
                else if (loweredMessage.Contains("api key not found") || loweredMessage.Contains("invalid signature"))
                {
                    error = new APIError(errorMessage, BExErrorCode.Authorization, this.ExchangeSourceType);
                }

                if (error == null)
                {
                    error = new APIError(message, BExErrorCode.Unknown, this.ExchangeSourceType);
                }

                return error;
            }
            else return null;
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

        protected internal override void CreateSignature(RestRequest request, ExchangeCommand command, CurrencyTradingPair pair, Dictionary<string, string> parameters = null)
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

        protected override Dictionary<CommandClass, ExchangeCommand> GetCommandCollection()
        {
            Dictionary<CommandClass, ExchangeCommand> res = new Dictionary<CommandClass, ExchangeCommand>();

            ExchangeCommand tick = new ExchangeCommand(CommandClass.Tick,
                                                        Method.GET,
                                                       "ticker/",
                                                        false);

            res.Add(tick.Identifier, tick);

            ExchangeCommand orderBook = new ExchangeCommand(

            CommandClass.OrderBook,
            Method.GET,
             "order_book/",
            false);

            List<ExchangeParameter> transactionParams = new List<ExchangeParameter>();
            transactionParams.Add(new ExchangeParameter(Request.ExchangeParameterType.Address, "time", "hour"));

            ExchangeCommand transactions = new ExchangeCommand(
         CommandClass.Transactions,
         Method.GET,
    "transactions/",
false, false, transactionParams);

            res.Add(transactions.Identifier, transactions);

            ExchangeCommand accountBalance = new ExchangeCommand(

             CommandClass.AccountBalance,
             Method.POST,
             "balance/",
             true);

            res.Add(accountBalance.Identifier, accountBalance);

            List<ExchangeParameter> buyParams = new List<ExchangeParameter>();

            buyParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "amount"));
            buyParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "price"));

            ExchangeCommand buyOrder = new ExchangeCommand(

            CommandClass.BuyOrder,
            Method.POST,
            "buy/",
            true,
            false,
            buyParams);

            res.Add(buyOrder.Identifier, buyOrder);

            List<ExchangeParameter> sellParams = new List<ExchangeParameter>();

            sellParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "amount"));

            sellParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "price"));

            ExchangeCommand sellOrder = new ExchangeCommand(

            CommandClass.SellOrder,
            Method.POST,
            "sell/",
            true,
            false,
            sellParams);

            res.Add(sellOrder.Identifier, sellOrder);

            ExchangeCommand openOrders = new ExchangeCommand(

            CommandClass.OpenOrders,
            Method.POST,
            "open_orders/",
            true);
            res.Add(openOrders.Identifier, openOrders);

            ExchangeCommand userTransactions = new ExchangeCommand(

            CommandClass.UserTransactions,
            Method.POST,
            "user_transactions/",
            true);

            res.Add(userTransactions.Identifier, userTransactions);

            List<ExchangeParameter> cancelParams = new List<ExchangeParameter>();

            cancelParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "id"));

            ExchangeCommand cancelOrder = new ExchangeCommand(

            CommandClass.CancelOrder,
            Method.POST,
            "cancel_order/",
            true,
            true,
            cancelParams);

            res.Add(cancelOrder.Identifier, cancelOrder);

            ExchangeCommand depositAddress = new ExchangeCommand(

            CommandClass.DepositAddress,
            Method.POST,
            "bitcoin_depost_address/",
            true,
            true);

            res.Add(depositAddress.Identifier, depositAddress);

            return res;
        }

        protected override HashSet<CurrencyTradingPair> GetSupportedTradingPairs()
        {
            HashSet<CurrencyTradingPair> res = new HashSet<CurrencyTradingPair>();

            res.Add(DefaultPair);

            return res;
        }

        #region Commands

        protected override AccountBalance ExecuteAccountBalanceCommand(ExchangeCommand command, CurrencyTradingPair pair)
        {
            return (AccountBalance)SendCommandToDispatcher<BitStampAccountBalanceJSON, AccountBalance>(command, pair);
        }

        protected override bool ExecuteCancelOrderCommand(ExchangeCommand command, int id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("id", id.ToString());

            return (bool)SendCommandToDispatcher<bool, bool>(command, DefaultPair, parameters);
        }

        protected override DepositAddress ExecuteGetDepositAddressCommand(ExchangeCommand command, Currency toDeposit)
        {
            return (DepositAddress)SendCommandToDispatcher<string, DepositAddress>(command, DefaultPair);
        }

        protected override OpenOrders ExecuteGetOpenOrdersCommand(ExchangeCommand command, CurrencyTradingPair pair)
        {
            return (OpenOrders)SendCommandToDispatcher<List<BitStampOrderConfirmationJSON>, OpenOrders>(command, pair);
        }

        protected override UserTransactions ExecuteGetUserTransactionsCommand(ExchangeCommand command, CurrencyTradingPair pair)
        {
            return (UserTransactions)SendCommandToDispatcher<List<BitStampUserTransactionJSON>, UserTransactions>(command, pair);
        }

        protected override OrderBook ExecuteOrderBookCommand(ExchangeCommand command, CurrencyTradingPair pair)
        {
            return (OrderBook)SendCommandToDispatcher<BitstampOrderBookJSON, OrderBook>(command, pair);
        }

        protected override Order ExecuteOrderCommand(ExchangeCommand command, CurrencyTradingPair pair, decimal amount, decimal price)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("price", price.ToString());

            return (Order)SendCommandToDispatcher<BitStampOrderConfirmationJSON, Order>(command, pair, parameters);
        }

        protected override Tick ExecuteTickCommand(ExchangeCommand command, CurrencyTradingPair pair)
        {
            return (Tick)SendCommandToDispatcher<BitstampTickJSON, Tick>(command, pair);
        }

        protected override Transactions ExecuteTransactionsCommand(ExchangeCommand command, CurrencyTradingPair pair)
        {
            return (Transactions)SendCommandToDispatcher<List<BitstampTransactionJSON>, Transactions>(command, pair);
        }

        #endregion Commands

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
    }
}