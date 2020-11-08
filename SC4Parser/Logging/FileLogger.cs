using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser.Logging
{
    class FileLogger : ILogger
    {
        private static List<LogLevel> EnabledChannels = new List<LogLevel>
        {
            LogLevel.Info,
            LogLevel.Warning,
            LogLevel.Error,
            LogLevel.Fatal
        };

        private static readonly Dictionary<LogLevel, string> LogLevelText = new Dictionary<LogLevel, string>
        {
            { LogLevel.Debug, "DEBUG" },
            { LogLevel.Info, "INFO" },
            { LogLevel.Warning, "WARNING" },
            { LogLevel.Error, "ERROR" },
            { LogLevel.Fatal, "FATAL" }
        };

        public string logPath { get; private set; } = "";
        public bool Created = false;

        public FileLogger()
        {
            try
            {
                // Generate a log path
                logPath = Path.Combine(
                    Path.GetTempPath(),
                    string.Format("{0}-log--{1}.txt", "SC4Cartographer", DateTime.Now.ToString("dd-MMM-yyy")));

                // Attempt to create the file
                File.Create(logPath);
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Fatal, "Encountered fatal exception trying to setup FileLogger ({0}:{1})",
                    e.GetType().ToString(),
                    e.Message);
                return;
            }

            Created = true;
            Logger.AddLogOutput(this);
        }

        public void EnableChannel(LogLevel level)
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevel level, string format, params object[] args)
        {
            if (EnabledChannels.Contains(level) == false)
                return;

            string message = args.Length == 0 ? format : string.Format(format, args);
            message = string.Format("[{0}] [{1}] {2}",
                DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss.ff"),
                LogLevelText[level],
                message);

            // Attempt to write data to log file
            try
            {
                using (var writer = File.AppendText(logPath))
                {
                    writer.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, "Encountered exception while trying to write to log file ({0}:{1})",
                    e.GetType().ToString(),
                    e.Message);
            }
        }
    }
}
