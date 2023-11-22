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
            + "\n{ XXXXXX } : obligator argument and"
            + "\n[ XXXXXX ] : optionnal argument"
            + "\n\n ---------- Todo ----------\n"
            + "\ncreatetodo : Use to create a todo | Createtodo {Title} {Priority} {DateDue} {UserId} [Description]"
            + "\nupdatetodo: Use to update a todo | Update {Id} {Title} {Description} {Priority} {DateDue} "
            + "\ndeletetodo : Use to delete a todo | Deletetodo {TodoId} OR Deletetodo {Priority} OR Deletetodo all"
            + "\ncompletedtodo : Use to complete Or not complete | completedtodo {TodoId}"
            + "\nadddesctodo : Use to add a description on a todo | adddesctodo {TodoId} {Description}"
            + "\n\n ---------- User ----------\n"
            + "\ncreateuser : Use to create a New User | createuser {name}"
            + "\n\n ---------- TodoUser ----------\n"
            + "\naddusertodo : Use to add a User to a Todo | createuser {name}"
            + "\n\n ---------- Show ----------\n"
            + "\nshowdetailtodos : Use to show a more detail on a todo | showdetailtodos {TodoId}"
            + "\nfiltertodo: Use to update a todo | Update {Id} {Title} {Description} {Priority} {DateDue} "
            + "\nshowtodos : Use to delete a todo | Deletetodo {TodoId} OR Deletetodo {Priority} OR Deletetodo all"
            + "\nshowstats: Use to complete Or not complete | completedtodo {TodoId}"
            + "\nzip : Use to add a description on a todo | adddesctodo {TodoId} {Description}");
        }
    }
}
