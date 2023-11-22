using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateProject.Model;

namespace UltimateProject.Controller
{
    public class CSVToDb
    {

        public static void ImportFromCsv(string[] args)
        {
            if (!Verif.HasArgsLength(args, 1)) return;

            string csvFilePath = args[0];
            string[] lines = File.ReadAllLines("../../../csv/" + csvFilePath);

            foreach (var line in lines.Skip(1))
            {
                var todo = ParseCsvValueToTodo(line);

                TodoModel.AddTodo(todo);
            }
        }

        public static void ExportToCsv()
        {
            List<TodoModel> todoModels = TodoModel.ReadTodos();
            string csvFilePath = AppDomain.CurrentDomain.BaseDirectory + "../../../csv/db.csv";

            using (StreamWriter writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                writer.WriteLine("Id;Name;Description;PriorityStatus;CreationDate;DueDate;IsCompleted");
                foreach (var todo in todoModels)
                {
                    writer.WriteLine($"{todo.Id};{todo.Name};{todo.Description};{todo.Status};{todo.CreationDate};{todo.DueDate};{todo.IsCompleted}");
                }
            }
        }


        private static TodoModel ParseCsvValueToTodo(string line)
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
