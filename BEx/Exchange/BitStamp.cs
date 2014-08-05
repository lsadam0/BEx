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
    public class BitStamp : Exchange//<BitstampTickJSON, BitstampTransactionJSON, BitstampOrderBookJSON>
    {

        public BitStamp()
            : base("Bitstamp.xml")
        {

        }

        public override Tick GetTick()
        {
            return base.GetTick<BitstampTickJSON>(Currency.BTC, Currency.USD);
        }

        public override Tick GetTick(Currency baseCurrency, Currency counterCurrency)
        {
            return base.GetTick<BitstampTickJSON>(baseCurrency, counterCurrency);
        }

        public override OrderBook GetOrderBook()
        {
            return base.GetOrderBook<BitstampOrderBookJSON>(Currency.BTC, Currency.USD);
        }

        public override OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency)
        {
            return base.GetOrderBook<BitstampOrderBookJSON>(baseCurrency, counterCurrency);
        }

        public override Transactions GetTransactions()
        {
            return base.GetTransactions<BitstampTransactionJSON>(Currency.BTC, Currency.USD);
        }

        public override Transactions GetTransactions(Currency baseCurrency, Currency counterCurrency)
        {
            return base.GetTransactions<BitstampTransactionJSON>(baseCurrency, counterCurrency);
        }

        public override AccountBalance GetAccountBalance()
        {
            return base.GetAccountBalance<BitStampAccountBalanceJSON>(Currency.BTC, Currency.USD);
        }

        public override Order CreateBuyOrder(decimal amount, decimal price)
        {
            return CreateBuyOrder(Currency.BTC, Currency.USD, amount, price);
        }

        public override Order CreateBuyOrder(Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price)
        {
            APICommand toExecute = APICommandCollection["BuyOrder"];

            toExecute.Parameters["amount"] = amount.ToString();
            toExecute.Parameters["price"] = price.ToString();

            return base.CreateBuyOrder<BitStampOrderConfirmationJSON>(baseCurrency, counterCurrency);
        }

        public override Order CreateSellOrder(decimal amount, decimal price)
        {
            return CreateSellOrder(Currency.BTC, Currency.USD, amount, price);
        }

        public override Order CreateSellOrder(Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price)
        {
            APICommand toExecute = APICommandCollection["SellOrder"];

            toExecute.Parameters["amount"] = amount.ToString();
            toExecute.Parameters["price"] = price.ToString();

            return base.CreateSellOrder<BitStampOrderConfirmationJSON>(baseCurrency, counterCurrency);
        }

        public override OpenOrders GetOpenOrders()
        {
            APICommand toExecute = APICommandCollection["OpenOrders"];

            return base.GetOpenOrders<BitStampOrderConfirmationJSON>(Currency.BTC, Currency.USD);
        }

        public override UserTransactions GetUserTransactions()
        {
            APICommand toExecute = APICommandCollection["UserTransactions"];

            return base.GetUserTransactions<BitStampUserTransactionJSON>(Currency.BTC, Currency.USD);
        }

        public override bool CancelOrder(int id)
        {
            APICommand toExecute = APICommandCollection["CancelOrder"];

            toExecute.Parameters["id"] = id.ToString();

            return base.CancelOrder<bool>(id);
        }

        public override bool CancelOrder(Order toCancel)
        {
            return CancelOrder(toCancel.ID);
        }

        public override string GetDepositAddress()
        {
            return GetDepositAddress(Currency.BTC);
        }

        public override string GetDepositAddress(Currency toDeposit)
        {
            APICommand toExecute = APICommandCollection["DepositAddress"];

            return base.GetDepositAddress<string>(toDeposit).ToString();
        }

        public override string Withdraw()
        {
            return Withdraw(Currency.BTC);
        }

        public override string Withdraw(Currency toWithdraw)
        {
            APICommand toExecute = APICommandCollection["Withdraw"];

            return base.Withdraw<string>(toWithdraw).ToString();
        }

        public override object PendingDeposits()
        {
            throw new NotImplementedException();
        }

        public override object PendingWithdrawals()
        {
            throw new NotImplementedException();
        }

        protected override void CreateSignature(RestRequest request, APICommand command, Currency baseCurrency, Currency counterCurrency)
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
                        
            foreach (KeyValuePair<string, string> param in command.Parameters)
            {
                request.AddParameter(param.Key, Uri.EscapeUriString(param.Value.ToString()));
            }

        }

        public static byte[] SignHMACSHA256(String key, byte[] data)
        {
            HMACSHA256 hashMaker = new HMACSHA256(Encoding.ASCII.GetBytes(key));
            return hashMaker.ComputeHash(data);
        }
    }
}
