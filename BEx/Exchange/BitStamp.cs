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

        internal override void SetParameters(APICommand command)
        {
            
        }

    }
}
