using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

using RestSharp;
using BEx.BTCeSupport;

namespace BEx
{
    public class BTCe : Exchange
    {

        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        public BTCe() : base("BTCe.xml")
        {
            foreach (APICommand command in APICommandCollection.Values)
            {
                command.CurrencyFormatter += FormatCurrency;
            }
        }

        private string FormatCurrency(string currency)
        {
            return currency.ToLower();
        }

        public override Tick GetTick()
        {
            return base.GetTick<BTCeTickJSON>(Currency.BTC, Currency.USD);
        }

        public override Tick GetTick(Currency baseCurrency, Currency counterCurrency)
        {
            return base.GetTick<BTCeTickJSON>(baseCurrency, counterCurrency);
        }

        public override OrderBook GetOrderBook()
        {
            return base.GetOrderBook<BTCeOrderBookJSON>(Currency.BTC, Currency.USD);
        }

        public override OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency)
        {
            return base.GetOrderBook<BTCeOrderBookJSON>(baseCurrency, counterCurrency);
        }

        public override List<Transaction> GetTransactions()
        {
            return base.GetTransactions<BTCeTransactionsJSON>(Currency.BTC, Currency.USD);
        }

        public override List<Transaction> GetTransactions(Currency baseCurrency, Currency counterCurrency)
        {
            return base.GetTransactions<BTCeTransactionsJSON>(baseCurrency, counterCurrency);
        }

        public override AccountBalance GetAccountBalance()
        {
            return base.GetAccountBalance<BTCeAccountBalanceJSON>(Currency.BTC, Currency.USD);
        }

        public override object CreateBuyOrder(Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price)
        {
            return null;
        }

        public override object CreateBuyOrder(decimal amount, decimal price)
        {
            return null;
        }

        public override object CreateSellOrder(Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price)
        {
            return null;
        }

        public override object CreateSellOrder(decimal amount, decimal price)
        {
            return null;
        }

        protected override void CreateSignature(RestRequest request, APICommand command)
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

   
    }
}
