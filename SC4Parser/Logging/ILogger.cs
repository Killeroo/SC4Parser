using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser.Logging
{
    public interface ILogger
    {
        private static List<LogLevel> EnabledChannels = new List<LogLevel>
        {
            LogLevel.Info,
            LogLevel.Warning,
            LogLevel.Error,
            LogLevel.Fatal
        };

        void EnableChannel(LogLevel level);

        void Log(LogLevel level, string format, params object[] args);
    }
}
