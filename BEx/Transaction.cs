using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BEx.BitStampSupport;
using BEx.BitFinexSupport;
using BEx.Common;

namespace BEx
{
    public class Transaction
    {

        public DateTime TimeStamp
        {
            get;
            set;
        }

        public long TransactionID
        {
            get;
            set;
        }

        public Decimal Price
        {
            get;
            set;
        }

        public Decimal Amount
        {
            get;
            set;
        }

    
        internal Transaction(BitstampTransactionJSON source)
        {
            this.Amount = Convert.ToDecimal(source.amount);
            this.Price = Convert.ToDecimal(source.price);
            this.TransactionID = Convert.ToInt64(source.tid);

            this.TimeStamp = UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(source.date));

        }

        internal Transaction(BitFinexTransactionJSON source)
        {
            this.Amount = Convert.ToDecimal(source.amount);
            this.Price = Convert.ToDecimal(source.price);
            this.TransactionID = Convert.ToInt64(source.tid);

            this.TimeStamp = UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(source.timestamp));

        }

        internal static List<Transaction> ConvertBitStampTransactionList(List<BitstampTransactionJSON> transactions)
        {
            List<Transaction> res = new List<Transaction>();

            foreach (BitstampTransactionJSON source in transactions)
            {
                res.Add(new Transaction(source));
            }

            return res;
        }

        internal static List<Transaction> ConvertBitFinexTransactionList(List<BitFinexTransactionJSON> transactions)
        {
            List<Transaction> res = new List<Transaction>();

            foreach (BitFinexTransactionJSON source in transactions)
            {
                res.Add(new Transaction(source));
            }

            return res;
        }
    }
}
