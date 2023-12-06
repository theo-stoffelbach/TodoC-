using UltimateProject.Controller;
using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.test_unit
{
    public class TestOther
    {
        public static void TestProtocol()
        {
            List<string> errorList = new List<string>();

            if (!_testExportCSV()) return;
            if (!_testImportCSV()) return;

        }

        public static bool _testExportCSV()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "../../../csv/db.csv";
                CSVToDb.ExportToCsv();

                if (!File.Exists(path)) throw new Exception("To export csv Because file not exist");

                Print.SuccessDisplay("To export csv");
                return true;
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"{err}");
            }
            return false;
        }
         
        public static bool _testImportCSV()
        {
            try
            {
                List<TodoModel>? listTodo = TodoModel.ReadTodos();

                if (listTodo == null) throw new Exception("To import csv Because DB is null ( or todoModel Read error )");

                int countTodos = listTodo.Count;

                string[] path = new string[] { "db.csv" };
                CSVToDb.ImportFromCsv(path);

                List<TodoModel>? listTodoImport = TodoModel.ReadTodos();
                if (listTodoImport == null) throw new Exception("To import csv Because DB is null");

                if (listTodo.Count * 2 != listTodoImport.Count) throw new Exception("To import csv Because DB is not the same");

                Print.SuccessDisplay("To import csv");
                return true;
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"To import csv : {err}");
            }
            return false;
        }
         
        public static bool _test()
        {
            try
            {        
                string[] path = new string[] { "db.csv" };
                CSVToDb.ImportFromCsv(path);

                Print.SuccessDisplay("To import csv");
                return true;
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"To import csv : {err}");
            }
            return false;
        }

        
    }

}