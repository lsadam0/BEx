using System.Collections.Immutable;

namespace BEx
{
    public interface IExchange
    {
        TradingPair DefaultPair { get; }
        ExchangeType ExchangeSourceType { get; }
        ImmutableHashSet<Currency> SupportedCurrencies { get; }
        ImmutableHashSet<TradingPair> SupportedTradingPairs { get; }

        Confirmation CancelOrder(Order toCancel);

        Confirmation CancelOrder(string id);

        Order CreateBuyLimitOrder(decimal amount, decimal price);

        Order CreateBuyLimitOrder(TradingPair pair, decimal amount, decimal price);

        Order CreateSellLimitOrder(decimal amount, decimal price);

        Order CreateSellLimitOrder(TradingPair pair, decimal amount, decimal price);

        AccountBalance GetAccountBalance();

        DepositAddress GetDepositAddress(Currency toDeposit);

        OpenOrders GetOpenOrders();

        OpenOrders GetOpenOrders(TradingPair pair);

        OrderBook GetOrderBook();

        OrderBook GetOrderBook(TradingPair pair);

        Tick GetTick();

        Tick GetTick(TradingPair pair);

        Transactions GetTransactions();

        Transactions GetTransactions(TradingPair pair);

        UserTransactions GetUserTransactions();

        UserTransactions GetUserTransactions(TradingPair pair);

        bool IsTradingPairSupported(TradingPair pair);
    }
}