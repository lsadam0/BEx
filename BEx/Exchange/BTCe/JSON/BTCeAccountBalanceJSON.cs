﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BEx.BTCeSupport
{
    public class Funds
    {

        [JsonProperty("usd")]
        public int Usd { get; set; }

        [JsonProperty("btc")]
        public int Btc { get; set; }

        [JsonProperty("ltc")]
        public int Ltc { get; set; }

        [JsonProperty("nmc")]
        public int Nmc { get; set; }

        [JsonProperty("rur")]
        public int Rur { get; set; }

        [JsonProperty("eur")]
        public int Eur { get; set; }

        [JsonProperty("nvc")]
        public int Nvc { get; set; }

        [JsonProperty("trc")]
        public int Trc { get; set; }

        [JsonProperty("ppc")]
        public int Ppc { get; set; }

        [JsonProperty("ftc")]
        public int Ftc { get; set; }

        [JsonProperty("xpm")]
        public int Xpm { get; set; }

        [JsonProperty("cnh")]
        public int Cnh { get; set; }

        [JsonProperty("gbp")]
        public int Gbp { get; set; }
    }

    public class Rights
    {

        [JsonProperty("info")]
        public int Info { get; set; }

        [JsonProperty("trade")]
        public int Trade { get; set; }

        [JsonProperty("withdraw")]
        public int Withdraw { get; set; }
    }

    public class Return
    {

        [JsonProperty("funds")]
        public Funds Funds { get; set; }

        [JsonProperty("rights")]
        public Rights Rights { get; set; }

        [JsonProperty("transaction_count")]
        public int TransactionCount { get; set; }

        [JsonProperty("open_orders")]
        public int OpenOrders { get; set; }

        [JsonProperty("server_time")]
        public int ServerTime { get; set; }
    }

    public class BTCeAccountBalanceJSON : ExchangeResponse<AccountBalances>
    {

        [JsonProperty("success")]
        public int Success { get; set; }

        [JsonProperty("return")]
        public Return Return { get; set; }

        public override AccountBalances ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {

            AccountBalances res;

           

            
            List<AccountBalance> balances = new List<AccountBalance>();
            
            PropertyInfo[] properties = this.Return.Funds.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                Currency pCurrency;

                if (Enum.TryParse<Currency>(prop.Name.ToUpper(), out pCurrency))
                {
                    AccountBalance b = new AccountBalance();
                    b.Balance.Add(pCurrency, Convert.ToDecimal(prop.GetValue(this.Return.Funds)));
                }

            }

            res = new AccountBalances(balances);

            return res;
        }
        /*
        public AccountBalance ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            AccountBalance res = new AccountBalance();


            //this.Return.Funds.

            PropertyInfo[] properties = this.Return.Funds.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                Currency pCurrency;

                if (Enum.TryParse<Currency>(prop.Name.ToUpper(), out pCurrency))
                {
                    res.Balance.Add(pCurrency, Convert.ToDecimal(prop.GetValue(this.Return.Funds)));
                }

            }


            return res;
        }*/
    }
}
