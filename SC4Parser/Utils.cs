using System;
using System.IO;

using SC4Parser.Logging;

namespace SC4Parser
{
    class Utils
    {
        /// <summary>
        /// Converts datetime to unixtime used as timestamps in saves
        /// </summary>
        /// <param name="unixTimestamp">Unix timestamp to convert</param>
        /// <returns>Converted Datatime</returns>
        /// <remarks>
        /// Based on https://stackoverflow.com/a/250400
        ///
        /// Could use DateTimeOffset.FromUnixTimeSeconds from .NET 4.6 > but thought it was new enough
        /// That I would ensure a bit of backwards compatability
        /// </remarks>
        public static DateTime UnixTimestampToDateTime(long unixTimestamp)
        {
            DateTime unixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            DateTime convertedDateTime = unixDateTime.AddSeconds(unixTimestamp);
            return convertedDateTime;
        }

        /// <summary>
        /// Save a byte array to a file
        /// </summary>
        /// <param name="data">Data to save</param>
        /// <param name="name">Name of file to save</param>
        /// <param name="path">Path to save file to</param>
        public static void SaveByteArrayToFile(byte[] data, string path, string name)
        {
            try
            {
                // Write buffer to specified path
                using (FileStream stream = new FileStream(Path.Combine(path, name), FileMode.OpenOrCreate))
                {
                    stream.Write(data, 0, data.Length);
                }

                Logger.Log(LogLevel.Info, "Byte array (size {0} bytes) written to path: {1}",
                    data.Length,
                    Path.Combine(path, name));
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, "Exception ({0}) occured while trying to save byte array to file {1}. msg={2} trace={3}",
                    e.GetType().ToString(),
                    Path.Combine(path),
                    e.Message,
                    e.StackTrace);
            }
        }
    }
}
