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

        public override Tick GetTick()
        {
            throw new NotImplementedException();

            return null;
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
