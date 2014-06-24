using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Xml;

using RestSharp;
namespace BEx
{
    public delegate bool IsCurrencyPairSupportedDelegate(Currency b, Currency c);

    [Serializable]
    public class APICommand
    {
        public IsCurrencyPairSupportedDelegate IsCurrencyPairSupported;

        public string ID
        {
            get;
            set;
        }

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

        public Dictionary<string, string> QueryStringArgs
        {
            get;
            set;
        }

        public Dictionary<string, string> Parameters
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

                foreach (KeyValuePair<string, string> pair in QueryStringArgs)
                {
                    res = res.Replace("{" + pair.Key + "}", pair.Value);
                }

                return res;
            }
        }

        public virtual string MessageBody
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                int x = 0;
                foreach (KeyValuePair<string, string> pair in Parameters)
                {
                    if (x > 0)
                        sb.Append("&");

                    sb.AppendFormat("{0}={1}", pair.Key, pair.Value);
                }

                return sb.ToString();
            }
        }

        public APICommand()
        {
            QueryStringArgs = new Dictionary<string, string>();
            Parameters = new Dictionary<string, string>();
        }

        public APICommand(XElement commandToLoad)
        {

            QueryStringArgs = new Dictionary<string, string>();
            Parameters = new Dictionary<string, string>();

            foreach (XElement c in commandToLoad.Elements())
            {

                if (c.Name == "Method")
                {
                    if (c.Value == "GET")
                    {
                        HttpMethod = Method.GET;
                    }
                    else
                        HttpMethod = Method.POST;
                }

                if (c.Name == "RelativeURL")
                {
                    RelativeURI = c.Value;
                }

                if (c.Name == "RequiresAuthentication")
                {
                    RequiresAuthentication = Convert.ToBoolean(c.Value);
                }

                if (c.Name == "args")
                    LoadArgs(c);

            }

            ID = commandToLoad.Attribute("ID").Value;
        }

        private void LoadArgs(XElement args)
        {
            foreach (XElement arg in args.Elements())
            {
                string type = arg.Attribute("type") != null ? arg.Attribute("type").Value : null;
                string id = arg.Attribute("ID").Value;

                string defaultValue = arg.Value ?? "";

                if (string.IsNullOrEmpty(type) || type == "query")
                {
                    QueryStringArgs.Add(id, defaultValue);
                }
                else
                {
                    Parameters.Add(id, defaultValue ?? "");
                }
            }

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

                if (!QueryStringArgs.ContainsKey("BaseCurrency"))
                    QueryStringArgs.Add("BaseCurrency", baseC.ToString());
                else
                    QueryStringArgs["BaseCurrency"] = baseC.ToString();
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

                if (!QueryStringArgs.ContainsKey("CounterCurrency"))
                    QueryStringArgs.Add("CounterCurrency", counterC.ToString());
                else
                    QueryStringArgs["CounterCurrency"] = counterC.ToString();
            }
        }

    }
}
