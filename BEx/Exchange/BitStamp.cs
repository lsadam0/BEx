using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

using RestSharp;
using BEx.BitStampSupport;

namespace BEx
{
    public class BitStamp : Exchange
    {
        Dictionary<string, Regex> errorMessageRegexCollection;

        public BitStamp()
            : base("Bitstamp.xml")
        {
            BuildErrorMessageRegexCollection();

            this.dispatcher.IsError += IsError;
            this.dispatcher.CreateException += CreateException;
        }



        private void BuildErrorMessageRegexCollection()
        {
            errorMessageRegexCollection = new Dictionary<string, Regex>();

            Regex errorId = new Regex("^{\"error\":");// \"API key not found\"}");

            errorMessageRegexCollection.Add("ErrorIdentification", new Regex("^{\"error\":"));
            errorMessageRegexCollection.Add("InsufFunds", new Regex("\"You have only"));
            errorMessageRegexCollection.Add("APIKeyMissing", new Regex("\"API key not found\""));
            errorMessageRegexCollection.Add("MissingAuthParameters", new Regex("\"Missing key, signature and nonce parameters\""));
            errorMessageRegexCollection.Add("InvalidSignature", new Regex("\"Invalid signature\""));
        }


        internal bool IsError(string content)
        {
            bool res = false;

            if (errorMessageRegexCollection["ErrorIdentification"].IsMatch(content))
            {
                res = true;
            }

            return res;
        }

        internal Exception CreateException(string message, string bareResponse, APICommand executedCommand, Exception inner = null)
        {
            Exception res = null;

            if (errorMessageRegexCollection["APIKeyMissing"].IsMatch(bareResponse) ||
                errorMessageRegexCollection["MissingAuthParameters"].IsMatch(bareResponse) ||
                errorMessageRegexCollection["InvalidSignature"].IsMatch(bareResponse)
                )
            {
                res = new ExchangeAuthorizationException(message, inner);
            }

            if (res == null)
            {
                switch (executedCommand.ID)
                {
                    case ("BuyOrder"):
                    case ("SellOrder"):


                        if (errorMessageRegexCollection["InsufFunds"].IsMatch(bareResponse))
                        {
                            res = new InsufficientFundsException(message, inner);
                        }
                        else
                            res = new OrderRejectedException(message, inner);
                        break;
                    default:
                        res = new Exception(message, inner);
                        break;
                }
            }

            return res;
        }


        #region Authorization

        protected override void CreateSignature(RestRequest request, APICommand command, Currency baseCurrency, Currency counterCurrency, Dictionary<string, string> parameters = null)
        {

            /*Signature is a HMAC-SHA256 encoded message containing: nonce, client ID and API key. The HMAC-SHA256 code must be generated using a secret key that was generated with your API key. This code must be converted to it's hexadecimal representation (64 uppercase characters).

                Example (Python):
                message = nonce + client_id + api_key
                signature = hmac.new(API_SECRET, msg=message, digestmod=hashlib.sha256).hexdigest().upper()*/

            //string signature = CreateToken(Nonce + ClientID + APIKey, SecretKey);

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
            return (Tick)SendCommandToDispatcher<BitstampTickJSON>(command, baseCurrency, counterCurrency);
        }

        protected override OrderBook ExecuteOrderBookCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (OrderBook)SendCommandToDispatcher<BitstampOrderBookJSON>(command, baseCurrency, counterCurrency);
        }

        protected override Transactions ExecuteTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (Transactions)SendCommandToDispatcher<List<BitstampTransactionJSON>>(command, baseCurrency, counterCurrency);
        }

        protected override AccountBalance ExecuteAccountBalanceCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (AccountBalance)SendCommandToDispatcher<BitStampAccountBalanceJSON>(command, baseCurrency, counterCurrency);
        }

        protected override Order ExecuteOrderCommand(APICommand command, Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("price", price.ToString());

            return (Order)SendCommandToDispatcher<BitStampOrderConfirmationJSON>(command, baseCurrency, counterCurrency, parameters);
        }

        protected override OpenOrders ExecuteGetOpenOrdersCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (OpenOrders)SendCommandToDispatcher<List<BitStampOrderConfirmationJSON>>(command, baseCurrency, counterCurrency);
        }

        protected override UserTransactions ExecuteGetUserTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (UserTransactions)SendCommandToDispatcher<List<BitStampUserTransactionJSON>>(command, baseCurrency, counterCurrency);
        }

        protected override bool ExecuteCancelOrderCommand(APICommand command, int id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("id", id.ToString());

            return (bool)SendCommandToDispatcher<bool>(command, Currency.BTC, Currency.USD, parameters);
        }

        protected override string ExecuteGetDepositAddressCommand(APICommand command, Currency toDeposit)
        {
            return (string)SendCommandToDispatcher<string>(command, Currency.BTC, Currency.USD);
        }

        protected override object ExecuteWithdrawCommand(APICommand command, Currency toWithdraw, string address, decimal amount)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("amount", amount.ToString());
            parameters.Add("address", address);

            return (string)SendCommandToDispatcher<string>(command, toWithdraw, Currency.None, parameters);
        }

        protected override PendingDeposits ExecutePendingDepositsCommand(APICommand command)
        {
            return (PendingDeposits)SendCommandToDispatcher<List<BitStampPendingDepositJSON>>(command, Currency.BTC, Currency.USD);
        }

        protected override PendingWithdrawals ExecutePendingWithdrawalsCommand(APICommand command)
        {
            return (PendingWithdrawals)SendCommandToDispatcher<List<BitStampPendingWithdrawalJSON>>(command, Currency.BTC, Currency.USD);
        }

        #endregion
    }
}
