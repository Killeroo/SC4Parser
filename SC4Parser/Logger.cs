using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser
{
    // TODO: I want this class to be able to take arguments like console.writeline

    class Logger
    {
        private static ConsoleColor m_BackgroundColor;
        private static ConsoleColor m_ForegroundColor; 

        public static void Info(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Log("[INFO] " + message);
            Console.ResetColor();
        }

        public static void Warning(string message, bool newline = true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Log("[WARNING] " + message);
            Console.ResetColor();
        }

        public static void Error(string message, bool newline = true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Log("[ERROR] " + message);
            Console.ResetColor();
        }

        public static void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
