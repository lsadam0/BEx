using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BEx.BTCeSupport;

namespace BEx
{
    public class BTCe : Exchange<BTCeTickJSON, BTCeTransactionsJSON, BTCeOrderBookJSON>
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
        /*
        #region GetOrderBook

        public override OrderBook GetOrderBook()
        {
            return GetOrderBook(Currency.BTC, Currency.USD);
        }

        public override OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency)
        {

            BTCeAPICommand toExecute = new BTCeAPICommand(APICommandCollection["OrderBook"]);

            toExecute.BaseCurrency = baseCurrency;
            toExecute.CounterCurrency = counterCurrency;

            BTCeOrderBookJSON r = ExecuteCommand<BTCeOrderBookJSON>(toExecute);

            return r.ToOrderBook(baseCurrency, counterCurrency);

        }

        #endregion

        
         */

        internal override void SetParameters(APICommand command)
        {
            
        }

    }
}
