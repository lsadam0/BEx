// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine.BitStamp;

namespace BEx
{
    public sealed class BitStamp : Exchange
    {
        public BitStamp()
            : base(
                new BitStampConfiguration(),
                new BitStampCommandFactory(),
                BitStampErrorInterpreter.GetInterpreter())
        {
        }

        public BitStamp(string apiKey, string secretKey, string clientId)
            : base(
                new BitStampConfiguration(),
                new BitStampCommandFactory(),
                BitStampErrorInterpreter.GetInterpreter(),
                new BitStampAuthenticator(apiKey, secretKey, clientId))
        {
        }
    }
}