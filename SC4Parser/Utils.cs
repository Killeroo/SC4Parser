using System;
using System.IO;
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

        public static void SaveByteArrayToFile(byte[] data, string path, string name)
        {
            try
            {
                // Write buffer to specified path
                using (FileStream stream = new FileStream(Path.Combine(path, name), FileMode.OpenOrCreate))
                {
                    stream.Write(data, 0, data.Length);
                }

                Logger.Info(string.Format("Data (tgi={0}, size {1} bytes) written to path: {2}",
                    name,
                    data.Length,
                    Path.Combine(path, name)));
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("Exception ({0}) occured while trying to save Index Entry ({4}) to path {1}. msg={2} trace={3}",
                    e.GetType().ToString(),
                    Path.Combine(path),
                    e.Message,
                    e.StackTrace,
                    name));
            }
        }
    }
}
