

namespace BEx
{
    public class BitFinexTransaction : Transaction
    {

        public string ExchangeSource
        {
            get;
            set;
        }

       internal BitFinexTransaction() : base()
        {
            
        }
    }
}
