using System;

namespace BEx.Common
{
    internal class UnixTime
    {
        private static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        internal static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            // return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;

            return Convert.ToInt64((dateTime - epoch.ToLocalTime()).TotalSeconds);
        }

        internal static DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            return UnixTimeStampToDateTime(Convert.ToDouble(unixTimeStamp));
        }

        internal static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            /*
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
            */
            return epoch.AddSeconds(unixTimeStamp).ToLocalTime();
        }
    }
}