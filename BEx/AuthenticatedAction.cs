using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEx
{
    public abstract class AuthenticatedAction : Action
    {
        public int APIKey
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int Nonce
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int Signature
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
