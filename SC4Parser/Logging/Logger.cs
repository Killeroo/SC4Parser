using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser.Logging
{
    public static class Logger
    {
        private static List<ILogger> logOutputs = new List<ILogger>();

        public static void AddLogOutput(ILogger logOutput)
        {
            logOutputs.Add(logOutput);
        }

        public static void Log(LogLevel level, string format, params object[] args)
        {
            foreach (var output in logOutputs)
            {
                output.Log(level, format, args);
            }
        }
    }
}
