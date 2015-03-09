using System;

namespace BEx.Common
{
    internal class UnixTime
    {
        private static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        internal static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return Convert.ToInt64((dateTime - epoch.ToLocalTime()).TotalSeconds);
        }

        internal static DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            return UnixTimeStampToDateTime(Convert.ToDouble(unixTimeStamp));
        }

        internal static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            return epoch.AddSeconds(unixTimeStamp).ToLocalTime();
        }
    }
}