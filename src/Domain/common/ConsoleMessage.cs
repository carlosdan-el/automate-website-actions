using System;

namespace Domain.Common
{
    public class ConsoleMessage
    {
        public static void Print(string message, string type = "info")
        {
            switch(type)
            {
                case "info":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "warning":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "success":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;

            }
            Console.WriteLine($"{type.ToUpper()} - {message}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }
    }
}
