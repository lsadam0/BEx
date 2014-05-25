using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BEx
{
    public abstract class Exchange
    {
        public Dictionary<CryptoCurrency, string> CryptoDepositAddresses
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int AccountInformation
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public abstract Tick GetTick();
 


    }
}
