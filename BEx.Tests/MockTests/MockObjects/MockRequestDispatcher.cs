using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Commands;
using BEx.ExchangeEngine.Utilities;

using RestSharp;
using BEx.UnitTests.MockTests.MockObjects.MockJSONIntermediates;
using Newtonsoft.Json;

namespace BEx.UnitTests.MockTests.MockObjects
{
    internal class MockRequestDispatcher : IRequestDispatcher
    {
        public IRestResponse Dispatch(IRestRequest request, IExchangeCommand referenceCommand)
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

        private IRestResponse AccountBalanceResponse(IRestRequest request, IExchangeCommand command)
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

        private IRestResponse LimitOrderResponse(IRestRequest request, IExchangeCommand command)
        {
            return null;
        }

        private IRestResponse CancelOrderResponse(IRestRequest request, IExchangeCommand command)
        {
            return null;
        }

        private IRestResponse DepositAddressResponse(IRestRequest request, IExchangeCommand command)
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

        private IRestResponse OpenOrdersResponse(IRestRequest request, IExchangeCommand command)
        {
            return null;
        }

        private IRestResponse OrderBookResponse(IRestRequest request, IExchangeCommand command)
        {
            return null;
        }

        private IRestResponse TickResponse(IRestRequest request, IExchangeCommand command)
        {
            MockTickJSON tick = new MockTickJSON()
            {
                Ask = (251.21m).ToString(),
                Bid = (252.22m).ToString(),
                High = (260.43m).ToString(),
                LastPrice = (251.50m).ToString(),
                Low = (249.34m).ToString(),
                Mid = (252.12m).ToString(),
                Timestamp = UnixTime.DateTimeToUnixTimestamp(DateTime.Now).ToString(),
                Volume = (32456m).ToString()
            };

            return new RestResponse()
            {
                Content = JsonConvert.SerializeObject(tick),
                ResponseStatus = ResponseStatus.Completed,
                StatusCode = HttpStatusCode.OK
            };
        }

        private IRestResponse TransactionsResponse(IRestRequest request, IExchangeCommand command)
        {
            return null;
        }

        private IRestResponse UserTransactionsResponse(IRestRequest request, IExchangeCommand command)
        {
            return null;
        }
    }

}
