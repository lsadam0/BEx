using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
    public class APIError : APIResult
    {

        public string Message
        {
            get;
            set;
        }

        public APIError() : base()
        {

        }
    }
}
