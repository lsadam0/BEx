using BEx.BitFinexSupport;
using BEx.Common;
using BEx.Request;
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
            : base(ExchangeType.BitFinex, "https://api.bitfinex.com")
        {
            VerifyCredentials(apiKey, secret);
        }

        internal APIError DetermineErrorCondition(string message)
        {
            APIError error = null;

            string errorMessage = ExtractMessage(message);

            string loweredMessage = errorMessage.ToLower();
            if (loweredMessage.Contains("not enough balance"))
            {
                error = new APIError(errorMessage, BExErrorCode.InsufficientFunds, this.ExchangeSourceType);
            }
            else if (loweredMessage.Contains("the given x-bfx-apikey") || loweredMessage.Contains("invalid x-bfx-signature"))
            {
                error = new APIError(errorMessage, BExErrorCode.Authorization, this.ExchangeSourceType);
            }

            if (error == null)
            {
                error = new APIError(message, BExErrorCode.Unknown, this.ExchangeSourceType);
            }

            return error;
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

        protected internal override void CreateSignature(RestRequest request, APICommand command, Currency baseCurrency, Currency counterCurrency, Dictionary<string, string> parameters = null)
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

        #region Commands

        protected override AccountBalance ExecuteAccountBalanceCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (AccountBalance)SendCommandToDispatcher<List<BitFinexAccountBalanceJSON>, AccountBalance>(command, baseCurrency, counterCurrency);
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

        protected override OrderBook ExecuteOrderBookCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (OrderBook)SendCommandToDispatcher<BitFinexOrderBookJSON, OrderBook>(command, baseCurrency, counterCurrency);
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

        protected override Tick ExecuteTickCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (Tick)SendCommandToDispatcher<BitfinexTickJSON, Tick>(command, baseCurrency, counterCurrency);
        }

        protected override Transactions ExecuteTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            double timeStamp = UnixTime.DateTimeToUnixTimestamp(DateTime.Now.AddHours(-1));

            parameters.Add("timestamp", timeStamp.ToString());

            return (Transactions)SendCommandToDispatcher<List<BitFinexTransactionJSON>, Transactions>(command, baseCurrency, counterCurrency, parameters);
        }

        #endregion Commands

        protected override Dictionary<Request.CommandClass, Request.ExchangeCommand> GetCommandCollection()
        {
            Dictionary<CommandClass, ExchangeCommand> res = new Dictionary<CommandClass, ExchangeCommand>();

            ExchangeParameter baseCurrency = new ExchangeParameter(ExchangeParameterType.Address, "base", "BTC");
            ExchangeParameter counterCurrency = new ExchangeParameter(ExchangeParameterType.Address, "counter", "USD");

            ExchangeCommand tick = new ExchangeCommand();
            tick.HttpMethod = Method.GET;
            tick.RelativeURI = "/v1/pubticker/{0}{1}";
            tick.IsAuthenticated = false;

            tick.Parameters.Add(baseCurrency.Name, baseCurrency);
            tick.Parameters.Add(counterCurrency.Name, counterCurrency);
            res.Add(tick.Identifier, tick);

            ExchangeCommand orderBook = new ExchangeCommand();
            orderBook.HttpMethod = Method.GET;
            orderBook.RelativeURI = "/v1/book/{0}{1}";
            orderBook.IsAuthenticated = false;
            orderBook.Parameters.Add(baseCurrency.Name, baseCurrency);
            orderBook.Parameters.Add(counterCurrency.Name, counterCurrency);
            res.Add(orderBook.Identifier, orderBook);

            ExchangeCommand transactions = new ExchangeCommand();
            transactions.HttpMethod = Method.GET;
            transactions.RelativeURI = "/v1/trades/{0}{1}";
            transactions.IsAuthenticated = false;
            transactions.Parameters.Add(baseCurrency.Name, baseCurrency);
            transactions.Parameters.Add(counterCurrency.Name, counterCurrency);

            ExchangeParameter transactionTimestamp = new ExchangeParameter(ExchangeParameterType.Post, "timestamp");
            transactions.Parameters.Add(transactionTimestamp.Name, transactionTimestamp);

            res.Add(transactions.Identifier, transactions);

            ExchangeCommand accountBalance = new ExchangeCommand();
            accountBalance.HttpMethod = Method.POST;
            accountBalance.RelativeURI = "/v1/balances";
            accountBalance.IsAuthenticated = true;

            res.Add(accountBalance.Identifier, accountBalance);

            ExchangeCommand depositAddress = new ExchangeCommand();
            depositAddress.HttpMethod = Method.POST;
            depositAddress.RelativeURI = "/v1/deposit/new";
            depositAddress.IsAuthenticated = true;

            ExchangeParameter depCurrency = new ExchangeParameter(ExchangeParameterType.Address, "currency", "BTC");
            depositAddress.Parameters.Add(depCurrency.Name, depCurrency);

            ExchangeParameter depMethod = new ExchangeParameter(ExchangeParameterType.Address, "method", "bitcoin");
            depositAddress.Parameters.Add(depMethod.Name, depMethod);

            ExchangeParameter depWallet = new ExchangeParameter(ExchangeParameterType.Address, "wallet_name", "exchange");
            depositAddress.Parameters.Add(depWallet.Name, depWallet);

            res.Add(depositAddress.Identifier, depositAddress);

            ExchangeCommand userTransactions = new ExchangeCommand();
            userTransactions.HttpMethod = Method.POST;
            userTransactions.RelativeURI = "/v1/mytrades";
            userTransactions.IsAuthenticated = true;

            ExchangeParameter symbolParam = new ExchangeParameter(ExchangeParameterType.Address, "symbol", "BTCUSD");
            userTransactions.Parameters.Add(symbolParam.Name, symbolParam);

            res.Add(userTransactions.Identifier, userTransactions);

            ExchangeParameter orderSymbol = new ExchangeParameter(ExchangeParameterType.Address, "symbol", "BTCUSD");
            ExchangeParameter orderAmount = new ExchangeParameter(ExchangeParameterType.Address, "amount");
            ExchangeParameter orderPrice = new ExchangeParameter(ExchangeParameterType.Address, "price");
            ExchangeParameter orderExchange = new ExchangeParameter(ExchangeParameterType.Address, "exchange");
            ExchangeParameter orderType = new ExchangeParameter(ExchangeParameterType.Address, "exchange limit");
            ExchangeParameter orderHidden = new ExchangeParameter(ExchangeParameterType.Address, "is_hidden");

            ExchangeCommand buy = new ExchangeCommand();
            buy.HttpMethod = Method.POST;
            buy.RelativeURI = "/v1/order/new";
            buy.IsAuthenticated = true;

            buy.Parameters.Add(orderSymbol.Name, orderSymbol);
            buy.Parameters.Add(orderAmount.Name, orderAmount);
            buy.Parameters.Add(orderPrice.Name, orderPrice);
            buy.Parameters.Add(orderExchange.Name, orderExchange);

            ExchangeParameter buyOrderSide = new ExchangeParameter(ExchangeParameterType.Address, "side", "buy");
            buy.Parameters.Add(buyOrderSide.Name, buyOrderSide);

            buy.Parameters.Add(orderType.Name, orderType);
            buy.Parameters.Add(orderHidden.Name, orderHidden);

            res.Add(buy.Identifier, buy);

            ExchangeCommand sell = new ExchangeCommand();
            sell.HttpMethod = Method.POST;
            sell.RelativeURI = "/v1/order/new";
            sell.IsAuthenticated = true;

            sell.Parameters.Add(orderSymbol.Name, orderSymbol);
            sell.Parameters.Add(orderAmount.Name, orderAmount);
            sell.Parameters.Add(orderPrice.Name, orderPrice);
            sell.Parameters.Add(orderExchange.Name, orderExchange);

            ExchangeParameter sellOrderSide = new ExchangeParameter(ExchangeParameterType.Address, "side", "sell");
            sell.Parameters.Add(sellOrderSide.Name, sellOrderSide);

            sell.Parameters.Add(orderType.Name, orderType);
            sell.Parameters.Add(orderHidden.Name, orderHidden);

            res.Add(sell.Identifier, sell);

            ExchangeCommand openOrders = new ExchangeCommand();
            openOrders.HttpMethod = Method.POST;
            openOrders.RelativeURI = "/v1/orders";
            openOrders.IsAuthenticated = true;

            res.Add(openOrders.Identifier, openOrders);

            ExchangeCommand cancelOrder = new ExchangeCommand();
            cancelOrder.HttpMethod = Method.POST;
            cancelOrder.RelativeURI = "/v1/order/cancel";
            cancelOrder.IsAuthenticated = true;

            ExchangeParameter cancelOrderId = new ExchangeParameter(ExchangeParameterType.Address, "order_id");

            cancelOrder.Parameters.Add(cancelOrderId.Name, cancelOrderId);

            res.Add(cancelOrder.Identifier, cancelOrder);

            return res;
        }

        protected override HashSet<CurrencyTradingPair> GetSupportedTradingPairs()
        {
            HashSet<CurrencyTradingPair> res = new HashSet<CurrencyTradingPair>();

            res.Add(new CurrencyTradingPair(Currency.BTC, Currency.USD));
            res.Add(new CurrencyTradingPair(Currency.LTC, Currency.USD));
            res.Add(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
            res.Add(new CurrencyTradingPair(Currency.DRK, Currency.USD));
            res.Add(new CurrencyTradingPair(Currency.DRK, Currency.BTC));

            return res;
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
    }
}