using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using UltimateProject.View;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TP_Theo_Stoffelbach.Controller
{
    public class Logger
    {
        public static void AddNewLogAction(string log)
        {
            string path = _GetPathFile();

            if (File.Exists(path))
            {
                _AddLog(log);
            }
            else
            {
                _CreateFile();
            }

        }

        public static void ZipAllLogs()
        {
            string path = _GetPathFile();

            if (File.Exists(path))
            {
                try {   
                    _ZipToFile(path);
                    Print.SuccessDisplay("Zip created :) ");
                }
                catch (Exception ex){
                    Print.ErrorDisplay(ex.ToString());
                }
            }
            else
            {
                Print.ErrorDisplay("No log, yet");
            }

        }

        private static void _ZipToFile(string path)
        {
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("dd-MM-yyyy");
            string cheminToZip = AppDomain.CurrentDomain.BaseDirectory + "../../../zip/" + formattedDate + ".zip";
            try
            {
                ZipFile.CreateFromDirectory(Path.GetDirectoryName(path), cheminToZip);
            }catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void _AddLog(string log)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(_GetPathFile()))
                {
                    writer.WriteLine(log);
                }
            }
            catch (IOException e)
            {
                Print.ErrorDisplay($"Une erreur est survenue lors de l'ajout du texte au fichier : {e.Message}");
            }
        }
        private static void _CreateFile()
        {
            string path = _GetPathFile();

            try
            {

                string cheminAbsolu = AppDomain.CurrentDomain.BaseDirectory + "../../../log";
                string[] dirs = Directory.GetFiles(cheminAbsolu);

                

                foreach (string fichier in dirs)
                {
                    File.Delete(fichier);
                }

                FileStream fs = File.Create(path);
                fs.Close();
                
            }
            catch (Exception ex)
            {
                Print.ErrorDisplay($"test : {ex.ToString()}");
            }
        }                                                                                                                                                                   
        private static string _GetPathFile()
        {
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("dd-MM-yyyy") + ".txt";

            string cheminAbsolu = AppDomain.CurrentDomain.BaseDirectory + "../../../log";

            string cheminDuFichier = Path.Combine(cheminAbsolu, formattedDate);

            return cheminDuFichier;
        }
    }
}
