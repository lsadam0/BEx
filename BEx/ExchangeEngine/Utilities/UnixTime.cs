// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace BEx.ExchangeEngine.Utilities
{
    internal static class UnixTime
    {
        private static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///     Convert Unix Time value to a UTC DateTime object
        /// </summary>
        /// <param name="source"></param>
        /// <returns>UTC DateTime</returns>
        internal static DateTime ToDateTimeUTC(this double source)
        {
            return UnixTimeStampToDateTime(source);
        }

        /// <summary>
        ///     Convert Unix Time value to a UTC DateTime object
        /// </summary>
        /// <param name="source"></param>
        /// <returns>UTC DateTime</returns>
        internal static DateTime ToDateTimeUTC(this string source)
        {
            return UnixTimeStampToDateTime(source);
        }

        /// <summary>
        ///     Converts DateTime to UTC, and returns the Unix Time representation
        /// </summary>
        /// <param name="source"></param>
        /// <returns>Unix Time</returns>
        internal static double ToUnixTime(this DateTime source)
        {
            return DateTimeToUnixTimestamp(source);
        }

        private static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (dateTime.ToUniversalTime() - epoch).TotalSeconds;
        }

        /// <summary>
        ///     Return the unixtime value converted to a UTC DateTime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        private static DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            return UnixTimeStampToDateTime(Conversion.ToDoubleInvariant(unixTimeStamp));
        }

        /// <summary>
        ///     Return the unixtime value converted to a UTC DateTime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            return epoch.AddSeconds(unixTimeStamp);
        }
    }
}