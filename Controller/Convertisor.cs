using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class Convertor
    {
        /// <summary>
        /// To convert string to PriorityStatus
        /// </summary>
        /// <param name="status"> it's a string to convert to priorityStatus </param>
        /// <returns> a Status </returns>
        public static PriorityStatus ChangeStringToPriority(string status)
        {
            if (Enum.TryParse(status, true, out PriorityStatus result)) return result;
            else
            {
                Print.ErrorDisplay("Please choose a really Value for PriorityStatus (Low / Medium / Hight)");
                Print.PrintGetValue("Priority status value");
                return ChangeStringToPriority(Console.ReadLine());
            };
        }

        /// <summary>
        /// To convert string to a date
        /// </summary>
        /// <param name="status"> it's a string to convert to a date </param>
        /// <returns> return a date  </returns>
        public static DateTime ChangeStringToDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime date)) return date;
            else
            {
                Print.ErrorDisplay("Please choose a really value for date ( ex : 28/01/2024 )");
                return ChangeStringToDate(Console.ReadLine());
            };
        }

        /// <summary>
        /// To convert string to an int
        /// </summary>
        /// <param name="inputUser"> it's a string to convert to an int </param>
        /// <returns> return an int </returns>
        public static int ConvStringToInt(string inputUser)
        {
            if (int.TryParse(inputUser, out int result)) return result;
            Print.ErrorDisplay("What you have entered is not a number");
            Print.PrintGetValue("Please select a valid number");
            return ConvStringToInt(Console.ReadLine()); ;
        }

        /// <summary>
        /// To convert string to a boolean
        /// </summary>
        /// <param name="inputUser"> it's a string to convert to a boolean </param>
        /// <returns> return a boolean </returns>
        public static bool ConvStringToBool(string inputUser)
        {
            if (bool.TryParse(inputUser, out bool result)) return result;
            Print.ErrorDisplay("Please choose a really value for Todo completed ( true or false )");
            return ConvStringToBool(Console.ReadLine());
        }

        /// <summary>
        /// To convert string to an int without display error 
        /// </summary>
        /// <param name="inputUser"> it's a string to convert to an int </param>
        /// <returns> return an int without display error </returns>
        public static int ConvStringToIntCommand(string inputUser)
        {
            if (int.TryParse(inputUser, out int result)) return result;
            Print.ErrorDisplay("What you have entered is not a number");
            Print.PrintGetValue("Please select a valid number");
            return ConvStringToInt(Console.ReadLine());
        }
    }
}
