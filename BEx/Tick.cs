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
        internal Tick(BitStampSupport.BitstampTickJSON source, Currency baseC, Currency counterC)
        {

            this.Ask = Convert.ToDecimal(source.ask);
            this.Bid = Convert.ToDecimal(source.bid);
            this.High = Convert.ToDecimal(source.high);
            this.Last = Convert.ToDecimal(source.last);
            this.Low = Convert.ToDecimal(source.low);
            //this.TimeStamp = Convert.tol
            this.Volume = Convert.ToDecimal(source.volume);
            //this.VWAP = Convert.ToDecimal(source.vwap);
            this.BaseCurrency = baseC;
            this.CounterCurrency = counterC;
        }

        internal Tick(BTCeSupport.BTCeTickJSON source, Currency baseC, Currency counterC)
        {

            this.Bid = Convert.ToDecimal(source.Ticker.Sell);
            this.Ask = Convert.ToDecimal(source.Ticker.Buy);
            this.High = Convert.ToDecimal(source.Ticker.High);
            this.Last = Convert.ToDecimal(source.Ticker.Last);
            this.Low = Convert.ToDecimal(source.Ticker.Low);
            this.Volume = Convert.ToDecimal(source.Ticker.Vol);

            this.BaseCurrency = baseC;
            this.CounterCurrency = counterC;
        }

        internal Tick(BitFinexSupport.BitfinexTickJSON source, Currency baseC, Currency counterC)
        {
            this.Ask = Convert.ToDecimal(source.Ask);
            this.BaseCurrency = baseC;
            this.Bid = Convert.ToDecimal(source.Bid);
            this.CounterCurrency = counterC;
            this.High = Convert.ToDecimal(source.High);
            this.Last = Convert.ToDecimal(source.LastPrice);
            this.Low = Convert.ToDecimal(source.Low);
            this.Volume = Convert.ToDecimal(source.Volume);
            //this.VWAP = source.
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
