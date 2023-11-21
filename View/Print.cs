using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateProject.View
{
    public class Print
    {

        public static void PrintGetValue(string message)
        {
            Console.Write($"{message} : ");
        }
        public static void SuccessDisplay(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Sucess");
            Console.ForegroundColor = originalColor;
            Console.WriteLine($" : {message}");
        }

        public static void Display(string message)
        {
            Console.WriteLine(message);
        }

        public static void ErrorDisplay(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error");
            Console.ForegroundColor = originalColor;
            Console.WriteLine($" : {message}");
        }
        public static void ErrorFatalDisplay(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error");
            Console.ForegroundColor = originalColor;
            Console.WriteLine($" : {message}");
            Environment.Exit(0);
        }
    }
}
