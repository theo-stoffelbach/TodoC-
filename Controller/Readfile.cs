using Microsoft.EntityFrameworkCore.Diagnostics;
using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class ReadFile
    {
        /// <summary>
        /// To read a file and execute all commands
        /// </summary>
        public static void FileCommand()
        {

            string[] files = _GetAllCommandFile();

            if (Verif.IsEmpty(files)) return;

            int choice = _GetFileChoose(files);


            _ReadAndExecuteCommand(files,choice);

            Print.SuccessDisplay("END ReadFile");
        }

        /// <summary>
        /// Read a file and execute all commands
        /// </summary>
        /// <param name="files"> all lines of files</param>
        /// <param name="choice"> int for the choise </param>
        private static void _ReadAndExecuteCommand(string[] files, int choice)
        {
            using (StreamReader sr = new StreamReader(files[choice - 1]))
            {
                while (!sr.EndOfStream)
                {
                    string ligne = sr.ReadLine();

                    Menu menu = Menu.GetInstance();
                    menu.ReadFileLine(ligne);
                }
            }
        }
        
        /// <summary>
        /// To get all files in the folder commandFile
        /// </summary>
        /// <returns></returns>
        private static string[] _GetAllCommandFile()
        {
            string cheminDossier = @"../../../commandFile/";
            string extensionRecherchee = ".txt";
            return Directory.GetFiles(cheminDossier)
                                         .Where(fichier => Path.GetExtension(fichier) == extensionRecherchee)
                                         .ToArray();
        }

        /// <summary>
        /// Get the file choose by the user
        /// </summary>
        /// <param name="files"> All choises </param>
        /// <returns></returns>
        private static int _GetFileChoose(string[] files)
        {
            Print.Display("\nThanks to choose : ");
            Print.Display("0 : exit ");
            int i = 1;
            foreach (string file in files)
            {
                string[] pathfile = file.Split("/");
                string filename = pathfile[pathfile.Count() - 1].Split(".")[0];
                Console.WriteLine($"{i} : {filename}");
                i++;
            }

            Print.PrintGetValue("\nYour choice (enter a number)");
            return _GetChooseUser(files);
        }

        /// <summary>
        /// Get the choice of the user
        /// </summary>
        /// <param name="files"> select with all choices </param>
        /// <returns></returns>
        private static int _GetChooseUser(string[] files)
        {
            int choice = Convertor.ConvStringToIntCommand(Console.ReadLine());
            if (choice == 0) Menu.GetInstance().UseMenu();
            if (choice > files.Count() || choice < 0)
            {
                Print.ErrorDisplay($"Your number is not between 1 and {files.Count()}");
                FileCommand();
                return 0;
            }
            return choice;
        }
    }
}
