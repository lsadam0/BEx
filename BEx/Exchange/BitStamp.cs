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

        protected override HashSet<CurrencyTradingPair> GetSupportedTradingPairs()
        {
            HashSet<CurrencyTradingPair> res = new HashSet<CurrencyTradingPair>();

            res.Add(new CurrencyTradingPair(Currency.BTC, Currency.USD));

            return res;
        }

        protected override Dictionary<CommandClass, ExchangeCommand> GetCommandCollection()
        {
            Dictionary<CommandClass, ExchangeCommand> res = new Dictionary<CommandClass, ExchangeCommand>();
            /*<Commands>
              <Command ID="Tick">
                <Method>GET</Method>
                <RelativeURL>ticker/</RelativeURL>
                <RequiresAuthentication>false</RequiresAuthentication>
                <args />
              </Command>
             */

            ExchangeCommand tick = new ExchangeCommand();

            tick.Identifier = CommandClass.Tick;
            tick.HttpMethod = Method.GET;
            tick.RelativeURI = "ticker/";
            tick.IsAuthenticated = false;

            res.Add(tick.Identifier, tick);

            /*
             <Command ID="OrderBook">
               <Method>GET</Method>
               <RelativeURL>order_book/</RelativeURL>
               <RequiresAuthentication>false</RequiresAuthentication>
               <args />
             </Command>
             */

            ExchangeCommand orderBook = new ExchangeCommand();

            orderBook.Identifier = CommandClass.OrderBook;
            orderBook.HttpMethod = Method.GET;
            orderBook.RelativeURI = "order_book/";
            orderBook.IsAuthenticated = false;

            res.Add(orderBook.Identifier, orderBook);
            /*
             <Command ID="Transactions">
               <Method>GET</Method>
               <RelativeURL>transactions/</RelativeURL>
               <RequiresAuthentication>false</RequiresAuthentication>
               <args>
                 <arg ID="time" type="parameter">hour</arg>
               </args>
             </Command>
             */

            ExchangeCommand transactions = new ExchangeCommand();

            transactions.Identifier = CommandClass.Transactions;
            transactions.HttpMethod = Method.GET;
            transactions.RelativeURI = "transactions/";
            transactions.IsAuthenticated = false;

            transactions.Parameters.Add("time", new ExchangeParameter(Request.ExchangeParameterType.Address, "time", "hour"));

            res.Add(transactions.Identifier, transactions);

            /*
             <Command ID="AccountBalance">
               <Method>POST</Method>
               <RelativeURL>balance/</RelativeURL>
               <RequiresAuthentication>true</RequiresAuthentication>
               <args />
             </Command>*/

            ExchangeCommand accountBalance = new ExchangeCommand();

            accountBalance.Identifier = CommandClass.AccountBalance;
            accountBalance.HttpMethod = Method.POST;
            accountBalance.RelativeURI = "balance/";
            accountBalance.IsAuthenticated = true;

            res.Add(accountBalance.Identifier, accountBalance);

            /*

             <Command ID="BuyOrder">
               <Method>POST</Method>
               <RelativeURL>buy/</RelativeURL>
               <RequiresAuthentication>true</RequiresAuthentication>
               <args>
                 <arg ID="amount" type="parameter" />
                 <arg ID="price" type="parameter" />
               </args>
             </Command>*/

            ExchangeCommand buyOrder = new ExchangeCommand();

            buyOrder.Identifier = CommandClass.BuyOrder;
            buyOrder.HttpMethod = Method.POST;
            buyOrder.RelativeURI = "buy/";
            buyOrder.IsAuthenticated = true;

            ExchangeParameter buyAmount = new ExchangeParameter(ExchangeParameterType.Post, "amount");
            buyOrder.Parameters.Add(buyAmount.Name, buyAmount);

            ExchangeParameter buyPrice = new ExchangeParameter(ExchangeParameterType.Post, "price");
            buyOrder.Parameters.Add(buyPrice.Name, buyPrice);

            res.Add(buyOrder.Identifier, buyOrder);

            /*
             <Command ID="SellOrder">
               <Method>POST</Method>
               <RelativeURL>sell/</RelativeURL>
               <RequiresAuthentication>true</RequiresAuthentication>
               <args>
                 <arg ID="amount" type="parameter" />
                 <arg ID="price" type="parameter" />
               </args>
             </Command>*/

            ExchangeCommand sellOrder = new ExchangeCommand();

            sellOrder.Identifier = CommandClass.SellOrder;
            sellOrder.HttpMethod = Method.POST;
            sellOrder.RelativeURI = "sell/";
            sellOrder.IsAuthenticated = true;

            ExchangeParameter sellAmount = new ExchangeParameter(ExchangeParameterType.Post, "amount");
            sellOrder.Parameters.Add(sellAmount.Name, sellAmount);

            ExchangeParameter sellPrice = new ExchangeParameter(ExchangeParameterType.Post, "price");
            sellOrder.Parameters.Add(sellPrice.Name, sellPrice);

            res.Add(sellOrder.Identifier, sellOrder);

            /*
             <Command ID="OpenOrders">
               <Method>POST</Method>
               <RelativeURL>open_orders/</RelativeURL>
               <RequiresAuthentication>true</RequiresAuthentication>
               <args />
             </Command>*/

            ExchangeCommand openOrders = new ExchangeCommand();

            openOrders.Identifier = CommandClass.OpenOrders;
            openOrders.HttpMethod = Method.POST;
            openOrders.RelativeURI = "open_orders/";
            openOrders.IsAuthenticated = true;

            res.Add(openOrders.Identifier, openOrders);

            /*
             <Command ID="UserTransactions">
               <Method>POST</Method>
               <RelativeURL>user_transactions/</RelativeURL>
               <RequiresAuthentication>true</RequiresAuthentication>
               <args />
             </Command>*/

            ExchangeCommand userTransactions = new ExchangeCommand();

            userTransactions.Identifier = CommandClass.UserTransactions;
            userTransactions.HttpMethod = Method.POST;
            userTransactions.RelativeURI = "user_transactions/";
            userTransactions.IsAuthenticated = true;

            res.Add(userTransactions.Identifier, userTransactions);

            /*
             <Command ID="CancelOrder">
               <Method>POST</Method>
               <RelativeURL>cancel_order/</RelativeURL>
               <RequiresAuthentication>true</RequiresAuthentication>
               <ReturnsValueType>true</ReturnsValueType>
               <args>
                 <arg ID="id" type="parameter"></arg>
               </args>
             </Command>*/

            ExchangeCommand cancelOrder = new ExchangeCommand();

            cancelOrder.Identifier = CommandClass.CancelOrder;
            cancelOrder.HttpMethod = Method.POST;
            cancelOrder.RelativeURI = "cancel_order/";
            cancelOrder.IsAuthenticated = true;
            cancelOrder.ReturnsValueType = true;

            ExchangeParameter cancelOrderId = new ExchangeParameter(ExchangeParameterType.Post, "id");
            cancelOrder.Parameters.Add(cancelOrderId.Name, cancelOrderId);

            res.Add(cancelOrder.Identifier, cancelOrder);

            /*
             <Command ID="DepositAddress">
               <Method>POST</Method>
               <RelativeURL>bitcoin_deposit_address/</RelativeURL>
               <RequiresAuthentication>true</RequiresAuthentication>
               <ReturnsValueType>true</ReturnsValueType>
               <args />
             </Command>*/

            ExchangeCommand depositAddress = new ExchangeCommand();

            depositAddress.Identifier = CommandClass.DepositAddress;
            depositAddress.HttpMethod = Method.POST;
            depositAddress.RelativeURI = "bitcoin_depost_address/";
            depositAddress.IsAuthenticated = true;
            depositAddress.ReturnsValueType = true;

            res.Add(depositAddress.Identifier, depositAddress);

            return res;
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

        protected internal override void CreateSignature(RestRequest request, APICommand command, Currency baseCurrency, Currency counterCurrency, Dictionary<string, string> parameters = null)
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

        #region Commands

        protected override AccountBalance ExecuteAccountBalanceCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (AccountBalance)SendCommandToDispatcher<BitStampAccountBalanceJSON, AccountBalance>(command, baseCurrency, counterCurrency);
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

        protected override OpenOrders ExecuteGetOpenOrdersCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (OpenOrders)SendCommandToDispatcher<List<BitStampOrderConfirmationJSON>, OpenOrders>(command, baseCurrency, counterCurrency);
        }

        protected override UserTransactions ExecuteGetUserTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (UserTransactions)SendCommandToDispatcher<List<BitStampUserTransactionJSON>, UserTransactions>(command, baseCurrency, counterCurrency);
        }

        protected override OrderBook ExecuteOrderBookCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (OrderBook)SendCommandToDispatcher<BitstampOrderBookJSON, OrderBook>(command, baseCurrency, counterCurrency);
        }

        protected override Order ExecuteOrderCommand(APICommand command, Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("price", price.ToString());

            return (Order)SendCommandToDispatcher<BitStampOrderConfirmationJSON, Order>(command, baseCurrency, counterCurrency, parameters);
        }

        protected override Tick ExecuteTickCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (Tick)SendCommandToDispatcher<BitstampTickJSON, Tick>(command, baseCurrency, counterCurrency);
        }

        protected override Transactions ExecuteTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (Transactions)SendCommandToDispatcher<List<BitstampTransactionJSON>, Transactions>(command, baseCurrency, counterCurrency);
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