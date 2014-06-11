using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEx
{
    public class BTCe : Exchange
    {
        public BTCe() : base("BTCe.xml")
        {
            

        }
        


        public Tick GetTick(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res;

            BTCeAPICommand tickCommand = new BTCeAPICommand(APICommandCollection["Tick"]);

            tickCommand.BaseCurrency = baseCurrency;
            tickCommand.CounterCurrency = counterCurrency;

            res = new Tick(ExecuteCommand<BTCeTickJSON>(tickCommand), baseCurrency, counterCurrency);

            return res;
        }

        /// <summary>
        /// Return the current BTC/USD Tick
        /// </summary>
        /// <returns></returns>
        public override Tick GetTick()
        {
            Tick res;

            BTCeAPICommand toExecute = new BTCeAPICommand(APICommandCollection["Tick"]);

            toExecute.BaseCurrency = Currency.BTC;
            toExecute.CounterCurrency = Currency.USD;

            res = new Tick(ExecuteCommand<BTCeTickJSON>(toExecute), Currency.BTC, Currency.USD);

            return res;
        }


        public override OrderBook GetOrderBook()
        {
            throw new NotImplementedException();
            return null;
        }

        public override List<Transaction> GetTransactions()
        {
            throw new NotImplementedException();
            return null;
        }
        
    }
}
