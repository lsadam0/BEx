// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the
// Code Analysis results, point to "Suppress Message", and click
// "In Suppression File".
// You do not need to add suppressions to this file manually.

using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("Common Practices and Code Improvements", "RECS0092:Convert field to readonly",
        Justification = "<Pending>", Scope = "member", Target = "~F:BEx.ExchangeEngine.Utilities.UnixTime.epoch")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Bitfinex",
        Scope = "namespace", Target = "BEx.Exchange.BitfinexSupport")]
[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api", Scope = "type",
        Target = "BEx.ApiResult")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Bitfinex",
        Scope = "type", Target = "BEx.Bitfinex")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "api",
        Scope = "member", Target = "BEx.Bitfinex.#.ctor(System.String,System.String)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api", Scope = "type",
        Target = "BEx.ApiError")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member",
        Target =
            "BEx.Exchange.BitfinexSupport.BitfinexAuthenticator.#Authenticate(RestSharp.IRestClient,RestSharp.IRestRequest)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Bitfinex",
        Scope = "type", Target = "BEx.Exchange.BitfinexSupport.BitfinexConfiguration")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Btc",
        Scope = "member", Target = "BEx.Currency.#Btc")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ltc",
        Scope = "member", Target = "BEx.Currency.#Ltc")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nmc",
        Scope = "member", Target = "BEx.Currency.#Nmc")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nvc",
        Scope = "member", Target = "BEx.Currency.#Nvc")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ppc",
        Scope = "member", Target = "BEx.Currency.#Ppc")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sgd",
        Scope = "member", Target = "BEx.Currency.#Sgd")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cny",
        Scope = "member", Target = "BEx.Currency.#Cny")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Hkd",
        Scope = "member", Target = "BEx.Currency.#Hkd")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Isk",
        Scope = "member", Target = "BEx.Currency.#Isk")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jpy",
        Scope = "member", Target = "BEx.Currency.#Jpy")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Krw",
        Scope = "member", Target = "BEx.Currency.#Krw")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Aud",
        Scope = "member", Target = "BEx.Currency.#Aud")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Trc",
        Scope = "member", Target = "BEx.Currency.#Trc")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ftc",
        Scope = "member", Target = "BEx.Currency.#Ftc")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Xpm",
        Scope = "member", Target = "BEx.Currency.#Xpm")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Usd",
        Scope = "member", Target = "BEx.Currency.#Usd")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gbp",
        Scope = "member", Target = "BEx.Currency.#Gbp")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Rur",
        Scope = "member", Target = "BEx.Currency.#Rur")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cnh",
        Scope = "member", Target = "BEx.Currency.#Cnh")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Eur",
        Scope = "member", Target = "BEx.Currency.#Eur")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Drk",
        Scope = "member", Target = "BEx.Currency.#Drk")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api",
        Scope = "member", Target = "BEx.Exchange.IExchangeConfiguration.#ApiKey")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
        Target = "BEx.Exchange.IExchangeCommandFactory.#GetCommands()")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Satisfiable",
        Scope = "member", Target = "BEx.HttpResponseCode.#RequestedRangeNotSatisfiable")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Finex",
        Scope = "member", Target = "BEx.ExchangeType.#BitFinex")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
        Target = "BEx.Exchange.IExchangeCommandFactory.#GetCommandCollection()")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
        Target = "BEx.Exchange.#GetAccountBalance()")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "BEx.Exchange.BitStampSupport")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "BEx.Exchange")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "BEx.Exchange.BitfinexSupport")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Bitfinex",
        Scope = "member", Target = "BEx.ExchangeType.#Bitfinex")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "BEx.Exchange.BitfinexSupport.BitfinexConfiguration.#ClientId")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Scope = "member",
        Target = "BEx.CurrencyTradingPair.#op_Inequality(BEx.CurrencyTradingPair,BEx.CurrencyTradingPair)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Scope = "member",
        Target = "BEx.CurrencyTradingPair.#op_Inequality(BEx.CurrencyTradingPair,BEx.CurrencyTradingPair)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Scope = "member",
        Target = "BEx.CurrencyTradingPair.#op_Equality(BEx.CurrencyTradingPair,BEx.CurrencyTradingPair)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Scope = "member",
        Target = "BEx.CurrencyTradingPair.#op_Equality(BEx.CurrencyTradingPair,BEx.CurrencyTradingPair)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "api",
        Scope = "member",
        Target = "BEx.Exchange.BitStampSupport.BitStampConfiguration.#.ctor(System.String,System.String,System.String)")
]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "api",
        Scope = "member",
        Target =
            "BEx.Exchange.BitStampSupport.BitStampConfiguration.#.ctor(System.String,System.String,System.String,System.String)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "api",
        Scope = "member", Target = "BEx.BitStamp.#.ctor(System.String,System.String,System.String)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "api",
        Scope = "member",
        Target = "BEx.Exchange.BitfinexSupport.BitfinexConfiguration.#.ctor(System.String,System.String,System.String)")
]
[assembly:
    SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member",
        Target =
            "BEx.Exchange.BitfinexSupport.BitfinexAuthenticator.#Authenticate(RestSharp.IRestClient,RestSharp.IRestRequest)"
        )]
[assembly:
    SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member",
        Target =
            "BEx.Exchange.RequestFactory.#PopulateCommandParameters(BEx.Exchange.ExchangeCommand,BEx.CurrencyTradingPair,System.Collections.Generic.Dictionary`2<BEx.Exchange.StandardParameter,System.String>)"
        )]
[assembly:
    SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Scope = "member",
        Target = "BEx.Exchange.ExchangeCommand.#GetResolvedRelativeUri(BEx.CurrencyTradingPair)")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member",
        Target = "BEx.EnumExtensions.#GetDescription(System.Enum)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "BTC", Scope = "member",
        Target = "BEx.Currency.#BTC")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LTC", Scope = "member",
        Target = "BEx.Currency.#LTC")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NMC", Scope = "member",
        Target = "BEx.Currency.#NMC")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NVC", Scope = "member",
        Target = "BEx.Currency.#NVC")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PPC", Scope = "member",
        Target = "BEx.Currency.#PPC")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TRC", Scope = "member",
        Target = "BEx.Currency.#TRC")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FTC", Scope = "member",
        Target = "BEx.Currency.#FTC")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "XPM", Scope = "member",
        Target = "BEx.Currency.#XPM")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "USD", Scope = "member",
        Target = "BEx.Currency.#USD")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "GBP", Scope = "member",
        Target = "BEx.Currency.#GBP")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RUR", Scope = "member",
        Target = "BEx.Currency.#RUR")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CNH", Scope = "member",
        Target = "BEx.Currency.#CNH")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EUR", Scope = "member",
        Target = "BEx.Currency.#EUR")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DRK", Scope = "member",
        Target = "BEx.Currency.#DRK")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "AUD", Scope = "member",
        Target = "BEx.Currency.#AUD")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CNY", Scope = "member",
        Target = "BEx.Currency.#CNY")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HKD", Scope = "member",
        Target = "BEx.Currency.#HKD")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ISK", Scope = "member",
        Target = "BEx.Currency.#ISK")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "JPY", Scope = "member",
        Target = "BEx.Currency.#JPY")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "KRW", Scope = "member",
        Target = "BEx.Currency.#KRW")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RUB", Scope = "member",
        Target = "BEx.Currency.#RUB")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SGD", Scope = "member",
        Target = "BEx.Currency.#SGD")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Ok", Scope = "member",
        Target = "BEx.HttpResponseCode.#Ok")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member",
        Target =
            "BEx.Exchange.BitStampSupport.BitStampAuthenticator.#Authenticate(RestSharp.IRestClient,RestSharp.IRestRequest)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "api",
        Scope = "member",
        Target = "BEx.Exchange.BitfinexSupport.BitfinexConfiguration.#.ctor(System.String,System.String)")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "pair", Scope = "member",
        Target =
            "BEx.AccountBalance.#.ctor(System.Collections.Generic.IEnumerable`1<BEx.Balance>,BEx.CurrencyTradingPair,BEx.ExchangeType)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "api",
        Scope = "member",
        Target = "BEx.Exchange.BitfinexSupport.BitfinexConfiguration.#.ctor(System.String,System.String,System.Uri)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "api",
        Scope = "member",
        Target =
            "BEx.Exchange.BitStampSupport.BitStampConfiguration.#.ctor(System.String,System.String,System.String,System.Uri)"
        )]