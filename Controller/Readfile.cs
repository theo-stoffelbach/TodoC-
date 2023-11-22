using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class ReadFile
    {
        public static void FileCommand()
        {

            string[] files = _getAllCommandFile();

            if (Verif.IsEmpty(files)) return;

            int choice = _getFileChoose(files);

            using (StreamReader sr = new StreamReader(files[choice - 1]))
            {
                while (!sr.EndOfStream)
                {
                    string ligne = sr.ReadLine();

                    Menu menu = Menu.GetInstance();
                    menu.ReadFileLine(ligne);
                }
            }


            Print.SuccessDisplay("END ReadFile");
        }

        private static string[] _getAllCommandFile()
        {
            string cheminDossier = @"../../../commandFile/";
            string extensionRecherchee = ".txt";
            return Directory.GetFiles(cheminDossier)
                                         .Where(fichier => Path.GetExtension(fichier) == extensionRecherchee)
                                         .ToArray();
        }
        private static int _getFileChoose(string[] files)
        {
            Print.Display("\nMerci de choisir entre : ");
            Print.Display("0 : sortir ");
            int i = 1;
            foreach (string file in files)
            {
                string[] pathfile = file.Split("/");
                string filename = pathfile[pathfile.Count() - 1].Split(".")[0];
                Console.WriteLine($"{i} : {filename}");
                i++;
            }

            Print.PrintGetValue("\nVotre choix (entré un nombre)");
            return _getChooseUser(files);
        }
        private static int _getChooseUser(string[] files)
        {
            int choice = Convertor.ConvStringToIntCommand(Console.ReadLine());
            if (choice == 0) Menu.GetInstance().UseMenu();
            if (choice > files.Count() || choice < 0)
            {
                Print.ErrorDisplay($"Votre nombre n'est pas entre 1 et {files.Count()}");
                FileCommand();
                return 0;
            }
            return choice;
        }


    }
}
