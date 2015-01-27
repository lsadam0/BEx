using System;

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
