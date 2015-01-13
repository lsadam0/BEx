using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class OpenOrders : APIResult
    {
        public List<Order> Orders
        {
            get;
            set;
        }

        internal OpenOrders() : base()
        {
            Orders = new List<Order>();
        }

        internal OpenOrders(List<Order> orders) : base()
        {
            Orders = orders;
        }
    }
}
