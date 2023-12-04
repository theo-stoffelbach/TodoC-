﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateProject.View
{
    public class Print
    {

        /// <summary>
        /// Print a message to get a value
        /// </summary>
        /// <param name="message"> It's message print </param>
        public static void PrintGetValue(string message)
        {
            Console.Write($"{message} : ");
        }


        /// <summary>
        /// Print a success message
        /// </summary>
        /// <param name="message"> It's message print </param>
        public static void SuccessDisplay(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Sucess");
            Console.ForegroundColor = originalColor;
            Console.WriteLine($" : {message}");
        }


        /// <summary>
        /// Print a message
        /// </summary>
        /// <param name="message"> It's message print </param>
        public static void Display(string message)
        {
            Console.WriteLine(message);
        }


        /// <summary>
        /// This method is use to display a error message
        /// </summary>
        /// <param name="message"> It's error message print </param>
        public static void ErrorDisplay(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error");  
            Console.ForegroundColor = originalColor;
            Console.WriteLine($" : {message}");
        }


        /// <summary>
        /// This method is use to display a error message and exit the program
        /// </summary>
        /// <param name="message"> It's message print </param>
        public static void ErrorFatalDisplay(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error");
            Console.ForegroundColor = originalColor;
            Console.WriteLine($" : {message}");
            Environment.Exit(0);
        }


        /// <summary>
        /// This method is use to display a help message
        /// </summary>
        public static void Help()
        {
            Print.Display("\n ---------- Help ----------\n"
            + "You have the command then what is do and after the | you have command with :"
            + "\n{ XXXXXX } : obligator argument and"
            + "\n[ XXXXXX ] : optionnal argument"
            + "\n\nExit : To exit a program"
            + "\n\n ---------- Todo ----------\n"
            + "\ncreatetodo : Use to create a todo | Createtodo {Title} {Priority} {DateDue} {UserId} [Description]"
            + "\nupdatetodo: Use to update a todo | Update {Id} {Title} {Description} {Priority} {DateDue} "
            + "\ndeletetodo : Use to delete a todo | Deletetodo {TodoId} OR Deletetodo {Priority} OR Deletetodo all"
            + "\ncompletedtodo : Use to complete Or not complete | completedtodo {TodoId}"
            + "\nchangeuseridtodo : Use to change the UserId | changeuseridtodo {TodoId} {UserId}"
            + "\nadddesctodo : Use to add a description on a todo | adddesctodo {TodoId} {Description}"
            + "\n\n ---------- User ----------\n"
            + "\ncreateuser : Use to create a New User | createuser {name}"
            + "\n\n ---------- Show ----------\n"
            + "\nshowdetailtodos : Use to show a more detail on a todo | showdetailtodos {TodoId}"
            + "\nfiltertodo: Use to update a todo | Update {Id} {Title} {Description} {Priority} {DateDue} "
            + "\nshowstats: Use to complete Or not complete | completedtodo {TodoId}"
            + "\nshowtodos : Use to show a todos | showtodos"
            + "\nshowtodos : Use to delete a todo | Deletetodo {TodoId} OR Deletetodo {Priority} OR Deletetodo all"
            + "\n\n ---------- Other ----------\n"
            + "\nimportcsv : Use to import a csv file | importcsv {Path}"
            + "\nexportcsv : Use to export a csv file | exportcsv"
            + "\nreadfile : Use to read a file | readfile {Path}"
            + "\nzip : Use to add a description on a todo | adddesctodo {TodoId} {Description}"

            );
        }
    }
}
