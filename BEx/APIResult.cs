using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class APIResult
    {
        public DateTime Timestamp
        {
            get;
            set;
        }

        internal APIResult()
        {
            Timestamp = DateTime.Now;
        }
    }
}
