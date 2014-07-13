using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEx
{
    public class Tick : APIResult
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

        internal Tick() : base()
        {

        }

    }
}
