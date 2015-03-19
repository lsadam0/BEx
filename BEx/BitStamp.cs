﻿using BEx.ExchangeSupport.BitStampSupport;
using System.Text.RegularExpressions;

namespace BEx
{
    public sealed class BitStamp : Exchange
    {
        private Regex errorId;

        public BitStamp()
            : base(new BitStampConfiguration(), new BitStampCommandFactory(), ExchangeType.BitStamp)
        {
        }

        public BitStamp(string apiKey, string secretKey, string clientId)
            : base(new BitStampConfiguration(apiKey, clientId, secretKey), new BitStampCommandFactory(), ExchangeType.BitStamp)
        {
            Authenticator = new BitStampAuthenticator(Configuration);

            errorId = new Regex("^{\"error\":");// \"API key not found\"}");
        }
    }
}