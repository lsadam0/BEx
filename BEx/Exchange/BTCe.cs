using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BEx.BTCeSupport;

namespace BEx
{
    public class BTCe : Exchange
    {
        public BTCe() : base("BTCe.xml")
        {
            

        }



        public override Tick GetTick(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res;

            BTCeAPICommand tickCommand = new BTCeAPICommand(APICommandCollection["Tick"]);

            tickCommand.BaseCurrency = baseCurrency;
            tickCommand.CounterCurrency = counterCurrency;

            res = ExecuteCommand<BTCeTickJSON>(tickCommand).ToTick(baseCurrency, counterCurrency);

            return res;
        }

        /// <summary>
        /// Return the current BTC/USD Tick
        /// </summary>
        /// <returns></returns>
        public Tick GetTick()
        {
            Tick res;

            BTCeAPICommand toExecute = new BTCeAPICommand(APICommandCollection["Tick"]);

            toExecute.BaseCurrency = Currency.BTC;
            toExecute.CounterCurrency = Currency.USD;

            res = ExecuteCommand<BTCeTickJSON>(toExecute).ToTick(Currency.BTC, Currency.USD);

            return res;
        }


        public override OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency)
        {
            throw new NotImplementedException();
            
        }

        public override List<Transaction> GetTransactions(Currency baseCurrency, Currency counterCurrency)
        {
            throw new NotImplementedException();
            
        }
        
    }
}
