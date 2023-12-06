using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class Verif
    {
        public static bool HasArgsLength(string[] args, int nbArg)
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

        public static bool HasArgsLength(string[] args, int minNbArgs, int maxNbArgs)
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

        public static bool VerifArgsWithoutPrint(string[] args, int nbArg)
        {
            if (args.Length == nbArg) return true;
            else return false;
        }

        public static bool IsEmpty(string[] str)
        {
            if (str.Length == 0)
            {
                Print.ErrorDisplay("Command Error is empty");
                return true;
            }
            else return false;
        }

        public static bool IsDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out _)) return true;
            Print.ErrorDisplay("Command Error is not a date");
            return false;
        }

        public static bool IsInt(string status)
        {
            if (int.TryParse(status, out _)) return true;
            Print.ErrorDisplay("Command Error is not an int");
            return false;
        }

        public static bool IsIntWithOutError(string status)
        {
            if (int.TryParse(status, out _)) return true;
            return false;
        }

        public static bool IsStatus(string status)
        {
            if (Enum.TryParse(status, out PriorityStatus _)) return true;
            Print.ErrorDisplay("Command Error is not a priority");
            return false;
        }
    }
}
