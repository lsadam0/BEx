using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx
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
        public static Int64 ToInt64Invariant(string source)
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