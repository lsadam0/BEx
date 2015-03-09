using BEx.Request;
using RestSharp;
using System;
using System.Collections.Generic;

namespace BEx
{
    internal delegate string GetSignature();

    public abstract class Exchange
    {
        public HashSet<Currency> SupportedCurrencies;

        public HashSet<CurrencyTradingPair> SupportedTradingPairs;

        internal ExecutionEngine CommandExecutionEngine;

        protected internal Dictionary<CommandClass, ExchangeCommand> CommandCollection;

        protected Exchange(ExchangeType exchangeSourceType, string baseUrl, IExchangeCommandFactory commandFactory)
        {
            DefaultPair = new CurrencyTradingPair(Currency.BTC, Currency.USD);

            CommandCollection = commandFactory.GetCommandCollection();

            ExchangeSourceType = exchangeSourceType;

            BaseURI = new Uri(baseUrl.TrimEnd('/', '\\'));

            BuildConfiguration();

            CommandExecutionEngine = new ExecutionEngine(this);
        }

        protected Dictionary<StandardParameterType, string> PopulateCommandParameters(ExchangeCommand command, CurrencyTradingPair pair, Dictionary<StandardParameterType, string> values)
        {
            if (command.DependentParameters.Count > 0)
            {
                var res = new Dictionary<StandardParameterType, string>();

                foreach (KeyValuePair<StandardParameterType, ExchangeParameter> param in command.DependentParameters)
                {
                    string value = "";
                    switch (param.Key)
                    {
                        case (StandardParameterType.Amount):
                            //res.Add(param.Key, values[param.Key]);
                            value = values[param.Key];
                            break;

                        case (StandardParameterType.Base):
                            //res.Add(param.Key, pair.BaseCurrency.ToString());
                            value = pair.BaseCurrency.ToString();
                            break;

                        case (StandardParameterType.Counter):
                            //res.Add(param.Key, pair.CounterCurrency.ToString());
                            value = pair.CounterCurrency.ToString();
                            break;

                        case (StandardParameterType.Currency):
                            // res.Add(param.Key, pair.BaseCurrency.ToString());
                            value = pair.BaseCurrency.ToString();
                            break;

                        case (StandardParameterType.CurrencyFullName):
                            //res.Add(param.Key, pair.BaseCurrency.ToString());
                            value = pair.BaseCurrency.GetDescription();
                            break;

                        case (StandardParameterType.Id):
                            //res.Add(param.Key, values[StandardParameterType.Id]);
                            value = values[StandardParameterType.Id];
                            break;

                        case (StandardParameterType.Pair):
                            //res.Add(param.Key, pair.ToString());
                            value = pair.ToString();
                            break;

                        case (StandardParameterType.Price):
                            //res.Add(param.Key, values[param.Key]);
                            value = values[param.Key];
                            break;

                        case (StandardParameterType.TimeStamp):
                            throw new NotImplementedException();

                        case (StandardParameterType.UnixTimeStamp):
                            //res.Add(param.Key, Common.UnixTime.DateTimeToUnixTimestamp(DateTime.Now.AddHours(-1)).ToString());
                            value = Common.UnixTime.DateTimeToUnixTimestamp(DateTime.Now.AddHours(-1)).ToString();
                            break;
                    }

                    if (param.Value.IsLowerCase)
                        res.Add(param.Key, value.ToLower());
                    else
                        res.Add(param.Key, value);
                }

                return res;
            }

            return null;
        }

        public Uri BaseURI
        {
            get;
            set;
        }

        public CurrencyTradingPair DefaultPair
        {
            get;
            set;
        }

        public ExchangeType ExchangeSourceType
        {
            get;
            private set;
        }

        /// <summary>
        /// Consecutively increasing action counter
        /// </summary>
        /// <value>0</value>
        public long Nonce
        {
            get
            {
                return DateTime.Now.Ticks;
            }
        }

        protected internal string APIKey
        {
            get;
            set;
        }

        protected internal string ClientID
        {
            get;
            set;
        }

        protected internal string SecretKey
        {
            get;
            set;
        }

        #region Commands

        private APIResult ExecuteCommand(CommandClass commandType, CurrencyTradingPair pair, Dictionary<StandardParameterType, string> values = null)
        {
            ExchangeCommand command = CommandCollection[commandType];

            if (command.HasDependentParameters)
            {
                var paramValues = PopulateCommandParameters(command, pair, values);

                return CommandExecutionEngine.ExecuteCommand(command, pair, paramValues);
            }
            else
                return CommandExecutionEngine.ExecuteCommand(command, pair);
        }

        public bool CancelOrder(Order toCancel)
        {
            return CancelOrder(toCancel.ID);
        }

        public bool CancelOrder(int id)
        {
            return false;
            /*
            bool res;

            res = (bool)BuildCancelOrderCommand(CommandCollection[CommandClass.CancelOrder], id);
            //res = (bool)SendCommandToDispatcher<B>(CommandCollection["CancelOrder"], defaultPair);

            return res;*/
        }

        public Order CreateBuyOrder(decimal amount, decimal price)
        {
            return CreateBuyOrder(DefaultPair, amount, price);
        }

        public Order CreateBuyOrder(CurrencyTradingPair pair, decimal amount, decimal price)
        {
            Order res = null;

            ExchangeCommand toExecute = CommandCollection[CommandClass.BuyOrder];

            Dictionary<StandardParameterType, string> setValues = new Dictionary<StandardParameterType, string>();

            setValues.Add(StandardParameterType.Amount, amount.ToString());
            setValues.Add(StandardParameterType.Price, price.ToString());

            this.CommandExecutionEngine.ExecuteCommand(toExecute, pair, setValues);

            // res = ExecuteOrderCommand(CommandCollection["BuyOrder"], baseCurrency, counterCurrency, amount, price);
            //res = (Order)SendCommandToDispatcher<B>(CommandCollection["BuyOrder"], defaultPair);

            return res;
        }

        public Order CreateSellOrder(decimal amount, decimal price)
        {
            return CreateSellOrder(DefaultPair, amount, price);
        }

        public Order CreateSellOrder(CurrencyTradingPair pair, decimal amount, decimal price)
        {
            return null;
            /*
            Order res;

            res = BuildOrderCommand(CommandCollection[CommandClass.SellOrder], pair, amount, price);

            return res;*/
        }

        /// <summary>
        /// Get complete Balance information for your Exchange account
        /// </summary>
        /// <returns>AccountBalance</returns>
        public AccountBalance GetAccountBalance()
        {
            return (AccountBalance)ExecuteCommand(CommandClass.AccountBalance, DefaultPair);
        }

        /// <summary>
        /// Get your BTC Deposit Address for the Exchange
        /// </summary>
        /// <returns>DepositAddress</returns>
        public DepositAddress GetDepositAddress()
        {
            return GetDepositAddress(Currency.BTC);
        }

        /// <summary>
        /// Get the Deposit Address for the requested CryptoCurrency
        /// </summary>
        /// <param name="toDeposit">CryptoCurrency to deposit</param>
        /// <returns></returns>
        public DepositAddress GetDepositAddress(Currency toDeposit)
        {
            CurrencyTradingPair pair = new CurrencyTradingPair(toDeposit, toDeposit);

            return (DepositAddress)ExecuteCommand(CommandClass.DepositAddress, pair);
        }

        public OpenOrders GetOpenOrders()
        {
            return GetOpenOrders(DefaultPair);
        }

        /// <summary>
        /// Get the current BTC/USD Order Book.
        /// </summary>
        /// <returns></returns>
        public OrderBook GetOrderBook()
        {
            return GetOrderBook(DefaultPair);
        }

        /// <summary>
        /// Get the current Order Book for the specified Currency pair.
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        public OrderBook GetOrderBook(CurrencyTradingPair pair)
        {
            return (OrderBook)ExecuteCommand(CommandClass.OrderBook, pair);
        }

        /// <summary>
        /// Get the current BTC/USD Tick.
        /// </summary>
        /// <returns></returns>
        public Tick GetTick()
        {
            return GetTick(DefaultPair);
        }

        /// <summary>
        /// Get the current Tick for the specified currency pair.
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        public Tick GetTick(CurrencyTradingPair pair)
        {
            return (Tick)ExecuteCommand(CommandClass.Tick, pair);
            // return (Tick)CommandExecutionEngine.ExecuteCommand(CommandCollection[CommandClass.Tick], pair);
        }

        /// <summary>
        /// Return BTC/USD general Transactions for past hour.
        /// </summary>
        /// <returns></returns>
        public Transactions GetTransactions()
        {
            return GetTransactions(DefaultPair);
        }

        /// <summary>
        /// Return general Transactions from the past hour for the specified currency pair.
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        public Transactions GetTransactions(CurrencyTradingPair pair)
        {
            Dictionary<StandardParameterType, string> values = new Dictionary<StandardParameterType, string>();

            values.Add(StandardParameterType.UnixTimeStamp, Common.UnixTime.DateTimeToUnixTimestamp(DateTime.Now.AddHours(-1)).ToString());

            return (Transactions)ExecuteCommand(CommandClass.Transactions, pair, values);
        }

        public UserTransactions GetUserTransactions()
        {
            return GetUserTransactions(DefaultPair);
        }

        public UserTransactions GetUserTransactions(CurrencyTradingPair pair)
        {
            return (UserTransactions)ExecuteCommand(CommandClass.UserTransactions, pair);
        }

        /// <summary>
        /// Verify that a currency pair (e.g. BTC/USD) is supported by this exchange.
        /// </summary>
        /// <param name="baseCurrency">Base Currency</param>
        /// <param name="counterCurrency">Counter Currency</param>
        /// <returns>True if supported, otherwise false.</returns>
        public bool IsTradingPairSupported(CurrencyTradingPair pair)
        {
            return SupportedTradingPairs.Contains(pair);
        }

        protected internal abstract void CreateSignature(RestRequest request, ExchangeCommand command, CurrencyTradingPair pair, Dictionary<string, string> parameters = null);

        protected OpenOrders GetOpenOrders(CurrencyTradingPair pair)
        {
            return (OpenOrders)ExecuteCommand(CommandClass.OpenOrders, pair);
        }

        #endregion Commands

        protected abstract HashSet<CurrencyTradingPair> GetSupportedTradingPairs();

        private void BuildConfiguration()
        {
            SupportedTradingPairs = GetSupportedTradingPairs();
            SupportedCurrencies = new HashSet<Currency>();

            foreach (CurrencyTradingPair pair in SupportedTradingPairs)
            {
                if (!SupportedCurrencies.Contains(pair.BaseCurrency))
                    SupportedCurrencies.Add(pair.BaseCurrency);

                if (!SupportedCurrencies.Contains(pair.CounterCurrency))
                    SupportedCurrencies.Add(pair.CounterCurrency);
            }
        }
    }
}