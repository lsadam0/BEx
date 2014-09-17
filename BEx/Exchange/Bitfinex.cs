using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.ComponentModel;

using RestSharp;
using BEx.BitFinexSupport;
using BEx.Common;

namespace BEx
{
    public class Bitfinex : Exchange
    {
        public Bitfinex()
            : base("BitFinex.xml")
        {

        }

        #region Authorization

        protected override void CreateSignature(RestRequest request, APICommand command, Currency baseCurrency, Currency counterCurrency,  Dictionary<string, string> parameters = null)
        {

            /*POST https://api.bitfinex.com/v1/order/new
                With a payload of
                {
                "request": "/v1/order/new",
                "nonce": "1234",
                "option1": ...
                }
                The nonce provided must be strictly increasing.

                To authenticate a request, use the following:

                payload = parameters-dictionary -> JSON encode -> base64
                signature = HMAC-SHA384(payload, api-secret) as hexadecimal
                send (api-key, payload, signature)
                These are encoded as HTTP headers named:
                X-BFX-APIKEY
                X-BFX-PAYLOAD
                X-BFX-SIGNATURE*/
            request.AddHeader("X-BFX-APIKEY", APIKey);

            StringBuilder payload = new StringBuilder();

            payload.Append("{");
            payload.Append("\"request\": \"" +  command.GetResolvedRelativeURI(baseCurrency, counterCurrency) + "\",");
            payload.Append("\"nonce\": \"" + Nonce + "\"");

            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> pair in parameters)
                {
                    payload.Append(",");
                    payload.Append("\"" + pair.Key + "\": \"" + pair.Value + "\"");
                }
            }

            payload.Append("}");

            string payload64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload.ToString()));

            request.AddHeader("X-BFX-PAYLOAD", payload64);

            using (HMACSHA384 hasher = new HMACSHA384(Encoding.UTF8.GetBytes(SecretKey)))
            {
               byte[] hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(payload64));
               request.AddHeader("X-BFX-SIGNATURE", BitConverter.ToString(hashBytes).Replace("-", "").ToLower());
            }
            
        }

        #endregion

        #region Command Execution

        protected override Tick ExecuteTickCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (Tick)SendCommandToDispatcher<BitfinexTickJSON>(command, baseCurrency, counterCurrency);
        }

        protected override OrderBook ExecuteOrderBookCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (OrderBook)SendCommandToDispatcher<BitFinexOrderBookJSON>(command, baseCurrency, counterCurrency);
        }

        protected override Transactions ExecuteTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (Transactions)SendCommandToDispatcher<List<BitFinexTransactionJSON>>(command, baseCurrency, counterCurrency);
        }

        protected override AccountBalance ExecuteAccountBalanceCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return (AccountBalance)SendCommandToDispatcher<List<BitFinexAccountBalanceJSON>>(command, baseCurrency, counterCurrency);
        }

        protected override Order ExecuteOrderCommand(APICommand command, Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price)
        {

            throw new NotImplementedException("BitFinex cannot create orders");
            /*
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("price", amount.ToString());

            return (Order)SendCommandToDispatcher<BT>(command, baseCurrency, counterCurrency, parameters);*/
        }

        protected override OpenOrders ExecuteGetOpenOrdersCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            throw new NotImplementedException("BitFinex cannot retrieve open orders");
            return null;
        }

        protected override UserTransactions ExecuteGetUserTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("symbol", baseCurrency.ToString().ToUpper() + counterCurrency.ToString().ToUpper());

            return (UserTransactions)SendCommandToDispatcher<List<BitFinexUserTransactionJSON>>(command, baseCurrency, counterCurrency, parameters);
        }

        protected override bool ExecuteCancelOrderCommand(APICommand command, int id)
        {
            throw new NotImplementedException("BitFinex cannot cancel orders");
            return false;
        }

        protected override DepositAddress ExecuteGetDepositAddressCommand(APICommand command, Currency toDeposit)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("currency", toDeposit.ToString());
            parameters.Add("method", EnumExtensions.GetDescription(toDeposit).ToLower());
            parameters.Add("wallet_name", "exchange");

            return (DepositAddress)SendCommandToDispatcher<BitFinexDepositAddressJSON>(command, toDeposit, toDeposit, parameters);
        }

        protected override object ExecuteWithdrawCommand(APICommand command, Currency toWithdraw, string address, decimal amount)
        {
            throw new NotImplementedException("BTCe cannot execute withdrawals");

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("amount", amount.ToString());
            parameters.Add("address", address);

            return (string)SendCommandToDispatcher<string>(command, toWithdraw, Currency.None, parameters);
        }

        protected override PendingDeposits ExecutePendingDepositsCommand(APICommand command)
        {
            throw new NotImplementedException("Get Pending Deposits is not implemented");
        }

        protected override PendingWithdrawals ExecutePendingWithdrawalsCommand(APICommand command)
        {
            throw new NotImplementedException("Get Pending Withdrawals is not implemented");
        }

        #endregion

     
    }
}
