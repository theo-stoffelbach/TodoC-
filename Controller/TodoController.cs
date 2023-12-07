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
        public static void AddTodo(string[] args,bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args,4,5)) return;
            if (readOnlyMode)
            {
                if (Verif.IsStatus(args[1])) return;
                if (Verif.IsDate(args[2])) return;
                if (Verif.IsInt(args[3])) return;
            };

            if (UserModel.SearchUserWithId(Convertor.ConvStringToInt(args[3])) == null) return;

            _CreateTodo(args);
        }

        /// <summary>
        /// to read all todos
        /// </summary>
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

        /// <summary>
        /// to read all todos with status and more
        /// </summary>
        /// <param name="args"> it for get more detail of a todo </param>
        /// <param name="readOnlyMode"> If it ReadOnly </param>
        public static void ReadDetailsTodos(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 1)) return;
            if (readOnlyMode && Verif.IsInt(args[1])) return;

            int id = Convertor.ConvStringToInt(args[0]);

            TodoModel todo = TodoModel.ReadTodo(id);
            if (todo == null || !todo.UserId.HasValue) 
            {
                Print.ErrorDisplay($"Todo is null");
                return;
            };

            UserModel? user = UserModel.SearchUserWithId(todo.UserId.Value);

            if (user == null)
            {
                Print.ErrorDisplay($"User is null");
                return;
            }; 

            Print.Display("");
            Print.Display(todo.ToString(user));
            Print.Display("");
        }


        /// <summary>
        /// To update a todo with id and params
        /// </summary>
        /// <param name="args"> 
        /// 0 : is a Int to get the id of todo
        /// 1 : is a string to get the new title of todo
        /// 2 : is a string to get the new description of todo
        /// 3 : is a Priority to get the new status of todo
        /// 4 : is a DateTime to get the new date of todo
        /// </param>
        /// <param name="readOnlyMode"> If it ReadOnly </param>
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

            TodoModel model = new TodoModel(args[1], args[2], status, date, id);

            TodoModel? todoUpdated = TodoModel.UpdateTodo(id, model);
            
            if (todoUpdated == null) return;

            Print.SuccessDisplay("Todo Updated with id : " + todoUpdated.Id);
        }

        /// <summary>
        /// To delete a todo with id
        /// </summary>
        /// <param name="args">
        /// 0 : is a Int to get the id of todo
        /// </param>
        /// <param name="readOnlyMode"> If it ReadOnly </param>
        public static void DeleteTodo(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 1,2)) return;
            if (readOnlyMode)
            {
                if (Verif.IsInt(args[1])) return;
                if (args.Length == 1 && Verif.IsDate(args[1])) return;
            };

            if (args.Length == 1 && !Verif.IsIntWithOutError(args[0]))
            {
                PriorityStatus status = Convertor.ChangeStringToPriority(args[0]);
                List<TodoModel>? listTodos = TodoModel.DeleteTodos(status);
                
                if (listTodos == null) Print.ErrorDisplay("Not todos found");
                
                Print.SuccessDisplay("Delete All Todos successful");
                return;
            }

            int id = int.Parse(args[0]);
            TodoModel.DeleteTodo(id);
            Print.SuccessDisplay($"Delete Todo {id} successful");
        }

        /// <summary>
        /// To delete all todos
        /// </summary>
        /// <param name="args">
        /// 0 : is a Int to get the id of todo for active 
        /// </param>
        /// <param name="readOnlyMode"> If it ReadOnly </param>
        public static void ActivateTodo(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 1)) return;

            if (readOnlyMode && Verif.IsInt(args[1])) return;

            int id = int.Parse(args[0]);

            TodoModel? todo = TodoModel.ActivateTodo(id);
            
            if(todo != null)
            {
                Print.ErrorDisplay($"Bug to Activate Todo ${todo}");
                return;
            };

            Print.SuccessDisplay($"Todo {id} is activate");

        }

        /// <summary>
        /// to add  a description to a todo or change it
        /// </summary>
        /// <param name="args">
        /// 0 : is a Int to get the id of todo
        /// 1 : is a string to get the new description of todo
        /// </param>
        /// <param name="readOnlyMode"> If it ReadOnly </param>
        public static void AddDescTodo(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 2)) return;

            if (readOnlyMode && Verif.IsInt(args[0])) return;

            int id = int.Parse(args[0]);

            TodoModel? todo = TodoModel.AddDescTodo(id, args[1]);

            if (todo == null)
            {
                Print.ErrorDisplay($"Bug with add description to todo {todo.Id}");
                return;
            }

            Print.SuccessDisplay($"Todo {todo.Id} is add description");
        }

        /// <summary>
        /// To change the user of a todo
        /// </summary>
        /// <param name="args">
        /// 0 : is a Int to get the id of todo
        /// 1 : is a Int to get the id of user
        /// </param>
        /// <param name="readOnlyMode"> If it ReadOnly </param>
        public static void ChangeTodoWithUserId(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 2)) return;

            if (readOnlyMode && Verif.IsInt(args[0])) return;
            if (readOnlyMode && Verif.IsInt(args[1])) return;

            int idTodo = int.Parse(args[0]);
            int idNewUser = int.Parse(args[1]);
                
            TodoModel? todo = TodoModel.ChangeUserIdToTodo(idTodo, idNewUser);

            if (todo == null)
            {
                Print.ErrorDisplay($"There is no todo with the Id ({idTodo}) mentioned.");
                return;
            }

            Print.SuccessDisplay($"Todo {idTodo} is change with User {idNewUser}");
        }

        /// <summary>
        /// To read all todos with user id
        /// </summary>
        /// <param name="todoId">
        /// 0 : is an Int to get the id of todo
        /// </param>
        /// <returns></returns>
        public static TodoModel? ReadTodosWithId(int todoId)
        {
            TodoModel? todo = TodoModel.ReadTodo(todoId);

            if (todo == null)
            {
                Print.ErrorDisplay($"There is no todo with the todoId ( {todoId} ) mentioned.");
                return null;
            }

            return todo;
        }

        /// <summary>
        /// To create a todo
        /// </summary>
        /// <param name="args">
        /// 0 : is a string to get the title of todo
        /// 1 : is a Priority to get the status of todo
        /// 2 : is a DateTime to get the date of todo
        /// 3 : is a Int to get the id of user
        /// 4 : is a string to get the description of todo
        /// </param>
        private static void _CreateTodo(string[] args)
        {
            PriorityStatus status = Convertor.ChangeStringToPriority(args[1]);
            DateTime date = Convertor.ChangeStringToDate(args[2]);
            int userId = Convertor.ConvStringToInt(args[3]);

            if (args.Length == 4)
            {
                Array.Resize(ref args, args.Length + 1);
            };

            TodoModel model = new TodoModel(args[0], args[4], status, date, userId);
            TodoModel? todo = TodoModel.AddTodo(model);

            if (todo == null)
            {
                Print.ErrorDisplay($"Bug with add todo {todo.Id} because is null  ");
                return;
            };
            Print.SuccessDisplay("Todo Created with id : " + todo.Id);

            if (todo.Description == "") Menu.GetInstance().AddNotifTodo(todo.Id);
        }

    }
}
