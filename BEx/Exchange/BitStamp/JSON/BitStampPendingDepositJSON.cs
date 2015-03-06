namespace BEx.BitStampSupport
{
    /*
    public class BitStampPendingDepositJSON : ExchangeResponse<PendingDeposit>
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("confirmations")]
        public int Confirmations { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        public override PendingDeposit ConvertToStandard(CurrencyTradingPair pair)
        {
            PendingDeposit res = new PendingDeposit(DateTime.Now);

            res.Amount = Convert.ToDecimal(Amount);
            res.Confirmations = Confirmations;
            res.Address = Address;
            res.DepositedCurrency = Currency.BTC;

            return res;
        }
    }*/
}