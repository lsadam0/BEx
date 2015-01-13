using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Web;

using RestSharp;
using BEx.BTCeSupport;

namespace BEx
{
    public class BTCe : Exchange
    {

        /// <summary>
        /// Nonce computation
        /// </summary>
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public BTCe()
            : base("BTCe.xml")
        {
            foreach (APICommand command in APICommandCollection.Values)
            {
                command.CurrencyFormatter += FormatCurrency;
            }
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

        #region Authorization

        protected override void CreateSignature(RestRequest request, APICommand command, Currency baseCurrneyc, Currency counterCurrency, Dictionary<string, string> parameters = null)
        {
            long _nonce = BTCeNonce;
            
            StringBuilder dataBuilder = new StringBuilder();
            
            string postString = "method=getInfo&nonce=" + _nonce.ToString();

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
            request.AddParameter("method", "getInfo");
            request.AddParameter("nonce", _nonce.ToString());
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

            throw new NotImplementedException("BTCe cannot create orders");
            /*
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("price", amount.ToString());

            return (Order)SendCommandToDispatcher<BT>(command, baseCurrency, counterCurrency, parameters);*/
        }

        protected override OpenOrders ExecuteGetOpenOrdersCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            throw new NotImplementedException("BTCe cannot retrieve open orders");
            return null;
        }

        protected override UserTransactions ExecuteGetUserTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            throw new NotImplementedException("BTCe cannot retrieve user transactions");
            return null;
        }

        protected override bool ExecuteCancelOrderCommand(APICommand command, int id)
        {
            throw new NotImplementedException("BTCe cannot cancel orders");
            return false;
        }

        protected override DepositAddress ExecuteGetDepositAddressCommand(APICommand command, Currency toDeposit)
        {
            throw new NotImplementedException("BTCe cannot retrieve deposit address");
            return null;
        }


        protected override object ExecuteWithdrawCommand(APICommand command, Currency toWithdraw, string address, decimal amount)
        {
            throw new NotImplementedException("BTCe cannot execute withdrawals");

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("amount", amount.ToString());
            parameters.Add("address", address);

            return (string)SendCommandToDispatcher<string, string>(command, toWithdraw, Currency.None, parameters);
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
