using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class Verif
    {

        /// <summary>
        /// To verify if the command is rigth with arguments of the command
        /// </summary>
        /// <param name="args"> the number of arguments of the commands </param>
        /// <param name="nbArg"> the right number of arguments an expected</param>
        /// <returns> return a boolean </returns>
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

        /// <summary>
        /// To verify if the command is rigth with arguments of the command
        /// </summary>
        /// <param name="args"> the number of arguments of the commands </param>
        /// <param name="minNbArgs"> the right minimun number of arguments an expected</param>
        /// <param name="maxNbArgs"> the right maximum number of arguments an expected</param>
        /// <returns> return a boolean </returns>
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

        /// <summary>
        /// To Test if the command has a certain number of arguments
        /// </summary>
        /// <param name="args"> the count of arguments </param>
        /// <param name="nbArg"> the number of argument wanted </param>
        /// <returns></returns>
        public static bool VerifArgsWithoutPrint(string[] args, int nbArg)
        {
            if (args.Length == nbArg) return true;
            else return false;
        }

        /// <summary>
        /// To verify if the command is empty or not
        /// </summary>
        /// <param name="str"> the argument test </param>
        /// <returns></returns>
        public static bool IsEmpty(string[] str)
        {
            if (str.Length == 0)
            {
                Print.ErrorDisplay("Command Error is empty");
                return true;
            }
            else return false;
        }

        /// <summary>
        /// To verify if the command is a date or not
        /// </summary>
        /// <param name="dateString"> the string tested</param>
        /// <returns></returns>
        public static bool IsDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out _)) return true;
            Print.ErrorDisplay("Command Error is not a date");
            return false;
        }

        /// <summary>
        /// To verify if the command is an int or not
        /// </summary>
        /// <param name="status"> the string to tested if is a int</param>
        /// <returns></returns>
        public static bool IsInt(string status)
        {
            if (int.TryParse(status, out _)) return true;
            Print.ErrorDisplay("Command Error is not an int");
            return false;
        }

        /// <summary>
        /// Test if the command is an int without print error
        /// </summary>
        /// <param name="status"> the string to tested if is a int </param>
        /// <returns></returns>
        public static bool IsIntWithOutError(string status)
        {
            if (int.TryParse(status, out _)) return true;
            return false;
        }

        /// <summary>
        /// Test if the command is a status or not
        /// </summary>
        /// <param name="status"> string to tested if is a status </param>
        /// <returns></returns>
        public static bool IsStatus(string status)
        {
            if (Enum.TryParse(status, out PriorityStatus _)) return true;
            Print.ErrorDisplay("Command Error is not a priority");
            return false;
        }

        /// <summary>
        /// Test if the command is test if the file exist or not
        /// </summary>
        /// <param name="path"> The path </param>
        /// <param name="nameFile"> the name file </param>
        /// <returns></returns>
        public static bool IsExistingFile(string path)
        {
            if (!File.Exists(path)) 
            {
                Print.ErrorDisplay($"The file doesn't exist");
                return false;
            };

            return true;
        }
    }
}
