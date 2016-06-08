// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

namespace BEx.ExchangeEngine.Utilities
{
    internal static class ExtensionMethods
    {
        /// <summary>
        ///     Return the InvariantCulture version of the string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariant(this decimal source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Return the InvariantCulture version of the string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariant(this double source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Return the InvariantCulture version of the string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariant(this int source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Return the InvariantCulture version of the string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariant(this long source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }

        public static void Log(this string message)
        {
            Debug.Log(message);
        }
    }
}