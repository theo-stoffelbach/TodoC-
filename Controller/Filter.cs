using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class Filter
    {
        private static string? _argumentForFilter;

        /// <summary>
        /// to choose the good filter and execute him
        /// </summary>
        /// <param name="args"> arguments of user </param>
        public static void FilterTodo(string[] args)
        {
            if (!Verif.HasArgsLength(args, 1, 2)) return;

            if (!Verif.VerifArgsWithoutPrint(args, 2))
            {
                _argumentForFilter = args[1];
            }

            Dictionary<string, Action> chooseAvaliable = _CreateDictionaryFilter();

            if (chooseAvaliable.ContainsKey(args[0]))
            {
                chooseAvaliable[args[0].ToLower()]();
                return;
            }
            Print.ErrorDisplay($"filter Not found with {args[0]}, please choose between : 'priority','completed' or 'date'");
            string input = Console.ReadLine();
        }


        /// <summary>
        /// For filter with priority of todo
        /// </summary>
        /// <param name="priority"> the priority to filter </param>
        public static void FilterCondition(PriorityStatus priority)
        {
            List<TodoModel>? todos = TodoModel.ReadTodos();

            if (todos == null)
            {
                Print.ErrorDisplay("No todo found");
                return;
            }

            foreach (var todo in todos)
            {
                if (priority == todo.Status) Print.Display($"Id : {todo.Id}, Name : {todo.Name}, {(todo.Description != "" ? " " : $"Description : {todo.Description}, ")}DueDate : {todo.DueDate}");
            }
        }

        /// <summary>
        /// For filter with date
        /// </summary>
        /// <param name="dateTime"> to filter with date</param>
        public static void FilterCondition(DateTime dateTime)
        {
            List<TodoModel>? todos = TodoModel.ReadTodos();

            if (todos == null)
            {
                Print.ErrorDisplay("No todo found");
                return;
            }

            foreach (var todo in todos)
            {
                if (dateTime == todo.DueDate) Print.Display($"Id : {todo.Id}, Name : {todo.Name}, {(todo.Description != "" ? " " : $"Description : {todo.Description}, ")}DueDate : {todo.DueDate}");
            }
        }

        /// <summary>
        /// For filter with todo completed
        /// </summary>
        /// <param name="isCompleted"> to know if is completed or not</param>
        public static void FilterCondition(bool isCompleted)
        {
            List<TodoModel>? todos = TodoModel.ReadTodos();

            if (todos == null)
            {
                Print.ErrorDisplay("No todo found");
                return;
            }

            foreach (var todo in todos)
            {
                if (isCompleted == todo.IsCompleted) Print.Display($"Id : {todo.Id}, Name : {todo.Name}, {(todo.Description != "" ? " " : $"Description : {todo.Description}, ")}DueDate : {todo.DueDate}");
            }
        }

        /// <summary>
        /// Filter with user id of todo
        /// </summary>
        /// <param name="todoId"> int of a todo </param>
        public static void FilterCondition(int todoId)
        {
            TodoModel? todo = TodoController.ReadTodosWithId(todoId);

            if (todo == null)
            {
                Print.ErrorDisplay($"The todo {todoId} not found");
                return;
            }

            Print.Display(todo.ToString());
        }


        /// <summary>
        /// To filter user has not task
        /// </summary>
        public static void FilterUserHasNotTask() // int todoId
        {
            List<int>? listUsersId = UserModel.GetAllUser();
            List<int> listUserIdHasNotTask = new List<int>();

            if (listUsersId == null)
            {
                Print.ErrorDisplay("No user found");
                return;
            };

            foreach (var userId in listUsersId)
            {
                if (!UserModel.IsUserIdToTodos(userId)) listUserIdHasNotTask.Add(userId);
            }

            _PrintTodosHasNotTask(listUserIdHasNotTask);
        }


        /// <summary>
        /// To print user has not task
        /// </summary>
        /// <param name="listUserIdHasNotTask"> all todos with not userId </param>
        private static void _PrintTodosHasNotTask(List<int> listUserIdHasNotTask)
        {

            if (listUserIdHasNotTask == null)
            {
                Print.ErrorDisplay("All user has a task");
                return;
            };

            Print.Display(" --- User has not Task --- ");

            foreach (var userId in listUserIdHasNotTask)
            {
                Print.Display($"User with Id : {userId} has not task");
            }

            Print.Display(" ------------------------- ");
        }

        /// <summary>
        /// To create a dictionary with all filter
        /// </summary>
        private static Dictionary<string, Action> _CreateDictionaryFilter()
        {
            return new Dictionary<string, Action>
            {
                {"priority",() => FilterCondition(Convertor.ChangeStringToPriority(_argumentForFilter))},
                {"date",() => FilterCondition(Convertor.ChangeStringToDate(_argumentForFilter))},
                {"completed",() => FilterCondition(Convertor.ConvStringToBool(_argumentForFilter))},
                {"getusertodo",() => FilterCondition(Convertor.ConvStringToInt(_argumentForFilter)) },
                {"userhasnottask",() => FilterUserHasNotTask() }, // Convertor.ConvStringToInt(input)
            };
        }
    }
}
