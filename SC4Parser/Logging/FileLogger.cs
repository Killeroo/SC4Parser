using System;
using System.IO;
using System.Collections.Generic;

namespace SC4Parser.Logging
{
    /// <summary>
    /// File Logger implementation, logs output to a file in a temp directory
    /// </summary>
    /// <example>
    /// <c>
    /// // Setup logger
    /// // This will automatically add it to list of log outputs
    /// FileLogger logger = new FileLogger();
    /// 
    /// // Check if the log file was created properly
    /// if (logger.Created == false)
    /// {
    ///     Console.WriteLine("Log file could not be created");
    ///     return;
    /// }
    /// else
    /// {
    ///     // Print out log location
    ///     Console.WriteLine("Created log at {0}", logger.LogPath);
    /// }
    /// 
    /// // Run some operations and generate some logs
    /// 
    /// // Load save game
    /// SC4SaveFile savegame;
    /// try
    /// {
    ///     savegame = new SC4SaveFile(@"C:\Path\To\Save\Game.sc4");
    /// }
    /// catch (DBPFParsingException)
    /// {
    ///     Console.Writeline("Issue occured while parsing DBPF");
    ///     return;
    /// }
    /// 
    /// </c>
    /// </example>
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

        public string LogPath { get; private set; } = "";
        public bool Created = false;

        public FileLogger()
        {
            try
            {
                string logDirectory = Path.Combine(Path.GetTempPath(), "SC4Parser");

                // Create log folder if it does not already exist
                if (Directory.Exists(logDirectory) == false)
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // Generate a log path
                LogPath = Path.Combine(
                    logDirectory,
                    string.Format("{0}-log--{1}.txt", "SC4Parser", DateTime.Now.ToString("HH-mm-ss--dd-MMM-yyy")));

                // Attempt to create the file
                File.Create(LogPath);
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
                File.AppendAllText(LogPath, message + Environment.NewLine);
            }
            catch (Exception)
            {
                // Causes cyclical error as it keeps calling itself to log the message which fails
                //Logger.Log(LogLevel.Error, "Encountered exception while trying to write to log file ({0}:{1})",
                //    e.GetType().ToString(),
                //    e.Message);
            }
        }
    }
}
