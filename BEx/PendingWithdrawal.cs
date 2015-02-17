﻿using System;

namespace BEx
{
    public class PendingWithdrawal : APIResult
    {
        public int ID
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

        public WithdrawalStatus CurrentStatus
        {
            get;
            set;
        }

        public string Miscellaneous
        {
            get;
            set;
        }

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


        internal PendingWithdrawal(DateTime exchangeTimeStamp)
            : base(exchangeTimeStamp)
        { }
    }

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
}