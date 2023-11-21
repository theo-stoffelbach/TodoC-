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
        public static DateTime ChangeStringToDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime date)) return date;
            else
            {
                Print.ErrorDisplay("Please choose a really value for date ( ex : 28/01/2024 )");
                return ChangeStringToDate(Console.ReadLine());
            };
        }

        public static int ConvStringToInt(string inputUser)
        {
            if (int.TryParse(inputUser, out int result)) return result;
            Print.ErrorDisplay("Ce que vous avez saissie n'est pas un nombre");
            Print.PrintGetValue("Merci de choisir une nombre valable");
            return ConvStringToInt(Console.ReadLine()); ;
        }

        public static bool ConvStringToBool(string inputUser)
        {
            if (bool.TryParse(inputUser, out bool result)) return result;
            Print.ErrorDisplay("Please choose a really value for Todo completed ( true or false )");
            return ConvStringToBool(Console.ReadLine());
        }

        public static int ConvStringToIntCommand(string inputUser)
        {
            if (int.TryParse(inputUser, out int result)) return result;
            Print.ErrorDisplay("Ce que vous avez saissie n'est pas un nombre");
            Print.PrintGetValue("Merci de choisir une nombre valable");
            return ConvStringToInt(Console.ReadLine());
        }


    }
}
