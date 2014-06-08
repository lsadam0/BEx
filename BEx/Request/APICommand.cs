using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;


using RestSharp;
namespace BEx
{
    [Serializable]
    public class APICommand
    {
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



        public string ResolvedRelativeURI
        {

            get
            {
                string res = RelativeURI;
                
                foreach (KeyValuePair<string, string> pair in Args)
                {
                    res = res.Replace(pair.Key, pair.Value);
                }

                return res;
            }
        }

        public APICommand()
        {
            Args = new Dictionary<string, string>();
        }

    }
}
