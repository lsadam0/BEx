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

        public Currency BaseCurrency
        {
            get;
            set;
        }

        public Currency CounterCurrency
        {
            get;
            set;

        }

        internal Tick()
        {

        }

    



        /*
        public static bool operator ==(Tick a, Tick b)
        {
            if (
                (a.Ask == b.Ask)
                &&
                (a.BaseCurrency == b.BaseCurrency)
                &&
                (a.Bid == b.Bid)
                &&
                (a.CounterCurrency == b.CounterCurrency)
                &&
                (a.High == b.High)
                &&
                (a.Last == b.Last)
                &&
                (a.Low == b.Low)
                &&
                (a.Volume == b.Volume)
                )
            {
                return true;
            }
            else
            {
                return false;
            }


        }*/
    }
}
