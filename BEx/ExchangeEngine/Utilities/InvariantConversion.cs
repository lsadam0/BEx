// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;

namespace BEx.ExchangeEngine.Utilities
{
    /// <summary>
    ///     Culture Invariant Conversion Methods
    /// </summary>
    internal static class Conversion
    {
        /// <summary>
        ///     Convert using CultureInfo.InvariantCulture to UTC Time
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeInvariant(string source)
        {
            return new DateTime(Convert.ToDateTime(source, CultureInfo.InvariantCulture).Ticks, DateTimeKind.Utc);
        }

        /// <summary>
        ///     Convert using CultureInfo.InvariantCulture
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal ToDecimalInvariant(string source)
        {
            return Convert.ToDecimal(source, CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Convert using CultureInfo.InvariantCulture
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static double ToDoubleInvariant(string source)
        {
            return Convert.ToDouble(source, CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Convert using CultureInfo.InvariantCulture
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static long ToInt64Invariant(string source)
        {
            return Convert.ToInt64(source, CultureInfo.InvariantCulture);
        }
    }
}