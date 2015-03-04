using System;

namespace BEx
{
    public enum WithdrawalMethod
    {
        SEPA,
        CryptoCurrency,
        WireTransfer,
        Unknown
    }

    public enum WithdrawalStatus
    {
        AwaitingProcessing,
        InProcess,
        Finished,
        Canceled,
        Failed,
        Unknown,
        PendingApproval
    }

    public class PendingWithdrawal : APIResult
    {
        internal PendingWithdrawal(DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        { }

        public Decimal Amount
        {
            get;
            set;
        }

        public Currency CurrencyWithdrawn
        {
            get;
            set;
        }

        public WithdrawalStatus CurrentStatus
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }

        public string Miscellaneous
        {
            get;
            set;
        }

        public DateTime RequestDate
        {
            get;
            set;
        }

        public WithdrawalMethod WithdrawnByMethod
        {
            get;
            set;
        }
    }
}