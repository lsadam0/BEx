// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine.BitStampSupport;

namespace BEx
{
    public sealed class BitStamp : Exchange
    {
        public BitStamp()
            : base(new BitStampConfiguration(), new BitStampCommandFactory(), ExchangeType.BitStamp)
        {
        }

        public BitStamp(string apiKey, string secretKey, string clientId)
            : base(new BitStampConfiguration(apiKey, clientId, secretKey), new BitStampCommandFactory(), ExchangeType.BitStamp)
        {
            Authenticator = new BitStampAuthenticator(Configuration);
        }
    }
}