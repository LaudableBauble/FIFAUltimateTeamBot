using System;

namespace UltimateTeam.Toolkit.Extension
{
    internal static class DateTimeExtensions
    {
        public static int ToUnixTimestamp(this DateTime dateTime)
        {
            var duration = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);

            return (int)duration.TotalSeconds;
        }
    }
}