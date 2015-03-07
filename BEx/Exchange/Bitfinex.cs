using BEx.BitFinexSupport;
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

        protected internal override void CreateSignature(RestRequest request, ExchangeCommand command, CurrencyTradingPair pair, Dictionary<string, string> parameters = null)
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
            payload.Append("\"request\": \"" + command.GetResolvedRelativeURI(pair) + "\",");
            payload.Append("\"nonce\": \"" + Nonce + "\"");

            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> kvPair in parameters)
                {
                    payload.Append(",");
                    payload.Append("\"" + kvPair.Key + "\": \"" + kvPair.Value + "\"");
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

        protected override Dictionary<Request.CommandClass, Request.ExchangeCommand> GetCommandCollection()
        {
            Dictionary<CommandClass, ExchangeCommand> res = new Dictionary<CommandClass, ExchangeCommand>();

            ExchangeParameter baseCurrency = new ExchangeParameter(ExchangeParameterType.Address, "base", StandardParameterType.Base, "BTC");
            ExchangeParameter counterCurrency = new ExchangeParameter(ExchangeParameterType.Address, "counter", StandardParameterType.Counter, "USD");

            List<ExchangeParameter> currencyParams = new List<ExchangeParameter>();
            currencyParams.Add(baseCurrency);
            currencyParams.Add(counterCurrency);

            ExchangeCommand tick = new ExchangeCommand(CommandClass.Tick,
            Method.GET,
            "/v1/pubticker/{0}{1}",
            false,
            typeof(BitfinexTickJSON),
            false,
            currencyParams);

            res.Add(tick.Identifier, tick);

            ExchangeCommand orderBook = new ExchangeCommand(CommandClass.OrderBook,
            Method.GET,
            "/v1/book/{0}{1}",
            false,
            typeof(BitFinexOrderBookJSON),
            false,
            currencyParams);

            res.Add(orderBook.Identifier, orderBook);

            List<ExchangeParameter> transactionsParams = new List<ExchangeParameter>();

            currencyParams.ForEach(x => transactionsParams.Add(x));

            currencyParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "timestamp", StandardParameterType.None, "needtoset"));
            ExchangeCommand transactions = new ExchangeCommand(
                CommandClass.Transactions,
            Method.GET,
            "/v1/trades/{0}{1}",
            false,
            typeof(List<BitFinexUserTransactionJSON>),
            false,
            transactionsParams);

            res.Add(transactions.Identifier, transactions);

            ExchangeCommand accountBalance = new ExchangeCommand(CommandClass.AccountBalance,
            Method.POST,
            "/v1/balances",
            true,
            typeof(BitFinexAccountBalanceJSON));

            res.Add(accountBalance.Identifier, accountBalance);

            List<ExchangeParameter> depositParams = new List<ExchangeParameter>();

            depositParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "currency", StandardParameterType.Currency, "BTC"));
            depositParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "method", StandardParameterType.CurrencyFullName, "bitcoin"));
            depositParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "wallet_name", StandardParameterType.None, "exchange"));

            ExchangeCommand depositAddress = new ExchangeCommand(CommandClass.DepositAddress,
            Method.POST,
            "/v1/deposit/new",
            true,
            typeof(BitFinexDepositAddressJSON),
            false,
            depositParams);

            res.Add(depositAddress.Identifier, depositAddress);

            List<ExchangeParameter> userTransParams = new List<ExchangeParameter>();

            userTransParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "symbol", StandardParameterType.Pair, "BTCUSD"));

            ExchangeCommand userTransactions = new ExchangeCommand(CommandClass.UserTransactions,
            Method.POST,
            "/v1/mytrades",
            true,
            typeof(List<BitFinexUserTransactionJSON>),
            false,
            userTransParams);

            res.Add(userTransactions.Identifier, userTransactions);

            List<ExchangeParameter> sharedOrderParams = new List<ExchangeParameter>();
            sharedOrderParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "symbol", StandardParameterType.Pair, "BTCUSD"));
            sharedOrderParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "amount", StandardParameterType.Amount));
            sharedOrderParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "price", StandardParameterType.Price));
            sharedOrderParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "exchange", StandardParameterType.None, "bitfinex"));
            sharedOrderParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "type", StandardParameterType.None, "exchange limit"));
            sharedOrderParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "is_hidden", StandardParameterType.None, "false"));

            List<ExchangeParameter> buyOrderParams = new List<ExchangeParameter>();

            sharedOrderParams.ForEach(x => buyOrderParams.Add(x));

            buyOrderParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "side", StandardParameterType.None, "buy"));
            ExchangeCommand buy = new ExchangeCommand(CommandClass.BuyOrder,
            Method.POST,
            "/v1/order/new",
            true,
            typeof(BitFinexOrderResponseJSON),
            false,
            buyOrderParams);

            res.Add(buy.Identifier, buy);

            List<ExchangeParameter> sellOrderParams = new List<ExchangeParameter>();

            sharedOrderParams.ForEach(x => sellOrderParams.Add(x));
            sellOrderParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "side", StandardParameterType.None, "sell"));

            ExchangeCommand sell = new ExchangeCommand(CommandClass.SellOrder,
            Method.POST,
            "/v1/order/new",
            true,
            typeof(BitFinexOrderResponseJSON),
            false,
            sellOrderParams);

            res.Add(sell.Identifier, sell);

            ExchangeCommand openOrders = new ExchangeCommand(CommandClass.OpenOrders,
            Method.POST,
            "/v1/orders",
            true,
            typeof(List<BitFinexOrderResponseJSON>));

            res.Add(openOrders.Identifier, openOrders);

            List<ExchangeParameter> cancelOrderParams = new List<ExchangeParameter>();

            cancelOrderParams.Add(new ExchangeParameter(ExchangeParameterType.Post, "order_id", StandardParameterType.Id));
            ExchangeCommand cancelOrder = new ExchangeCommand(CommandClass.CancelOrder,
            Method.POST,
            "/v1/order/cancel",
            true,
            typeof(bool),
            false,
            cancelOrderParams);

            res.Add(cancelOrder.Identifier, cancelOrder);

            return res;
        }

        protected override HashSet<CurrencyTradingPair> GetSupportedTradingPairs()
        {
            HashSet<CurrencyTradingPair> res = new HashSet<CurrencyTradingPair>();

            res.Add(DefaultPair);
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