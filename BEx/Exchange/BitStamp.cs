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

        public override List<Transaction> GetTransactions()
        {
            return base.GetTransactions<BitstampTransactionJSON>(Currency.BTC, Currency.USD);
        }

        public override List<Transaction> GetTransactions(Currency baseCurrency, Currency counterCurrency)
        {
            return base.GetTransactions<BitstampTransactionJSON>(baseCurrency, counterCurrency);
        }

        public override AccountBalance GetAccountBalance()
        {
            return base.GetAccountBalance<BitStampAccountBalanceJSON>(Currency.BTC, Currency.USD);
        }

        internal override void SetParameters(APICommand command)
        {

        }


        protected override void CreateSignature(RestRequest request, APICommand command)
        {
            
            /*Signature is a HMAC-SHA256 encoded message containing: nonce, client ID and API key. The HMAC-SHA256 code must be generated using a secret key that was generated with your API key. This code must be converted to it's hexadecimal representation (64 uppercase characters).

                Example (Python):
                message = nonce + client_id + api_key
                signature = hmac.new(API_SECRET, msg=message, digestmod=hashlib.sha256).hexdigest().upper()*/

            //string signature = CreateToken(Nonce + ClientID + APIKey, SecretKey);

            string msg = string.Format("{0}{1}{2}", Nonce, ClientID, APIKey);

            string signature = ByteArrayToString(SignHMACSHA256(SecretKey, StringToByteArray(msg))).ToUpper();


            request.AddParameter("key", APIKey);
            request.AddParameter("nonce", Nonce);
            request.AddParameter("signature", signature);

        }



        public static byte[] SignHMACSHA256(String key, byte[] data)
        {
            HMACSHA256 hashMaker = new HMACSHA256(Encoding.ASCII.GetBytes(key));
            return hashMaker.ComputeHash(data);
        }
    }
}
