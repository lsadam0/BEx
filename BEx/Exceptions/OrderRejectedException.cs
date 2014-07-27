using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx.Exceptions
{
    //class OrderRejectedException
    //{
    //}

    public class OrderRejectedException : Exception
    {
        public OrderRejectedException()
        {
        }

        public OrderRejectedException(string message)
            : base(message)
        {
        }

        public OrderRejectedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
