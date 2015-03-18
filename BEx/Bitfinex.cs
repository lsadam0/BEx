using BEx.ExchangeSupport.BitfinexSupport;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BEx
{
    public sealed class Bitfinex : Exchange
    {
        public Bitfinex()
            : base(new BitfinexConfiguration(), new BitfinexCommandFactory(), ExchangeType.Bitfinex)
        {
        }

        public Bitfinex(string apiKey, string secret)
            : base(new BitfinexConfiguration(apiKey, secret), new BitfinexCommandFactory(), ExchangeType.Bitfinex)
        {
            Authenticator = new BitfinexAuthenticator(base.Configuration);
        }

        protected internal override ApiError DetermineErrorCondition(string message)
        {
            ApiError error = null;

            string errorMessage = ExtractMessage(message);

            string loweredMessage = errorMessage.ToLower(CultureInfo.CurrentCulture);
            if (loweredMessage.Contains("not enough balance"))
            {
                error = new ApiError(errorMessage, BExErrorCode.InsufficientFunds, ExchangeType.Bitfinex);
            }
            else if (loweredMessage.Contains("the given x-bfx-apikey") || loweredMessage.Contains("invalid x-bfx-signature"))
            {
                error = new ApiError(errorMessage, BExErrorCode.Authorization, ExchangeType.Bitfinex);
            }

            if (error == null)
            {
                error = new ApiError(message, BExErrorCode.Unknown, ExchangeType.Bitfinex);
            }

            return error;
        }

        internal static string ExtractMessage(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                StringBuilder res = new StringBuilder();
                // this works for auth errors
                JObject error = JObject.Parse(content);

                // for other errors

                if (error["message"] is JValue)
                {
                    JValue v = (JValue)error["message"];

                    res.Append(v.Value.ToString());
                }
                else
                {
                    IDictionary<string, JToken> errors = (JObject)error["message"];

                    foreach (KeyValuePair<string, JToken> er in errors)
                    {
                        foreach (JToken token in er.Value.Values())
                        {
                            res.Append(((JValue)token).Value.ToString());
                        }

                        //res.Append(er.Value.ToString().Replace("{\"error\":", "").Replace("{\"__all__\": [\"", "").Replace("\"]}}", "").Replace("[", "").Replace("]", "").Replace("\"", "").Trim());
                    }
                }

                return res.ToString();//Regex.Replace(res.ToString(), @"\t|\n|\r", "");
            }
            else
                return "The Error response was empty";
        }

        protected internal override bool IsError(string content)
        {
            bool res = false;

            //if (errorId.IsMatch(content))
            //{
            //  res = true;
            //}

            return res;
        }
    }
}