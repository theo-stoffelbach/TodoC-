using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateProject.Model;
using UltimateProject.View;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UltimateProject.Controller
{
    public class Utils
    {

        public static bool VerifArgs(string[] args, int nbArg)
        {
            if (args.Length == nbArg)
            {
                return true;
            }
            else
            {
                Print.ErrorDisplay("The number of arguments isn't rigth");
                return false;
            }
        }
        public static bool VerifArgs(string[] args, int minNbArgs, int maxNbArgs)
        {
            if (args.Length > minNbArgs && args.Length < maxNbArgs)
            {
                return true;
            }
            else
            {
                Print.ErrorDisplay($"The number of arguments isn't rigth normaly is between {minNbArgs} and {maxNbArgs}");
                return false;
            }
        }
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

        public static bool ConvStringToBool(string inputUser)
        {
            if (bool.TryParse(inputUser, out bool result)) return result;
            Print.ErrorDisplay("Please choose a really value for Todo completed ( true or false )");
            return ConvStringToBool(Console.ReadLine());
        }

        public static Dictionary<string, Action> createDictionaryFilter(string input,List<TodoModel> todos)
        {
            return new Dictionary<string, Action>
            {
                {"priority",() => TodoController.FilterCondition(ChangeStringToPriority(input),todos) },
                {"date",() => TodoController.FilterCondition(ChangeStringToDate(input),todos) },
                {"completed",() => TodoController.FilterCondition(ConvStringToBool(input),todos) },
            };
        }

    }
}
