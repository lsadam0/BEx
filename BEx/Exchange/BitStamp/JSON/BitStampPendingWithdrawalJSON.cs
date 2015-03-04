using Newtonsoft.Json;
using System;

namespace BEx.BitStampSupport
{
    /*
    public class BitStampPendingWithdrawalJSON : ExchangeResponse<PendingWithdrawal>
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("datetime")]
        public string Datetime { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        public override PendingWithdrawal ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            PendingWithdrawal res = new PendingWithdrawal(Convert.ToDateTime(Datetime));

            res.ID = Id;
            res.Miscellaneous = Data;

            //status - (0 - open; 1 - in process; 2 - finished; 3 - canceled; 4 - failed)
            switch (Status)
            {
                case "0":
                    res.CurrentStatus = WithdrawalStatus.AwaitingProcessing;
                    break;

                case "1":
                    res.CurrentStatus = WithdrawalStatus.InProcess;
                    break;

                case "2":
                    res.CurrentStatus = WithdrawalStatus.Finished;
                    break;

                case "3":
                    res.CurrentStatus = WithdrawalStatus.Canceled;
                    break;

                case "4":
                    res.CurrentStatus = WithdrawalStatus.Failed;
                    break;

                case "5":
                    res.CurrentStatus = WithdrawalStatus.PendingApproval;
                    break;

                default:
                    res.CurrentStatus = WithdrawalStatus.Unknown;
                    break;
            }

            res.RequestDate = Convert.ToDateTime(Datetime);
            res.Amount = Convert.ToDecimal(Amount);

            // type - (0 - SEPA; 1 - bitcoin; 2 - WIRE transfer)

            switch (Type)
            {
                case 0:
                    res.WithdrawnByMethod = WithdrawalMethod.SEPA;
                    break;

                case 1:
                    res.WithdrawnByMethod = WithdrawalMethod.CryptoCurrency;
                    break;

                case 2:
                    res.WithdrawnByMethod = WithdrawalMethod.WireTransfer;
                    break;

                default:
                    res.WithdrawnByMethod = WithdrawalMethod.Unknown;
                    break;
            }

            if (res.WithdrawnByMethod == WithdrawalMethod.CryptoCurrency)
                res.CurrencyWithdrawn = Currency.BTC;
            else
                res.CurrencyWithdrawn = Currency.USD;

            return res;
        }
    }
    */
}