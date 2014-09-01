using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Security.Cryptography;

using RestSharp;
using BEx.BitStampSupport;

namespace BEx
{
    public class BitStamp : Exchange
    {
        public BitStamp()
            : base("Bitstamp.xml")
        {
            
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
