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
        public Logger() { }

        public void AddNewLogAction(string log)
        {
            string path = GetPathFile();

            if (File.Exists(path))
            {
                AddLog(log);
            }
            else
            {
                CreateFile();
            }

        }

        public void ZipAllLogs()
        {
            string path = GetPathFile();

            if (File.Exists(path))
            {
                try {   
                    ZipToFile(path);
                    Print.SucessDisplay("Zip created :) ");
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

        private void ZipToFile(string path)
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


        private void AddLog(string log)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(GetPathFile()))
                {
                    writer.WriteLine(log);
                }
            }
            catch (IOException e)
            {
                Print.ErrorDisplay($"Une erreur est survenue lors de l'ajout du texte au fichier : {e.Message}");
            }
        }

        private void CreateFile()
        {
            string path = GetPathFile();

            try
            {
                string cheminAbsolu = AppDomain.CurrentDomain.BaseDirectory + "../../../log";
                string[] dirs = Directory.GetFiles(cheminAbsolu);


                foreach (string fichier in dirs)
                {
                    File.Delete(fichier);
                }

                File.Create(path);  
            }
            catch (Exception ex)
            {
                Print.ErrorDisplay($"test : {ex.ToString()}");
            }
        }                                                                                                                                                                   

        private string GetPathFile()
        {
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("dd-MM-yyyy") + ".txt";

            string cheminAbsolu = AppDomain.CurrentDomain.BaseDirectory + "../../../log";

            string cheminDuFichier = Path.Combine(cheminAbsolu, formattedDate);

            return cheminDuFichier;
        }
    }
}
