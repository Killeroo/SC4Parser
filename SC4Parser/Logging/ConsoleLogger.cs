using System;
using System.Collections.Generic;

namespace SC4Parser.Logging
{
    /// <summary>
    /// Console Logger implementation, logs output to standard output
    /// </summary>
    /// <example>
    /// <c>
    /// // Setup logger
    /// // This will automatically add it to list of log outputs
    /// ConsoleLogger logger = new ConsoleLogger();
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
    /// TerrainMapSubfile terrainMap = null
    /// try 
    /// {
    ///     terrainMap = savegame.GetTerrainMapSubfile();
    /// }
    /// catch (SubfileNotFoundException)
    /// {
    ///     Console.Writeline("Could not find subfile");
    /// }
    /// </c>
    /// </example>
    class ConsoleLogger : ILogger
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

        private static readonly Dictionary<LogLevel, ConsoleColor> LogLevelColors = new Dictionary<LogLevel, ConsoleColor>
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

        public void EnableChannel(LogLevel level)
        {
            EnabledChannels.Add(level);
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

            Console.ForegroundColor = LogLevelColors[level];
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
