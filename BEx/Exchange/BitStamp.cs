using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using RestSharp;
using BEx.BitStampSupport;

namespace BEx
{
    public class BitStamp : Exchange<BitstampTickJSON, BitstampTransactionJSON, BitstampOrderBookJSON>
    {

        public BitStamp()
            : base("Bitstamp.xml")
        {
            
            
        }

        /*
        #region GetOrderBook

        /// <summary>
        /// Get the BTC/USD Order Book
        /// </summary>
        /// <returns></returns>
        public override OrderBook GetOrderBook()
        {
            return GetOrderBook(Currency.BTC, Currency.USD);
        }

        public override OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency)
        {
            OrderBook res;

            APICommand toExecute = APICommandCollection["OrderBook"];

            res = ExecuteCommand<BitstampOrderBookJSON>(toExecute).ToOrderbook(Currency.BTC, Currency.USD);

            return res;
        }

        #endregion

       
         * 
         */

        internal override void SetParameters(APICommand command)
        {
            
        }

    }
}
