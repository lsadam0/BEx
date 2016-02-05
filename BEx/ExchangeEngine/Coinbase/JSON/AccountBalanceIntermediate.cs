using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEx.ExchangeEngine.Utilities;

namespace BEx.ExchangeEngine.Coinbase.JSON
{
    public class AccountBalanceIntermediate : IExchangeResponseIntermediate<Balance>
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
                    ExchangeType.Coinbase,
                    Conversion.ToDecimalInvariant(hold));
            }

            return default(Balance);
        }
    }


}





