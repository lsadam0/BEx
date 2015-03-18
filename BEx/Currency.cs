using System;
using System.ComponentModel;
using System.Reflection;

namespace BEx
{
    public enum Currency
    {
        // Crypto
        None,

        [Description("BitCoin")]
        Btc, // BitCoin

        [Description("LiteCoin")]
        Ltc, // LiteCoin

        [Description("NameCoin")]
        Nmc, // NameCoin

        Nvc, // NovaCoin
        Ppc, // PeerCoin
        Trc, // TerraCoin
        Ftc, // FeatherCoin
        Xpm, // PrimeCoin
        Usd, // United States Dollar
        Gbp, // Great British Pound
        Rur, // Russian
        Cnh, // Yuan
        Eur, // Euro

        [Description("DarkCoin")]
        Drk, // DarkCoin

        Aud, //	Australia Dollar
        Cny, //	China Yuan Renminbi
        Hkd, //	Hong Kong Dollar
        Isk, //	Iceland Krona
        Jpy, //	Japan Yen
        Krw, //	Korea (South) Won
        Rub, //	Russia Ruble
        Sgd, //	Singapore Dollar
        Unknown
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}