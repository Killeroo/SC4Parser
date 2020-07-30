using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser
{
    class Logger
    {
        private static ConsoleColor m_BackgroundColor;
        private static ConsoleColor m_ForegroundColor; 

        public static void Info(string message, ConsoleColor color = ConsoleColor.White)
        {
            SaveConsoleColors();

            Console.ForegroundColor = color;
            Log("[INFO] " + message);

            RestoreConsoleColors();
        }

        public static void Warning(string message, bool newline = true)
        {
            SaveConsoleColors();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Log("[WARNING] " + message);

            RestoreConsoleColors();
        }

        public static void Error(string message, bool newline = true)
        {
            SaveConsoleColors();

            Console.ForegroundColor = ConsoleColor.Red;
            Log("[ERROR] " + message);
            
            RestoreConsoleColors();
        }

        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        private static void SaveConsoleColors()
        {
            m_BackgroundColor = Console.BackgroundColor;
            m_ForegroundColor = Console.ForegroundColor;
        }

        private static void RestoreConsoleColors()
        {
            Console.BackgroundColor = m_BackgroundColor;
            Console.ForegroundColor = m_ForegroundColor;
        }
    }
}
