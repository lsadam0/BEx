// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BEx.Tests
{
    public class ExchangeCommandVerification
    {
        public ExchangeCommandVerification(Exchange testCandidate)
        {
            TestCandidate = testCandidate;
        }

        protected Exchange TestCandidate { get; set; }

        public void CreateAndVerifyBuyOrder()
        {
            var toVerify = TestCandidate.CreateBuyLimitOrder(1m, 5m);

            ResponseVerification.VerifyOrder(
                toVerify,
                TestCandidate.DefaultPair,
                OrderType.Buy,
                TestCandidate.ExchangeSourceType);
        }

        public void CreateAndVerifySellOrder()
        {
            var toVerify = TestCandidate.CreateSellLimitOrder(0.02m, 10000m);

            ResponseVerification.VerifyOrder(
                toVerify,
                TestCandidate.DefaultPair,
                OrderType.Sell,
                TestCandidate.ExchangeSourceType);
        }

        public void RetrieveAndVerifyAccountBalance()
        {
            var toVerify = TestCandidate.GetAccountBalance();

            ResponseVerification.VerifyAccountBalance(
                toVerify,
                TestCandidate.ExchangeSourceType,
                TestCandidate.SupportedCurrencies);
        }

        public void RetrieveAndVerifyOpenOrders()
        {
            var toVerify = TestCandidate.GetOpenOrders();

            ResponseVerification.VerifyOpenOrders(
                toVerify,
                TestCandidate.ExchangeSourceType,
                TestCandidate.DefaultPair);
        }

        public void RetrieveAndVerifyOrderBook(TradingPair pair)
        {
            var toVerify = TestCandidate.GetOrderBook(pair);

            ResponseVerification.VerifyOrderBook(
                toVerify,
                TestCandidate.DefaultPair,
                TestCandidate.ExchangeSourceType);
        }

        public void RetrieveAndVerifyTick(TradingPair pair)
        {
            var toVerify = TestCandidate.GetTick(pair);

            ResponseVerification.VerifyTick(toVerify, pair, TestCandidate.ExchangeSourceType);
        }

        public void RetrieveAndVerifyTransactions(TradingPair pair)
        {
            var toVerify = TestCandidate.GetTransactions(pair);

            ResponseVerification.VerifyTransactions(
                toVerify,
                TestCandidate.ExchangeSourceType,
                TestCandidate.DefaultPair);
        }

        public void RetrieveAndVerifyUserTransactions(TradingPair pair)
        {
            var toVerify = TestCandidate.GetUserTransactions(pair);

            ResponseVerification.VerifyUserTransactions(
                toVerify,
                TestCandidate.ExchangeSourceType,
                TestCandidate.DefaultPair);
        }

        public void RetrieveAnVerifyDayRange(TradingPair pair)
        {
            var toVerify = TestCandidate.Get24HrStats(pair);

            ResponseVerification.VerifyDayRange(toVerify, TestCandidate.ExchangeSourceType);
        }
    }
}