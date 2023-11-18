using UltimateProject.Model;
using UltimateProject.View;

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
        public static bool VerifArgsWithoutPrint(string[] args, int nbArg)
        {
            if (args.Length == nbArg) return true;
            else return false;
        }

        public static bool VerifArgs(string[] args, int minNbArgs, int maxNbArgs)
        {
            if (args.Length >= minNbArgs && args.Length <= maxNbArgs)
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

        public static bool TestTypeStringToStatus(string status)
        {
            if (Enum.TryParse(status, out PriorityStatus _)) return true;
            Print.ErrorDisplay("Command Error is not a priority");
            return false;
        }
        public static bool TestTypeStringToDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out _)) return true;
            Print.ErrorDisplay("Command Error is not a date");
            return false;
        }
        public static bool TestTypeStringToInt(string status)
        {
            if (int.TryParse(status, out _)) return true;
            Print.ErrorDisplay("Command Error is not an int");
            return false;
        }

        public static bool TestTypeStringToIntWithoutPrint(string status)
        {
            if (int.TryParse(status, out _)) return true;
            return false;
        }

        public static bool TestTypeStringToIntOrEnum(string status)
        {
            if (int.TryParse(status, out _) || Enum.TryParse(status, out PriorityStatus _)) return true;

            Print.ErrorDisplay("Command Error is not an int or a priority");
            return false;
        }

        public static Dictionary<string, Action> createDictionaryFilter(string input,List<TodoModel> todos)
        {
            return new Dictionary<string, Action>
            {
                {"priority",() => TodoController.FilterCondition(ChangeStringToPriority(input),todos) },
                {"date",() => TodoController.FilterCondition(ChangeStringToDate(input),todos) },
                {"taskuser",() => TodoController.FilterCondition(ConvStringToInt(input),todos) },
                {"completed",() => TodoController.FilterCondition(ConvStringToBool(input),todos) },
            };
        }

    }
}
