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
            return null;
        }

        /// <summary>
        /// Return the current BTC/USD Tick
        /// </summary>
        /// <returns></returns>
        public override Tick GetTick()
        {
            Tick res;

            res = new Tick(ExecuteCommand<BTCeTickJSON>(APICommandCollection["Tick"]));

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
