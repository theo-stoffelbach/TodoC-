using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class TodoController
    {

        public static void readFile() //string[] args
        {
            string cheminDossier = @"../../../commandFile/";
            string extensionRecherchee = ".txt";
            string[] fichiers = Directory.GetFiles(cheminDossier)
                                         .Where(fichier => Path.GetExtension(fichier) == extensionRecherchee)
                                         .ToArray();

            if (fichiers.Count() == 0)
            {
                Print.ErrorDisplay("Vous n'avez aucun fichier de commandes.");
                return;
            }

            Print.Display("\nMerci de choisir entre : ");
            Print.Display("0 : sortir ");
            int i = 1;
            foreach (string fichier in fichiers)
            {
                string[] pathfile = fichier.Split("/");
                string filename = pathfile[pathfile.Count() - 1].Split(".")[0] ;
                Console.WriteLine($"{i} : {filename}");
                i++;
            }

            Print.PrintGetValue("\nVotre choix (entré un nombre)");
            int choice = Utils.ConvStringToIntCommand(Console.ReadLine());
            if (choice == 0) return;
            if (choice > fichiers.Count() || choice < 0)
            {
                Print.ErrorDisplay($"Votre nombre n'est pas entre 1 et {fichiers.Count()}");
                readFile();
                return;
            }
            Print.Display($"test : {choice}\n");

            using (StreamReader sr = new StreamReader(fichiers[choice - 1]))
            {
                while (!sr.EndOfStream)
                {
                    string ligne = sr.ReadLine();
                    //Console.WriteLine(ligne);

                    Menu menu = Menu.GetInstance();
                    menu.ReadFileLine(ligne);
                    
                }
            }


            Print.SucessDisplay("END ReadFile");
        }

        public static void ImportFromCsv(string[] args)
        {
            if (!Utils.VerifArgs(args, 1)) return;
            
            string csvFilePath = args[0];
            string[] lines = File.ReadAllLines("../../../csv/" + csvFilePath);

            foreach (var line in lines.Skip(1))
            {
                var todo = ParseCsvValueToTodo(line);

                TodoModel.AddTodo(todo);
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

        /// <summary>
        /// The command allows for create a new Task and params are : 
        /// </summary>
        /// <param name="args"> args is the parameters of User use when is active the command add</param>
        public static void AddTodo(string[] args,bool readOnlyMode)
        {
            PriorityStatus status;
            DateTime date;

            if (!Utils.VerifArgs(args,4,5)) return;
            if (readOnlyMode)
            {
                if (Utils.TestTypeStringToStatus(args[1])) return;
                if (Utils.TestTypeStringToDate(args[2])) return;
                if (Utils.TestTypeStringToInt(args[3])) return;
            };

            if (UserModel.SearchUserWithId(Utils.ConvStringToInt(args[3])) == null) return;  

            //if(Utils.ConvStringToInt(args[3]))

            status = Utils.ChangeStringToPriority(args[1]);
            date = Utils.ChangeStringToDate(args[2]);
            
            Print.Display("arg : " + args.Length);
            if (args.Length == 4) Array.Resize(ref args, args.Length + 1);
            TodoModel model = new TodoModel(args[0], args[4], status, date);
            TodoModel todo = TodoModel.AddTodo(model);

            UserTodosModel.AddUserTodoModel(todo.Id, Utils.ConvStringToInt(args[3]));
            if (args.Length == 4) Menu.GetInstance().AddNotifTodo(todo.Id);
        }

        public static void ReadTodos()
        { 
            List<TodoModel> todos = TodoModel.ReadTodos();

            Print.Display(""); 
            todos.ForEach(todo =>
            {
                Print.Display(todo.ToString());
            });
            Print.Display("");
        }

        public static void ReadDetailsTodos(string[] args, bool readOnlyMode)
        {
            if (!Utils.VerifArgs(args, 1)) return;

            if (readOnlyMode && Utils.TestTypeStringToInt(args[1])) return;

            int id = int.Parse(args[0]);

            TodoModel todo = TodoModel.ReadTodos(id);

            todo.TodosId = TodoUserController.ReadUserTodosModelWithId(todo.Id);
            List<UserModel> users = UserController.userModels(todo.TodosId);

            if (users == null) return;

            Print.Display("");
            Print.Display(todo.ToString(users));
            Print.Display("");
        }


        // updatetodo 5 TestDB DescDB 
        public static void UpdateTodos(string[] args, bool readOnlyMode)
        {
            if (!Utils.VerifArgs(args, 5)) return;

            if (readOnlyMode)
            {
                if (Utils.TestTypeStringToStatus(args[3])) return;
                if (Utils.TestTypeStringToDate(args[4])) return;
                if (Utils.TestTypeStringToInt(args[0])) return;
            };

            PriorityStatus status = Utils.ChangeStringToPriority(args[3]);
            DateTime date = Utils.ChangeStringToDate(args[4]);
            int id = int.Parse(args[0]);

            TodoModel model = new TodoModel(args[1], args[2], status, date);

            TodoModel.UpdateTodo(id, model);
        }
        public static void DeleteTodo(string[] args, bool readOnlyMode)
        {
            if (!Utils.VerifArgs(args, 1,2)) return;

            if (args[0] == "all")
            {
                TodoModel.DeleteAllTodos();
                return;
            }

            if (readOnlyMode)
            {
                if (Utils.TestTypeStringToInt(args[1])) return;
                if (args.Length == 1 && Utils.TestTypeStringToDate(args[1])) return;
            };

            if(args.Length == 1)
            {
                PriorityStatus status = Utils.ChangeStringToPriority(args[0]);
                TodoModel.DeleteTodos(status);
            }

            int id = int.Parse(args[0]);

            TodoModel.DeleteTodo(id);
        }
        public static void ActivateTodo(string[] args, bool readOnlyMode)
        {
            if (!Utils.VerifArgs(args, 1)) return;

            if (readOnlyMode && Utils.TestTypeStringToInt(args[1])) return;

            int id = int.Parse(args[0]);

            TodoModel.ActivateTodo(id);
        }
        public static void AddDescTodo(string[] args, bool readOnlyMode)
        {
            if (!Utils.VerifArgs(args, 2)) return;

            if (readOnlyMode && Utils.TestTypeStringToInt(args[0])) return;

            int id = int.Parse(args[0]);

            TodoModel.AddDescTodo(id, args[1]);
        }
        public static void FilterTodo(string[] args)
        {
            if (!Utils.VerifArgs(args, 2)) return;

            List<TodoModel> todos = TodoModel.ReadTodos();

            Dictionary<string, Action> chooseAvaliable = Utils.createDictionaryFilter(args[1], todos);

            if (chooseAvaliable.ContainsKey(args[0]))
            {
                chooseAvaliable[args[0].ToLower()]();
                return;
            }
            Print.ErrorDisplay($"filter Not found with {args[0]}, please choose between : 'priority','completed' or 'date'");
            string input = Console.ReadLine();

            //FilterTodo(new string[] { args[0], input });
        }

        public static void FilterCondition(PriorityStatus priority,List<TodoModel> todos)
        {
            foreach (var todo in todos)
            {
                if (priority == todo.Status) Print.Display($"Id : {todo.Id}, Name : {todo.Name}, {(todo.Description != "" ? " " : $"Description : {todo.Description}, ")}DueDate : {todo.DueDate}");
            }
        }

        public static void FilterCondition(DateTime dateTime, List<TodoModel> todos)
        {
            foreach (var todo in todos)
            {
                if (dateTime == todo.DueDate) Print.Display($"Id : {todo.Id}, Name : {todo.Name}, {(todo.Description != "" ? " " : $"Description : {todo.Description}, ")}DueDate : {todo.DueDate}");
            }
        }

        public static void FilterCondition(bool isCompleted, List<TodoModel> todos)
        {
            foreach (var todo in todos)
            {
                if (isCompleted == todo.IsCompleted) Print.Display($"Id : {todo.Id}, Name : {todo.Name}, {(todo.Description != "" ? " " : $"Description : {todo.Description}, ")}DueDate : {todo.DueDate}");
            }
        }
        public static void FilterCondition(int userId, List<TodoModel> todos)
        {
            List<UserTodosModel> listUserTodosId = UserTodosModel.ReadIdUserModel(userId);
            List<int> listUsersId = listUserTodosId.Select(userstodo => userstodo.TodoId).ToList();
            List<TodoModel> listTodos = ReadTodosWithId(listUsersId);

            Print.Display("");
            listTodos.ForEach(todo =>
            {
                Print.Display(todo.ToString());
            });
            Print.Display("");
        }

        public static List<TodoModel> ReadTodosWithId(List<int> listUsersId)
        {
            return TodoModel.ReadTodosWithId(listUsersId);
        }

        public static TodoModel ChangeTodoValue(TodoModel todoOrigin, TodoModel todoValue)
        {
            if (todoValue == null) return todoOrigin;

            if (todoValue.Name != null) todoOrigin.Name = todoValue.Name;
            if (todoValue.Description != null) todoOrigin.Description = todoValue.Description;
            if (todoValue.Status != null) todoOrigin.Status = todoValue.Status;
            if (todoValue.DueDate != null) todoOrigin.DueDate = todoValue.DueDate;

            return todoOrigin;
        }

    }
}
