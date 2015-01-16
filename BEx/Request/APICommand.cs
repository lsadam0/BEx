using RestSharp;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BEx
{


    public delegate string CurrencyFormatterDelegate(string currency);

    [Serializable]
    public class APICommand
    {

        public CurrencyFormatterDelegate CurrencyFormatter;
        
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

        public Dictionary<string, string> Parameters
        {

            get;
            set;
        }

        public bool ReturnsValueType = false;

        public string GetResolvedRelativeURI(Currency baseC, Currency counterC)
        {
            string res = RelativeURI;

            res = res.Replace("{BaseCurrency}", GetCurrencyFormat(baseC.ToString()));
            res = res.Replace("{CounterCurrency}", GetCurrencyFormat(counterC.ToString()));

            return res;
        }

        private string GetCurrencyFormat(string currency)
        {

            if (CurrencyFormatter != null)
                return CurrencyFormatter(currency);

            else return currency;
        }

        public APICommand()
        {
            Parameters = new Dictionary<string, string>();
        }

        public APICommand(XElement commandToLoad)
        {
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

                if (c.Name == "ReturnsValueType")
                {
                    ReturnsValueType = Convert.ToBoolean(c.Value);
                }

                

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
                    //QueryStringArgs.Add(id, defaultValue);
                }
                else
                {
                    Parameters.Add(id, defaultValue ?? "");
                }
            }

        }



    }
}
