using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEx
{
    public class Tick
    {

        public Decimal High
        {
            get;
            set;
        }

        public Decimal Last
        {
            get;
            set;

        }
        /*
        public long TimeStamp
        {
            get;
            set;
        }*/

        public Decimal Bid
        {
            get;
            set;
        }

        public Decimal VWAP
        {
            get;
            set;

        }

        public Decimal Volume
        {
            get;
            set;

        }

        public Decimal Low
        {
            get;
            set;
        }

        public Decimal Ask
        {
            get;
            set;

        }

        internal Tick(BitstampTickJSON source)
        {

            this.Ask = Convert.ToDecimal(source.ask);
            this.Bid = Convert.ToDecimal(source.bid);
            this.High = Convert.ToDecimal(source.high);
            this.Last = Convert.ToDecimal(source.last);
            this.Low = Convert.ToDecimal(source.low);
            //this.TimeStamp = Convert.tol
            this.Volume = Convert.ToDecimal(source.volume);
            this.VWAP = Convert.ToDecimal(source.vwap);
        }

        internal Tick(BTCeTickJSON source)
        {
            this.Bid = Convert.ToDecimal(source.ticker.sell);
            this.Ask = Convert.ToDecimal(source.ticker.buy);
            this.High = Convert.ToDecimal(source.ticker.high);
            this.Last = Convert.ToDecimal(source.ticker.last);
            this.Low = Convert.ToDecimal(source.ticker.low);
            this.Volume = Convert.ToDecimal(source.ticker.vol);
         
        }
    }
}
