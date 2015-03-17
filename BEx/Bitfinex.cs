using BEx.CommandProcessing;
using BEx.ExchangeSupport;
using BEx.ExchangeSupport.BitfinexSupport;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BEx
{
    public sealed class Bitfinex : Exchange
    {
        public Bitfinex()
            : base(new BitfinexConfiguration(), new BitfinexCommandFactory(), ExchangeType.BitFinex)
        {
            Authenticator = new BitfinexAuthenticator(base.Configuration);
        }

        public Bitfinex(BitfinexConfiguration configuration)
            : base(configuration, new BitfinexCommandFactory(), ExchangeType.BitFinex)
        {
            Authenticator = new BitfinexAuthenticator(base.Configuration);
        }

        public Bitfinex(string apiKey, string secret)
            : base(new BitfinexConfiguration(apiKey, secret), new BitfinexCommandFactory(), ExchangeType.BitFinex)
        {
            VerifyCredentials(apiKey, secret);
            Authenticator = new BitfinexAuthenticator(base.Configuration);
        }

        protected internal override APIError DetermineErrorCondition(string message)
        {
            APIError error = null;

            string errorMessage = ExtractMessage(message);

            string loweredMessage = errorMessage.ToLower();
            if (loweredMessage.Contains("not enough balance"))
            {
                error = new APIError(errorMessage, BExErrorCode.InsufficientFunds, ExchangeType.BitFinex);
            }
            else if (loweredMessage.Contains("the given x-bfx-apikey") || loweredMessage.Contains("invalid x-bfx-signature"))
            {
                error = new APIError(errorMessage, BExErrorCode.Authorization, ExchangeType.BitFinex);
            }

            if (error == null)
            {
                error = new APIError(message, BExErrorCode.Unknown, ExchangeType.BitFinex);
            }

            return error;
        }

        internal string ExtractMessage(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                StringBuilder res = new StringBuilder();
                // this works for auth errors
                JObject error = JObject.Parse(content);

                // for other errors

                try
                {
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
                }
                catch (Exception)
                {
                    res.Append(error.ToString());
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

        private void VerifyCredentials(string apiKey, string secretKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ExchangeAuthorizationException("Invalid APIKey specified.");
            else
                this.APIKey = apiKey;

            if (string.IsNullOrEmpty(secretKey))
                throw new ExchangeAuthorizationException("Invalid SecretKey specified.");
            else
                this.SecretKey = secretKey;
        }
    }
}