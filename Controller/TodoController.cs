using UltimateProject.Model;
using UltimateProject.View;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UltimateProject.Controller
{
    public class TodoController
    {
        /// <summary>
        /// The command allows for create a new Task and params are : 
        /// </summary>
        /// <param name="args"> args is the parameters of User use when is active the command add</param>
        public static void AddTodo(string[] args,bool readOnlyMode)
        {
            if (!Verif.VerifArgs(args,4,5)) return;
            if (readOnlyMode)
            {
                if (Verif.IsStatus(args[1])) return;
                if (Verif.IsDate(args[2])) return;
                if (Verif.IsInt(args[3])) return;
            };

            if (UserModel.SearchUserWithId(Convertor.ConvStringToInt(args[3])) == null) return;

            _CreateTodo(args);
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
            if (!Verif.HasArgsLength(args, 1)) return;
            if (readOnlyMode && Verif.IsInt(args[1])) return;

            int id = Convertor.ConvStringToInt(args[0]);

            TodoModel todo = TodoModel.ReadTodo(id);

            todo.TodosId = TodoUserController.ReadUserTodosModelWithId(todo.Id);
            List<UserModel> users = UserController.userModels(todo.TodosId);

            if (users == null) return;

            Print.Display("");
            Print.Display(todo.ToString(users));
            Print.Display("");
        }


        // updatetodo 5 TestDB DescDB 
        public static void UpdateTodo(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 5)) return;

            if (readOnlyMode)
            {
                if (Verif.IsStatus(args[3])) return;
                if (Verif.IsDate(args[4])) return;
                if (Verif.IsInt(args[0])) return;
            };

            PriorityStatus status = Convertor.ChangeStringToPriority(args[3]);
            DateTime date = Convertor.ChangeStringToDate(args[4]);
            int id = Convertor.ConvStringToInt(args[0]);

            TodoModel model = new TodoModel(args[1], args[2], status, date);

            TodoModel.UpdateTodo(id, model);
        }
        public static void DeleteTodo(string[] args, bool readOnlyMode)
        {
            if (!Verif.VerifArgs(args, 1,2)) return;
            if (readOnlyMode)
            {
                if (Verif.IsInt(args[1])) return;
                if (args.Length == 1 && Verif.IsDate(args[1])) return;
            };

            if (args.Length == 1 && !Verif.IsIntWithOutError(args[0]))
            {
                PriorityStatus status = Convertor.ChangeStringToPriority(args[0]);
                TodoModel.DeleteTodos(status);
                return;
            }

            int id = int.Parse(args[0]);

            TodoUserController.DeleteAllRefOfTodo(id);
            TodoModel.DeleteTodo(id);
        }

        public static void ActivateTodo(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 1)) return;

            if (readOnlyMode && Verif.IsInt(args[1])) return;

            int id = int.Parse(args[0]);

            TodoModel.ActivateTodo(id);
        }
        public static void AddDescTodo(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 2)) return;

            if (readOnlyMode && Verif.IsInt(args[0])) return;

            int id = int.Parse(args[0]);

            TodoModel.AddDescTodo(id, args[1]);
        }
        public static void FilterTodo(string[] args)
        {
            if (!Verif.HasArgsLength(args, 2)) return;

            List<TodoModel> todos = TodoModel.ReadTodos();

            Dictionary<string, Action> chooseAvaliable = createDictionaryFilter(args[1], todos);

            if (chooseAvaliable.ContainsKey(args[0]))
            {
                chooseAvaliable[args[0].ToLower()]();
                return;
            }
            Print.ErrorDisplay($"filter Not found with {args[0]}, please choose between : 'priority','completed' or 'date'");
            string input = Console.ReadLine();
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

        public static Dictionary<string, Action> createDictionaryFilter(string input, List<TodoModel> todos)
        {
            return new Dictionary<string, Action>
            {
                {"priority",() => TodoController.FilterCondition(Convertor.ChangeStringToPriority(input),todos) },
                {"date",() => TodoController.FilterCondition(Convertor.ChangeStringToDate(input),todos) },
                {"taskuser",() => TodoController.FilterCondition(Convertor.ConvStringToInt(input),todos) },
                {"completed",() => TodoController.FilterCondition(Convertor.ConvStringToBool(input),todos) },
            };
        }


        private static void _CreateTodo(string[] args)
        {
            PriorityStatus status;
            DateTime date;

            status = Convertor.ChangeStringToPriority(args[1]);
            date = Convertor.ChangeStringToDate(args[2]);

            if (args.Length == 4)
            {
                Array.Resize(ref args, args.Length + 1);
            };

            TodoModel model = new TodoModel(args[0], args[4], status, date);
            TodoModel todo = TodoModel.AddTodo(model);

            UserTodosModel.AddUserTodoModel(todo.Id, Convertor.ConvStringToInt(args[3]));
            if (todo.Description == "") Menu.GetInstance().AddNotifTodo(todo.Id);
        }

    }
}
