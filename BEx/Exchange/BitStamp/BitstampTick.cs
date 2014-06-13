using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BEx.BitStampSupport;

namespace BEx
{
    public class BitstampTick : Tick
    {
        public Decimal VolumeWeightedAveragePrice
        {
            get;
            set;
        }

        internal BitstampTick(BitstampTickJSON source, Currency baseC, Currency counterC) : base(source, baseC, counterC)
        {

            this.VolumeWeightedAveragePrice = Convert.ToDecimal(source.vwap);
        }
    }
}
