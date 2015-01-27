using System.Collections.Generic;

namespace BEx
{
    public class OpenOrders : APIResult
    {
        public List<Order> Orders
        {
            get;
            set;
        }

        internal OpenOrders()
            : base()
        {
            Orders = new List<Order>();
        }

        internal OpenOrders(List<Order> orders)
            : base()
        {
            Orders = orders;
        }
    }
}