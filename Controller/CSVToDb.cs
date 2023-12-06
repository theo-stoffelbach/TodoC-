using System.Text;
using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class CSVToDb
    {
        /// <summary>
        /// To import csv file to db
        /// </summary>
        /// <param name="args"> a string to the path</param>
        public static void ImportFromCsv(string[] args)
        {
            if (!Verif.HasArgsLength(args, 1)) return;

            string csvFilePath = args[0];
            Print.Display("../../../csv/" + csvFilePath);
            string[] lines = File.ReadAllLines("../../../csv/" + csvFilePath);


            foreach (var line in lines.Skip(1))
            {
                var todo = _ParseCsvValueToTodo(line);

                TodoModel.AddTodo(todo);
            }
        }

        /// <summary>
        /// To export csv file to db
        /// </summary>
        public static void ExportToCsv()
        {
            List<TodoModel>? todoModels = TodoModel.ReadTodos();
            string csvFilePath = AppDomain.CurrentDomain.BaseDirectory + "../../../csv/db.csv";

            if (todoModels == null)
            {
                Print.ErrorDisplay("To export csv Because DB is null");
                return;
            };

            using (StreamWriter writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                writer.WriteLine("Id;Name;Description;PriorityStatus;CreationDate;DueDate;IsCompleted");
                foreach (var todo in todoModels)
                {
                    writer.WriteLine($"{todo.Id};{todo.Name};{todo.Description};{todo.Status};{todo.CreationDate};{todo.DueDate};{todo.IsCompleted}");
                }
            }
        }

        /// <summary>
        /// To parse a line of csv to a TodoModel and create a todo
        /// </summary>
        /// <param name="line"> to parse and execute to create a todo </param>
        /// <returns></returns>
        private static TodoModel _ParseCsvValueToTodo(string line)
        {
            string[] values = line.Split(';');
            return new TodoModel
            {
                Name = values[1],
                Description = values[2],
                Status = Enum.TryParse(values[3], out PriorityStatus status) ? status : (PriorityStatus?)null,
                CreationDate = DateTime.TryParse(values[4], out DateTime creationDate) ? creationDate : DateTime.MinValue,
                DueDate = DateTime.TryParse(values[5], out DateTime dueDate) ? (DateTime?)dueDate : null,
                IsCompleted = bool.TryParse(values[6], out bool isCompleted) && isCompleted
            };
        }

    }
}
