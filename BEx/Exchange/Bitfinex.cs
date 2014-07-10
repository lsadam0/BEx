using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BEx.BitFinexSupport;
using BEx.Common;

namespace BEx
{
    public class Bitfinex : Exchange<BitfinexTickJSON, BitFinexTransactionJSON, BitFinexOrderBookJSON>
    {

        public Bitfinex() : base("BitFinex.xml")
        {

        }

        /*
        #region GetOrderBook

        public override OrderBook GetOrderBook()
        {
            return GetOrderBook(Currency.BTC, Currency.USD);
        }

        public override OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency)
        {
            OrderBook res;

            APICommand toExecute = APICommandCollection["OrderBook"];

            toExecute.BaseCurrency = baseCurrency;
            toExecute.CounterCurrency = counterCurrency;

            res = ExecuteCommand<BitFinexOrderBookJSON>(toExecute).ToOrderBook(baseCurrency, counterCurrency);

            return res;
        }

        #endregion

       */



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
