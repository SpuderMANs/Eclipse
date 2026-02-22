namespace Eclipse.API.Features
{
    using Eclipse.API.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    public static class Log
    {
        public static void Info(string message) => Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogType.Info, ConsoleColor.Cyan);
        public static void Warn(string message) => Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogType.Warn, ConsoleColor.Red);
        public static void Error(string message) => Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogType.Error, ConsoleColor.DarkRed);
        public static void Special(string message) => Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogType.Special, ConsoleColor.Magenta);


        public static void Send(string message, LogType logType, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"[{logType.ToString().ToUpper()}] {message}");
            Console.ResetColor();
        }
    }
}
