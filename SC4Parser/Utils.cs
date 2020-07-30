using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser
{
    class Utils
    {
        // Based on https://stackoverflow.com/a/250400
        // Could use DateTimeOffset.FromUnixTimeSeconds from .NET 4.6 > but thought it was new enough
        // That I would ensure a bit of backwards compatability
        public static DateTime UnixTimestampToDateTime(long unixTimestamp)
        {
            DateTime unixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            DateTime convertedDateTime = unixDateTime.AddSeconds(unixTimestamp);
            return convertedDateTime;
        }
    }
}
