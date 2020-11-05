using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser.Logging
{
    /// <summary>
    /// Logger interface, used to create new logging implementations that can be used to print out
    /// internal logging from the library
    /// </summary>
    /// <remarks>
    /// See ConsoleLogger to see how the logging interface can be implemented
    /// </remarks>
    /// <seealso cref="SC4Parser.Logging.Logger"/>
    /// <seealso cref="SC4Parser.Logging.ConsoleLogger"/>
    public interface ILogger
    {
        /// <summary>
        /// Enable a log channel to be included in log output
        /// </summary>
        /// <param name="level">Log level to be enabled</param>
        void EnableChannel(LogLevel level);

        /// <summary>
        /// Log a message 
        /// </summary>
        /// <param name="level">Message log level</param>
        /// <param name="format">Format of message</param>
        /// <param name="args">Message arguments</param>
        /// <see cref="SC4Parser.Logging.LogLevel"/>
        void Log(LogLevel level, string format, params object[] args);
    }
}
