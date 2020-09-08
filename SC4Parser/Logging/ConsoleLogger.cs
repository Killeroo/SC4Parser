using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser.Logging
{
    class ConsoleLogger : ILogger
    {
        public static readonly Dictionary<LogLevel, string> logLevelText = new Dictionary<LogLevel, string>
        {
            { LogLevel.Debug, "DEBUG" },
            { LogLevel.Info, "INFO" },
            { LogLevel.Warning, "WARNING" },
            { LogLevel.Error, "ERROR" },
            { LogLevel.Fatal, "FATAL" }
        };

        public static readonly Dictionary<LogLevel, ConsoleColor> logLevelColors = new Dictionary<LogLevel, ConsoleColor>
        {
            { LogLevel.Debug, ConsoleColor.DarkGray },
            { LogLevel.Info, ConsoleColor.White },
            { LogLevel.Warning, ConsoleColor.Yellow },
            { LogLevel.Error, ConsoleColor.Red },
            { LogLevel.Fatal, ConsoleColor.Magenta }
        };

        public ConsoleLogger()
        {
            Logger.AddLogOutput(this);
        }

        public void Log(LogLevel level, string format, params object[] args)
        {
            string message = args.Length == 0 ? format : string.Format(format, args);
            message = string.Format("[{0}] [{1}] {2}",
                DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss.ff"),
                logLevelText[level],
                message);

            Console.ForegroundColor = logLevelColors[level];
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
