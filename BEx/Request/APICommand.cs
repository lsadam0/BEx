using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;


using RestSharp;
namespace BEx
{
    public delegate bool IsCurrencyPairSupportedDelegate(Currency b, Currency c);

    [Serializable]
    public class APICommand
    {
        public IsCurrencyPairSupportedDelegate IsCurrencyPairSupported;


        public Method HttpMethod
        {
            get;
            set;
        }

        public bool RequiresAuthentication
        {
            get;
            set;
        }

        public string RelativeURI
        {
            get;
            set;
        }

        public Dictionary<string, string> Args
        {
            get;
            set;
        }

        private bool CheckCurrencyPairSupport(Currency baseC, Currency counterC)
        {
            bool res = true;
            if (IsCurrencyPairSupported != null)
            {
                res = IsCurrencyPairSupported(baseC, counterC);
            }

            return res;
        }

        public virtual string ResolvedRelativeURI
        {

            get
            {
                string res = RelativeURI;

                foreach (KeyValuePair<string, string> pair in Args)
                {
                    res = res.Replace("{" + pair.Key + "}", pair.Value);
                }

                return res;
            }
        }

        public APICommand()
        {
            Args = new Dictionary<string, string>();
        }

        private Currency? baseC = null;
        public Currency? BaseCurrency
        {
            get
            {
                return baseC;
            }
            set
            {
                baseC = value;

                if (!Args.ContainsKey("BaseCurrency"))
                    Args.Add("BaseCurrency", baseC.ToString());
                else
                    Args["BaseCurrency"] = baseC.ToString();
            }
        }

        private Currency? counterC;
        public Currency? CounterCurrency
        {
            get
            {
                return counterC;
            }
            set
            {
                counterC = value;

                if (!Args.ContainsKey("CounterCurrency"))
                    Args.Add("CounterCurrency", counterC.ToString());
                else
                    Args["CounterCurrency"] = counterC.ToString();
            }
        }

    }
}
