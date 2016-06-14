using System;
using System.Globalization;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;

using BEx.ExchangeEngine.API;

namespace BEx.Exchanges.Gdax.API.Models
{
    public class AccountBalanceModel : IExchangeResponseIntermediate<Balance>
    {
        public string available { get; set; }
        public string balance { get; set; }
        public string currency { get; set; }
        public string hold { get; set; }
        public string id { get; set; }
        public string profile_id { get; set; }

        public Balance Convert(TradingPair pair)
        {
            Currency balanceCurrency;

            if (Enum.TryParse(currency.ToUpper(CultureInfo.InvariantCulture), out balanceCurrency))
            {
                return new Balance(
                    Conversion.ToDecimalInvariant(available),
                    balanceCurrency,
                    Conversion.ToDecimalInvariant(balance),
                    DateTime.UtcNow,
                    ExchangeType.Gdax,
                    Conversion.ToDecimalInvariant(hold));
            }

            return default(Balance);
        }
    }
}