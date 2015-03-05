using RestSharp;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BEx
{
    [Serializable]
    internal class APICommand
    {
        public bool ReturnsValueType = false;

        internal APICommand()
        {
            Parameters = new Dictionary<string, string>();
        }

        internal APICommand(XElement commandToLoad)
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

        public Method HttpMethod
        {
            get;
            private set;
        }

        public string ID
        {
            get;
            private set;
        }

        public Dictionary<string, string> Parameters
        {
            get;
            private set;
        }

        public string RelativeURI
        {
            get;
            private set;
        }

        public bool RequiresAuthentication
        {
            get;
            private set;
        }

        public string GetResolvedRelativeURI(Currency baseC, Currency counterC)
        {
            string res = RelativeURI;

            res = res.Replace("{BaseCurrency}", baseC.ToString());
            res = res.Replace("{CounterCurrency}", counterC.ToString());

            return res;
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