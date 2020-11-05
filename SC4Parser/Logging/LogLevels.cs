using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser.Logging
{
    /// <summary>
    /// Log levels used in log output
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Debug messages
        /// </summary>
        Debug,
        /// <summary>
        /// General messages
        /// </summary>
        Info,
        /// <summary>
        /// Warning messages
        /// </summary>
        Warning,
        /// <summary>
        /// Error messages
        /// </summary>
        Error,
        /// <summary>
        /// Fatal error messages
        /// </summary>
        Fatal
    }
}
