using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using RestSharp;
using BEx.BitFinexSupport;
using BEx.Common;

namespace BEx
{
    public class Bitfinex : Exchange
    {

        public Bitfinex()
            : base("BitFinex.xml")
        {

        }



        public override Tick GetTick()
        {
            return base.GetTick<BitfinexTickJSON>(Currency.BTC, Currency.USD);
        }

        public override Tick GetTick(Currency baseCurrency, Currency counterCurrency)
        {
            return base.GetTick<BitfinexTickJSON>(baseCurrency, counterCurrency);
        }

        public override OrderBook GetOrderBook()
        {

            return base.GetOrderBook<BitFinexOrderBookJSON>(Currency.BTC, Currency.USD);
        }

        public override OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency)
        {
            return base.GetOrderBook<BitFinexOrderBookJSON>(baseCurrency, counterCurrency);
        }

        public override List<Transaction> GetTransactions()
        {
            return base.GetTransactions<BitFinexTransactionJSON>(Currency.BTC, Currency.USD);
        }

        public override List<Transaction> GetTransactions(Currency baseCurrency, Currency counterCurrency)
        {
            return base.GetTransactions<BitFinexTransactionJSON>(baseCurrency, counterCurrency);
        }


        public override AccountBalance GetAccountBalance()
        {
            return base.GetAccountBalance<List<BitFinexAccountBalanceJSON>>(Currency.BTC, Currency.USD);
        }


        protected override void CreateSignature(RestRequest request, APICommand command)
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

          //  string payloadFrame = "{\"request\": \"/v1/balances\",\"nonce\": \"" + Nonce + "\"}"; //ie. {"request": "/v1/balances","nonce": "1402207693893"}
           StringBuilder payload = new StringBuilder();

            payload.Append("{");
            payload.Append("\"request\": \"" +  command.ResolvedRelativeURI + "\",");
            payload.Append("\"nonce\": \"" + Nonce + "\"");
            payload.Append("}");

            string payload64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload.ToString()));

            request.AddHeader("X-BFX-PAYLOAD", payload64);

            

            using (HMACSHA384 hasher = new HMACSHA384(Encoding.UTF8.GetBytes(SecretKey)))
            {

               byte[] hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(payload64));

                
               request.AddHeader("X-BFX-SIGNATURE", BitConverter.ToString(hashBytes).Replace("-", "").ToLower());
            }


            
        }

        protected byte[] SignHMACSHA256(String key, byte[] data)
        {
            // borrowed
            HMACSHA384 hashMaker = new HMACSHA384(Encoding.ASCII.GetBytes(key));
            return hashMaker.ComputeHash(data);
        }

        internal override void SetParameters(APICommand command)
        {
            /*
            switch (command.ID)
            {
                case "Transactions":
                    DateTime sinceThisDate = DateTime.Now.AddHours(-1);
                    command.Parameters["timestamp"] = UnixTime.DateTimeToUnixTimestamp(sinceThisDate).ToString();
                    break;
                default:
                    break;
            }*/
        }
    }
}
