using System;
using BEx.ExchangeEngine.API;
using BEx.ExchangeEngine.Utilities;

namespace BEx.Exchanges.Gdax.API.Models
{
    internal class UserTransactionModel : IExchangeResponseIntermediate<UserTransaction>
    {
        public int trade_id { get; set; }
        public string product_id { get; set; }
        public string price { get; set; }
        public string size { get; set; }
        public string order_id { get; set; }
        public DateTime created_at { get; set; }
        public string liquidity { get; set; }
        public string fee { get; set; }
        public bool settled { get; set; }
        public string side { get; set; }

        public UserTransaction Convert(TradingPair pair)
        {
            return new UserTransaction(
                Conversion.ToDecimalInvariant(size),
                Conversion.ToDecimalInvariant(price),
                (long)created_at.ToUnixTime(),
                //Conversion.ToDecimalInvariant(size) * Conversion.ToDecimalInvariant(price),
                (1/Conversion.ToDecimalInvariant(size)) * Conversion.ToDecimalInvariant(price),
                
                order_id,
                Conversion.ToDecimalInvariant(fee),
                pair.BaseCurrency,
                pair,
                ExchangeType.Gdax,
                side == "buy" ? OrderType.Buy : OrderType.Sell,
                trade_id);
        }
    }
}