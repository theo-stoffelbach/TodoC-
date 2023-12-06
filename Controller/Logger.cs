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
        /// <summary>
        /// To add a new log in the file or create a file if not exist
        /// </summary>
        /// <param name="log"> what action to user </param>
        public static void AddNewLogAction(string log)
        {
            string path = _getPathFile();

            if (File.Exists(path))
            {
                _addLog(log);
            }
            else
            {
                _createFile();
            }

        }

        /// <summary>
        /// To zip all logs in a zip file
        /// </summary>
        public static void ZipAllLogs()
        {
            string path = _getPathFile();

            if (File.Exists(path))
            {
                try {   
                    _zipToFile(path);
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

        /// <summary>
        /// To zip all logs in a zip file
        /// </summary>
        /// <param name="path"></param>
        private static void _zipToFile(string path)
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

        /// <summary>
        /// To add a new log in the file
        /// </summary>
        /// <param name="log"> Add a log string </param>
        private static void _addLog(string log)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(_getPathFile()))
                {
                    writer.WriteLine(log);
                }
            }
            catch (IOException e)
            {
                Print.ErrorDisplay($"Une erreur est survenue lors de l'ajout du texte au fichier : {e.Message}");
            }
        }

        /// <summary>
        /// To create a file
        /// </summary>
        private static void _createFile()
        {
            string path = _getPathFile();
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
        
        /// <summary>
        /// Get the path of the file
        /// </summary>
        /// <returns></returns>
        private static string _getPathFile()
        {
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("dd-MM-yyyy") + ".txt";

            string cheminAbsolu = AppDomain.CurrentDomain.BaseDirectory + "../../../log";

            string cheminDuFichier = Path.Combine(cheminAbsolu, formattedDate);

            return cheminDuFichier;
        }
    }
}
