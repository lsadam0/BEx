using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEx.ExchangeEngine.Utilities;

namespace BEx.ExchangeEngine.Coinbase.JSON
{
    internal class TickIntermediate : IExchangeResponseIntermediate<Tick>
    {
        public int trade_id { get; set; }
        public string price { get; set; }
        public string size { get; set; }
        public string bid { get; set; }
        public string ask { get; set; }
        public string volume { get; set; }
        public DateTime time { get; set; }

        public Tick Convert(TradingPair pair)
        {
            return null;
        }
    }
}
