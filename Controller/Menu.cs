using TP_Theo_Stoffelbach.Controller;
using UltimateProject.Model;
using UltimateProject.View;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UltimateProject.Controller
{

    public record Menu
    {
        private static string _chooseUser;
        private static string[] _arguments;
        private static bool _readOnly;
        public List<Notif> NotifList = new List<Notif>();
        public static Dictionary<string, Action> ChooseMenu;

        private static Menu _instance = null;


        /// <summary>
        /// The constructor of Menu
        /// </summary>
        private Menu()
        {
            ChooseMenu = new Dictionary<string, Action>
            {
                { "exit", () => Environment.Exit(0)},
                { "help", () => Print.Help()},
                { "createtodo", () => TodoController.AddTodo(_arguments,_readOnly)},
                { "updatetodo", () => TodoController.UpdateTodo(_arguments,_readOnly)},
                { "deletetodo", () => TodoController.DeleteTodo(_arguments,_readOnly)},
                { "completedtodo", () => TodoController.ActivateTodo(_arguments,_readOnly)},
                { "changeuseridtodo", () => TodoController.ChangeTodoWithUserId(_arguments,_readOnly)},
                { "adddesctodo",() => TodoController.AddDescTodo(_arguments,_readOnly)},
                { "showdetailtodos",  () => TodoController.ReadDetailsTodos(_arguments,_readOnly)},
                { "showusers", () => UserController.ReadUsers()},

                { "filtertodo", () => Filter.FilterTodo(_arguments)},
                { "showtodos",  () => TodoController.ReadTodos()},
                { "showstats", () => Stats.Show()},
                { "zip", () => Logger.ZipAllLogs()},

                { "importcsv", () => CSVToDb.ImportFromCsv(_arguments)},
                { "exportcsv", () => CSVToDb.ExportToCsv()},

                { "readfile", () => ReadFile.FileCommand()},

                { "createuser", () => UserController.AddUser(_arguments, _readOnly)},
                { "updateuser", () => UserController.UpdateTodo(_arguments, _readOnly)},
                { "deleteuser", () => UserController.DeleteUser(_arguments, _readOnly)},
            };
        }

        /// <summary>
        /// Get the instance of Menu because it's a singleton
        /// </summary>
        /// <returns></returns>
        public static Menu GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Menu();
            }   
            return _instance;
        }

        /// <summary>
        /// It's a method to add a new Notif in the list
        /// </summary>
        /// <param name="id"></param>
        public void AddNotifTodo(int id)
        {
            NotifList.Add(new Notif(id));
        }

        /// <summary>
        /// Use to execute the menu
        /// </summary>
        public void UseMenu()
        {
            Notif.TestNotifTime(NotifList);

            Print.PrintGetValue("Enter a command");
            string inputUser = Console.ReadLine();

            string[] command = inputUser.Split(' ');
            _chooseUser = command[0].ToLower();
            _arguments = command.Skip(1).ToArray();
            _readOnly = false;

            _selectCommand(inputUser);

            Print.Display("");
            UseMenu();
        }


        /// <summary>
        /// Use to read a file and line by line execute the command
        /// </summary>
        /// <param name="Command"></param>
        /// <returns></returns>
        public bool ReadFileLine(string Command)
        {
            string[] command = Command.Split(' ');
            _chooseUser = command[0].ToLower();
            _arguments = command.Skip(1).ToArray();
            _readOnly = true;

            if (ChooseMenu.ContainsKey(_chooseUser))
            {
                ChooseMenu[_chooseUser.ToLower()](); // Here to execute command
                return true;
            }
            else
            {
                Print.ErrorDisplay($"Error to Execute this : {Command}");
                return false;
            }
        }


        /// <summary>
        /// MenuFilter is a method to execute a command without display
        /// </summary>
        public void MenuFilter()
        {
            Print.PrintGetValue("Enter a command");
            string inputUser = Console.ReadLine();

            string[] command = inputUser.Split(' ');
            _chooseUser = command[0].ToLower();
            _arguments = command.Skip(1).ToArray();

            _ChooseMenu();

            UseMenu();
        }

        private void _selectCommand(string command)
        {
            if (ChooseMenu.ContainsKey(_chooseUser))
            {
                ChooseMenu[_chooseUser.ToLower()](); // Here to execute command
            }
            else
            {
                Print.ErrorDisplay($"Error to Execute this : {Command}");
            }
        }

        private void _ChooseMenu()
        {
            if (ChooseMenu.ContainsKey(_chooseUser))
            {
                ChooseMenu[_chooseUser.ToLower()]();
            }
            else
            {
                Print.ErrorDisplay("Command not found, write : 'help' if you want");
            }
            
        }

    }
}
