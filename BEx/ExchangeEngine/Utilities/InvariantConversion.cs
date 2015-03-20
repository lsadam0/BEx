using System;
using System.Globalization;

namespace BEx.ExchangeEngine.Utilities
{
    /// <summary>
    /// Culture Invariant Conversion Methods
    /// </summary>
    internal static class Conversion
    {
        /// <summary>
        /// Convert using CultureInfo.InvariantCuluture
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal ToDecimalInvariant(string source)
        {
            return Convert.ToDecimal(source, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convert using CultureInfo.InvariantCuluture
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static long ToInt64Invariant(string source)
        {
            return Convert.ToInt64(source, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convert using CultureInfo.InvariantCuluture
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeInvariant(string source)
        {
            return Convert.ToDateTime(source, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convert using CultureInfo.InvariantCuluture
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static double ToDoubleInvariant(string source)
        {
            return Convert.ToDouble(source, CultureInfo.InvariantCulture);
        }
    }
}