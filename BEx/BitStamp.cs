// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine.BitStamp;

namespace BEx
{
    public sealed class BitStamp : Exchange
    {
        public BitStamp()
            : base(
                BitStampConfiguration.Singleton,
                BitStampCommandFactory.Singleton)
        {
        }

        public BitStamp(string apiKey, string secretKey, string clientId)
            : base(
               BitStampConfiguration.Singleton,
                BitStampCommandFactory.Singleton,
                new BitStampAuthenticator(apiKey, secretKey, clientId))
        {
        }
    }
}