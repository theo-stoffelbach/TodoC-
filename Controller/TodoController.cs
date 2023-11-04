using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.Controller
{


    public class TodoController
    {

        /// <summary>
        /// The command allows for create a new Task and params are : 
        /// </summary>
        /// <param name="args"> args is the parameters of User use when is active the command add</param>
        public static void AddTodo(string[] args)
        { 
            if (!Utils.VerifArgs(args,3,4)) return;

            PriorityStatus status = Utils.ChangeStringToPriority(args[2]);
            DateTime date = Utils.ChangeStringToDate(args[3]);
            TodoModel model;

            if (Utils.VerifArgs(args, 3))
            {
                model = new TodoModel(args[0], status, date);
                
            }
            else
            {
                model = new TodoModel(args[0], args[1], status, date);
            }

            TodoModel.AddTodo(model);
        }        
        
        public static void ReadTodos(string[] args)
        { 
            if (!Utils.VerifArgs(args,0)) return;

            List<TodoModel> todos = TodoModel.ReadTodos();

            Print.Display(""); 
            todos.ForEach(todo =>
            {
                Print.Display(todo.ToString());
            });
            Print.Display("");
        }

        // updatetodo 5 TestDB DescDB 
        public static void UpdateTodos(string[] args)
        {
            if (!Utils.VerifArgs(args, 5)) return;

            PriorityStatus status = Utils.ChangeStringToPriority(args[3]);
            DateTime date = Utils.ChangeStringToDate(args[4]);
            int id = int.Parse(args[0]);

            TodoModel model = new TodoModel(args[1], args[2], status, date);

            TodoModel.UpdateTodo(id, model);
        }
        public static void DeleteTodo(string[] args)
        {
            if (!Utils.VerifArgs(args, 1)) return;

            if (args[0] == "all") TodoModel.DeleteAllTodos();
            
            int id = int.Parse(args[0]);

            TodoModel.DeleteTodo(id);
        }
        public static void ActivateTodo(string[] args)
        {
            if (!Utils.VerifArgs(args, 1)) return;
            
            int id = int.Parse(args[0]);

            TodoModel.ActivateTodo(id);
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

            FilterTodo(new string[] { args[0], input });
        }
        public static void NotificationTodo(string[] args)
        {

        }

        public static void FilterCondition(PriorityStatus priority,List<TodoModel> todos)
        {
            foreach (var todo in todos)
            {
                if (priority == todo.Status)
                {
                    Console.WriteLine($"Id : {todo.Id}, Name : {todo.Name}, {(todo.Description != "" ? " " : $"Description : {todo.Description}, ")}DueDate : {todo.DueDate}");
                }; 
            }
        }

        public static void FilterCondition(DateTime dateTime, List<TodoModel> todos)
        {
            foreach (var todo in todos)
            {
                if (dateTime == todo.DueDate)
                {
                    Console.WriteLine($"Id : {todo.Id}, Name : {todo.Name}, {(todo.Description != "" ? " " : $"Description : {todo.Description}, ")}DueDate : {todo.DueDate}");
                };
            }
        }

        public static void FilterCondition(bool isCompleted, List<TodoModel> todos)
        {
            foreach (var todo in todos)
            {
                if (isCompleted == todo.IsCompleted)
                {
                    Console.WriteLine($"Id : {todo.Id}, Name : {todo.Name}, {(todo.Description != "" ? " " : $"Description : {todo.Description}, ")}DueDate : {todo.DueDate}");
                };
            }
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
