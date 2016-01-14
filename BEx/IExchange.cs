using System.Collections.Immutable;

namespace BEx
{
    public interface IExchange
    {
        CurrencyTradingPair DefaultPair { get; }
        ExchangeType ExchangeSourceType { get; }
        ImmutableHashSet<Currency> SupportedCurrencies { get; }
        ImmutableHashSet<CurrencyTradingPair> SupportedTradingPairs { get; }

        Confirmation CancelOrder(Order toCancel);

        Confirmation CancelOrder(int id);

        Order CreateBuyLimitOrder(decimal amount, decimal price);

        Order CreateBuyLimitOrder(CurrencyTradingPair pair, decimal amount, decimal price);

        Order CreateSellLimitOrder(decimal amount, decimal price);

        Order CreateSellLimitOrder(CurrencyTradingPair pair, decimal amount, decimal price);

        AccountBalance GetAccountBalance();

        DepositAddress GetDepositAddress(Currency toDeposit);

        OpenOrders GetOpenOrders();

        OpenOrders GetOpenOrders(CurrencyTradingPair pair);

        OrderBook GetOrderBook();

        OrderBook GetOrderBook(CurrencyTradingPair pair);

        Tick GetTick();

        Tick GetTick(CurrencyTradingPair pair);

        Transactions GetTransactions();

        Transactions GetTransactions(CurrencyTradingPair pair);

        UserTransactions GetUserTransactions();

        UserTransactions GetUserTransactions(CurrencyTradingPair pair);

        bool IsTradingPairSupported(CurrencyTradingPair pair);
    }
}