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


        internal override void SetParameters(APICommand command)
        {
            
        }

        protected override void CreateSignature(RestRequest request, APICommand command)
        {
            Tuple<string, string, string> res = new Tuple<string, string, string>("", "", "");
            //hashMaker = new HMACSHA512(Encoding.ASCII.GetBytes(secret));

            // PostData order
            // method
            // nonce


            long _nonce = Nonce;

            StringBuilder dataBuilder = new StringBuilder();

           // foreach (string p in data)

            string postString = "method=getInfo&nonce=" + _nonce.ToString();
                
            string signature;
            using (HMACSHA512 hasher = new HMACSHA512(Encoding.ASCII.GetBytes(SecretKey)))
            {
                byte[] hashBytes = hasher.ComputeHash(Encoding.ASCII.GetBytes(postString));
                //request.AddHeader("Sign", BitConverter.ToString(hashBytes).Replace("-", "").ToLower());
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

    }
}
