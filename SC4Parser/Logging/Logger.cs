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
        /// <example>
        /// <c>
        /// MyOwnLogger myLogger = new MyOwnLogger();
        /// Logger.AddLogOutput(myLogger);
        /// 
        /// // Your logger will now be used as an output for any log message..
        /// </c>
        /// </example>
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
        /// <example>
        /// <c>
        /// Logger.Log(LogLevel.Error, "This is a test log message it can include {0} {1} {2}",
        ///     "strings!",
        ///     123,
        ///     "Or any other type you want to pass!"
        /// );
        /// </c>
        /// </example>
        public static void Log(LogLevel level, string format, params object[] args)
        {
            // Send log message to each output
            foreach (var output in logOutputs)
            {
                output.Log(level, format, args);
            }
        }
    }
}
