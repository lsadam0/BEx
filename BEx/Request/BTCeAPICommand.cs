using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
{
 /*   
    public class BTCeAPICommand : APICommand
    {
        public override string ResolvedRelativeURI 
        {

            get
            {
                string res = RelativeURI;

                foreach (KeyValuePair<string, string> pair in QueryStringArgs)
                {
                    res = res.Replace("{" + pair.Key + "}", pair.Value.ToLower());
                }

                return res;
            }
        }

        public BTCeAPICommand(APICommand source)
        {
            this.QueryStringArgs = source.QueryStringArgs;
            this.BaseCurrency = source.BaseCurrency;
            this.CounterCurrency = source.CounterCurrency;
            this.HttpMethod = source.HttpMethod;
            this.IsCurrencyPairSupported = source.IsCurrencyPairSupported;
            this.RelativeURI = source.RelativeURI;
            this.RequiresAuthentication = source.RequiresAuthentication;
        }
    }*/
}
