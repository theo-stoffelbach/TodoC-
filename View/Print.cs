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

        public static void Help()
        {
            Print.Display("\n ---------- Help ----------\n"
            + "You have the command then what is do and after the | you have command with :"
            + "{ XXXXXX } : obligator argument and"
            + "[ XXXXXX ] : optionnal argument"
            + "\n ---------- Todo ----------\n"
            + "createtodo : Use to create a todo | Createtodo {Title} {Priority} {DateDue} {UserId} [Description]"
            + "updatetodo: Use to update a todo | Update {Id} {Title} {Description} {Priority} {DateDue} "
            + "deletetodo : Use to delete a todo | Deletetodo {TodoId} OR Deletetodo {Priority} OR Deletetodo all"
            + "completedtodo : Use to complete Or not complete | completedtodo {TodoId}"
            + "adddesctodo : Use to add a description on a todo | adddesctodo {TodoId} {Description}"
            + "\n ---------- User ----------\n"
            + "createuser : Use to create a New User | createuser {name}"
            + "\n ---------- TodoUser ----------\n"
            + "addusertodo : Use to "
            + "\n ---------- Show ----------\n"
            + "showdetailtodos : Use to show a more detail on a todo | showdetailtodos {TodoId}"
            + "filtertodo: Use to update a todo | Update {Id} {Title} {Description} {Priority} {DateDue} "
            + "showtodos : Use to delete a todo | Deletetodo {TodoId} OR Deletetodo {Priority} OR Deletetodo all"
            + "showstats: Use to complete Or not complete | completedtodo {TodoId}"
            + "zip : Use to add a description on a todo | adddesctodo {TodoId} {Description}");
        }
    }
}
