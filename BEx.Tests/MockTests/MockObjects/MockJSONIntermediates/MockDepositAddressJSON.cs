// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine;
using Newtonsoft.Json;
using System;

namespace BEx.UnitTests.MockTests.MockObjects.MockJSONIntermediates
{
    internal class MockDepositAddressJSON : IExchangeResponse<DepositAddress>
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        public DepositAddress Convert(CurrencyTradingPair pair)
        {
            return new DepositAddress(Address, DateTime.UtcNow, pair.BaseCurrency, ExchangeType.Mock);
            //  return new DepositAddress(Address, DateTime.UtcNow,  ExchangeType.Mock, pair);
        }
    }
}