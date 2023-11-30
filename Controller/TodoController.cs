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


            // WAIT TO FIX
            //List<UserModel> users = UserController.userModels(todo);

            //if (users == null) return;

            Print.Display("");
            //Print.Display(todo.ToString(users));
            Print.Display("");
        }


        // updatetodo 5 TestDB DescDB 
        public static void UpdateTodo(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 6)) return;

            if (readOnlyMode)
            {
                if (Verif.IsStatus(args[3])) return;
                if (Verif.IsDate(args[4])) return;
                if (Verif.IsInt(args[0])) return;
                if (Verif.IsInt(args[5])) return;
            };

            PriorityStatus status = Convertor.ChangeStringToPriority(args[3]);
            DateTime date = Convertor.ChangeStringToDate(args[4]);
            int id = Convertor.ConvStringToInt(args[0]);
            int userId = Convertor.ConvStringToInt(args[5]);

            TodoModel model = new TodoModel(args[1], args[2], status, date, userId);

            TodoModel todoUpdated = TodoModel.UpdateTodo(id, model);
            
            if (todoUpdated == null) return;

            Print.SuccessDisplay("Todo Updated with id : " + todoUpdated.Id);
        }
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
                Print.ErrorDisplay($"Il n'y a pas de todo avec le User (Id) mentionnés.");
                return;
            }

            Print.SuccessDisplay($"Todo {idTodo} is change with User {idNewUser}");
        }

        public static TodoModel? ReadTodosWithId(int todoId)
        {
            TodoModel? todo = TodoModel.ReadTodosWithId(todoId);

            if (todo == null)
            {
                Print.ErrorDisplay($"Il n'y a pas de todo avec le User (Id) mentionnés.");
                return todo;
            }

            return todo;
        }

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
            TodoModel todo = TodoModel.AddTodo(model);

            if (todo != null) return;
            Print.SuccessDisplay("Todo Created with id : " + todo.Id);


            if (todo.Description == "") Menu.GetInstance().AddNotifTodo(todo.Id);
        }

    }
}
