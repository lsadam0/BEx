using BEx.ExchangeEngine;
using BEx.ExchangeEngine.BitStamp.JSON;
using BEx.ExchangeEngine.Commands;
using BEx.ExchangeEngine.Utilities;
using BEx.UnitTests.MockTests.MockObjects.MockJSONIntermediates;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BEx.UnitTests.MockTests.MockObjects
{
    internal class MockFailedRequestDispatcher : IRequestDispatcher
    {
        public IRestResponse Dispatch<T>(IRestRequest request, IExchangeCommand<T> referenceCommand) where T : IExchangeResult
        {
            if (referenceCommand is AccountBalanceCommand)
                return AccountBalanceResponse(request, referenceCommand);
            else if (referenceCommand is LimitOrderCommand)
                return LimitOrderResponse(request, referenceCommand);
            else if (referenceCommand is CancelOrderCommand)
                return CancelOrderResponse(request, referenceCommand);
            else if (referenceCommand is DepositAddressCommand)
                return DepositAddressResponse(request, referenceCommand);
            else if (referenceCommand is OpenOrdersCommand)
                return OpenOrdersResponse(request, referenceCommand);
            else if (referenceCommand is OrderBookCommand)
                return OrderBookResponse(request, referenceCommand);
            else if (referenceCommand is TickCommand)
                return TickResponse(request, referenceCommand);
            else if (referenceCommand is TransactionsCommand)
                return TransactionsResponse(request, referenceCommand);
            else if (referenceCommand is UserTransactionsCommand)
                return UserTransactionsResponse(request, referenceCommand);

            return null;
        }

        private IRestResponse AccountBalanceResponse<T>(IRestRequest request, IExchangeCommand<T> command) where T : IExchangeResult
        {
            var usdBalance = new MockAccountBalanceJSON()
            {
                Amount = "100.00",
                Available = "50.00",
                Currency = Currency.USD.ToString(),
                Type = "exchange"
            };

            var btcBalance = new MockAccountBalanceJSON()
            {
                Amount = "2.123456",
                Available = "1.9876543",
                Currency = Currency.BTC.ToString(),
                Type = "exchange"
            };

            var ltcBalance = new MockAccountBalanceJSON()
            {
                Amount = "200.123456",
                Available = "100.9876543",
                Currency = Currency.LTC.ToString(),
                Type = "exchange"
            };

            var balances = new List<MockAccountBalanceJSON>()
            {
                usdBalance,
                btcBalance,
                ltcBalance
            };

            return new RestResponse()
            {
                Content = JsonConvert.SerializeObject(balances),
                ResponseStatus = ResponseStatus.Completed,
                StatusCode = HttpStatusCode.OK
            };
        }

        private IRestResponse LimitOrderResponse<T>(IRestRequest request, IExchangeCommand<T> command) where T : IExchangeResult
        {
            ErrorIntermediate error = new ErrorIntermediate();
            //{
            //  Error = "blah blah blah"
            //};

            return null;
        }

        private IRestResponse CancelOrderResponse<T>(IRestRequest request, IExchangeCommand<T> command) where T : IExchangeResult
        {
            return null;
        }

        private IRestResponse DepositAddressResponse<T>(IRestRequest request, IExchangeCommand<T> command) where T : IExchangeResult
        {
            MockDepositAddressJSON deposit = new MockDepositAddressJSON()
            {
                Address = Guid.NewGuid().ToString(),
                Currency = "unk",
                Method = "unknown",
                Result = "unknown"
            };

            return new RestResponse()
            {
                Content = JsonConvert.SerializeObject(deposit),
                ResponseStatus = ResponseStatus.Completed,
                StatusCode = HttpStatusCode.OK
            };
        }

        private IRestResponse OpenOrdersResponse<T>(IRestRequest request, IExchangeCommand<T> command) where T : IExchangeResult
        {
            var OrderOne = new MockOrderResponseJSON()
            {
                OriginalAmount = "100.00",
                Id = 100,
                Price = "234.12",
                Side = "sell"
            };

            var OrderTwo = new MockOrderResponseJSON()
            {
                OriginalAmount = "200.00",
                Id = 200,
                Price = "334.12",
                Side = "buy"
            };

            var OrderThree = new MockOrderResponseJSON()
            {
                OriginalAmount = "300.00",
                Id = 300,
                Price = "434.12",
                Side = "sell"
            };

            var orders = new List<MockOrderResponseJSON>()
            {
                OrderOne,
                OrderTwo,
                OrderThree
            };

            return new RestResponse()
            {
                Content = JsonConvert.SerializeObject(orders),
                ResponseStatus = ResponseStatus.Completed,
                StatusCode = HttpStatusCode.OK
            };
        }

        private IRestResponse OrderBookResponse<T>(IRestRequest request, IExchangeCommand<T> command) where T : IExchangeResult
        {
            var asks = new List<Ask>()
            {
                new Ask()
                {
                    Price = "212.34",
                    Amount = "2.332245",
                    Timestamp = DateTime.UtcNow.ToUnixTime().ToString()
                },
                new Ask()
                {
                    Price = "214.34",
                    Amount = "3.332245",
                    Timestamp = DateTime.UtcNow.ToUnixTime().ToString()
                }
            };

            var bids = new List<Bid>()
            {
                new Bid()
                {
                    Price = "212.34",
                    Amount = "2.332245",
                    Timestamp = DateTime.UtcNow.ToUnixTime().ToString()
                },
                new Bid()
                {
                    Price = "214.34",
                    Amount = "3.332245",
                    Timestamp = DateTime.UtcNow.ToUnixTime().ToString()
                }
            };

            var orderBook = new MockOrderBookJSON()
            {
                Bids = bids.OrderByDescending(x => x.Price).ToArray(),
                Asks = asks.OrderBy(x => x.Price).ToArray()
            };

            return new RestResponse()
            {
                Content = JsonConvert.SerializeObject(orderBook),
                ResponseStatus = ResponseStatus.Completed,
                StatusCode = HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Times out
        /// </summary>
        /// <param name="request"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        private IRestResponse TickResponse<T>(IRestRequest request, IExchangeCommand<T> command) where T : IExchangeResult
        {
            RestClient client = new RestClient("http://nothingnothing");

            client.Timeout = 0;
            return client.Execute(request);
        }

        private IRestResponse TransactionsResponse<T>(IRestRequest request, IExchangeCommand<T> command) where T : IExchangeResult
        {
            var firstTransaction = new MockTransactionJSON()
            {
                amount = "4.0",
                exchange = "",
                price = "250.43",
                tid = 101,
                timestamp = Convert.ToInt64(DateTime.UtcNow.ToUnixTime()),
                type = ""
            };

            var secondTransaction = new MockTransactionJSON()
            {
                amount = "4.0",
                exchange = "",
                price = "251.43",
                tid = 102,
                timestamp = Convert.ToInt64(DateTime.UtcNow.ToUnixTime()),
                type = ""
            };

            var thirdTransaction = new MockTransactionJSON()
            {
                amount = "4.0",
                exchange = "",
                price = "252.43",
                tid = 103,
                timestamp = Convert.ToInt64(DateTime.UtcNow.ToUnixTime()),
                type = ""
            };

            var trans = new List<MockTransactionJSON>()
            {
                firstTransaction,
                secondTransaction,
                thirdTransaction
            };

            return new RestResponse()
            {
                Content = JsonConvert.SerializeObject(trans),
                ResponseStatus = ResponseStatus.Completed,
                StatusCode = HttpStatusCode.OK
            };
        }

        private IRestResponse UserTransactionsResponse<T>(IRestRequest request, IExchangeCommand<T> command) where T : IExchangeResult
        {
            var transactions = new List<MockUserTransactionJSON>()
            {
                new MockUserTransactionJSON()
                {
                    Tid = 100,
                    Price = "241.32",
                    Amount = "2.321",
                    FeeAmount = "5.43",
                    FeeCurrency = "BTC",
                    Timestamp = DateTime.UtcNow.ToUnixTime().ToString(),
                    Type = "Buy"
                },
                new MockUserTransactionJSON()
                {
                    Tid = 200,
                    Price = "242.32",
                    Amount = "3.321",
                    FeeAmount = "6.43",
                    FeeCurrency = "BTC",
                    Timestamp = DateTime.UtcNow.ToUnixTime().ToString(),
                    Type = "Sell"
                },
                new MockUserTransactionJSON()
                {
                    Tid = 300,
                    Price = "243.32",
                    Amount = "4.321",
                    FeeAmount = "8.43",
                    FeeCurrency = "BTC",
                    Timestamp = DateTime.UtcNow.ToUnixTime().ToString(),
                    Type = "Buy"
                }
            };

            return new RestResponse()
            {
                Content = JsonConvert.SerializeObject(transactions.OrderByDescending(x => x.Timestamp)),
                ResponseStatus = ResponseStatus.Completed,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}