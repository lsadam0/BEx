// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;

namespace BEx
{
    /// <summary>
    ///     Crypto & Fiat Currencies
    /// </summary>
    public enum Currency
    {
        Unknown,

        [Description("bitcoin")] BTC,

        [Description("litecoin")] LTC,

        [Description("NameCoin")] NMC,

        [Description("NovaCoin")] NVC,

        [Description("PeerCoin")] PPC,

        [Description("TerraCoin")] TRC,

        [Description("FeatherCoin")] FTC,

        [Description("PrimeCoin")] XPM,

        [Description("United States Dollar")] USD,

        [Description("Great British Pound")] GBP,

        [Description("Russian")] RUR,

        [Description("Yuan")] CNH,

        [Description("Euro")] EUR,

        [Description("DarkCoin")] DRK,

        [Description("Australia Dollar")] AUD,

        [Description("China Yuan Renminbi")] CNY,

        [Description("Hong Kong Dollar")] HKD,

        [Description("Iceland Krona")] ISK,

        [Description("Japan Yen")] JPY,

        [Description("Korea (South) Won")] KRW,

        [Description("Russia Ruble")] RUB,

        [Description("Singapore Dollar")] SGD

 
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute =
                Attribute.GetCustomAttribute(fieldInfo, typeof (DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}