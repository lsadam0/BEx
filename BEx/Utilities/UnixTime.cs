﻿using System;

namespace BEx
{
    internal class UnixTime
    {
        private static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        internal static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return Convert.ToDouble((dateTime - epoch.ToLocalTime()).TotalSeconds);
        }

        internal static DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            return UnixTimeStampToDateTime(Conversion.ToDoubleInvariant(unixTimeStamp));
        }

        internal static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            return epoch.AddSeconds(unixTimeStamp).ToLocalTime();
        }
    }
}