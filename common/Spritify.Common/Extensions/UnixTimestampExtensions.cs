using System;

namespace Spritify.Common.Extensions
{
    public static class UnixTimestampExtensions
    {
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            return (long)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static DateTime FromUnixTimestamp(this long timestamp)
        {
            return new DateTime(1970, 1, 1).AddSeconds(timestamp);
        }

        public static DateTime FromUnixTimestamp(this int timestamp)
        {
            return new DateTime(1970, 1, 1).AddSeconds(timestamp);
        }
    }
}
