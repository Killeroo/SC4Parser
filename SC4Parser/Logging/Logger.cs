using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser.Logging
{
    /// <summary>
    /// Static logger class, used to pass log messages from library components to any attached/implemented log interfaces
    /// </summary>
    public static class Logger
    {
        private static List<ILogger> logOutputs = new List<ILogger>();

        /// <summary>
        /// Add a logger interface to log output
        /// </summary>
        /// <param name="logOutput">Logger interface to add</param>
        /// <see cref="SC4Parser.Logging.ILogger"/>
        public static void AddLogOutput(ILogger logOutput)
        {
            logOutputs.Add(logOutput);
        }

        /// <summary>
        /// Log a message 
        /// </summary>
        /// <param name="level">Message log level</param>
        /// <param name="format">Format of message</param>
        /// <param name="args">Message arguments</param>
        /// <see cref="SC4Parser.Logging.LogLevel"/>
        public static void Log(LogLevel level, string format, params object[] args)
        {
            foreach (var output in logOutputs)
            {
                output.Log(level, format, args);
            }
        }
    }
}
