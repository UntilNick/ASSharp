using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ASSharp.Modules
{
    class LogService
    {
        internal static void Log(string Format, LogLevel FormatColor = LogLevel.Debug)
        {
            var ConsoleColour = Console.ForegroundColor;

            switch (FormatColor)
            {
                case LogLevel.Debug:
                    ConsoleColour = ConsoleColor.Cyan;
                    break;
                case LogLevel.Error:
                    ConsoleColour = ConsoleColor.Red;
                    break;
                case LogLevel.Warn:
                    ConsoleColour = ConsoleColor.Magenta;
                    break;
                default:
                    // Default color
                    break;
            }

            Console.ForegroundColor = ConsoleColour;

            if (String.IsNullOrEmpty(Format))
            {
                Console.WriteLine($"[{DateTime.Now.ToString("h:mm:ss tt")}]: StringNullOrEmpty Occured at LogService.Log");
                return;
            }

            Console.WriteLine($"[{DateTime.Now.ToString("h:mm:ss tt")}]: {Format}");
        }
    }
}
