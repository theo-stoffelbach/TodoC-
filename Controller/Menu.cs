using TP_Theo_Stoffelbach.Controller;
using UltimateProject.Model;
using UltimateProject.View;

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
        private static Logger _logger = new Logger();

        private Menu()
        {
            ChooseMenu = new Dictionary<string, Action>
            {
                { "exit", () => Environment.Exit(0)},
                { "help", () => Help()},
                { "createtodo", () => TodoController.AddTodo(_arguments,_readOnly)},
                { "updatetodo", () => TodoController.UpdateTodos(_arguments,_readOnly)},
                { "deletetodo", () => TodoController.DeleteTodo(_arguments,_readOnly)},
                { "completedtodo", () => TodoController.ActivateTodo(_arguments,_readOnly)},
                { "adddesctodo",() => TodoController.AddDescTodo(_arguments,_readOnly)},
                { "showdetailtodos",  () => TodoController.ReadDetailsTodos(_arguments,_readOnly)},

                { "filtertodo", () => TodoController.FilterTodo(_arguments)},
                { "showtodos",  () => TodoController.ReadTodos()},
                { "showstats", () => Stats.Show()},
                { "zip", () => _logger.ZipAllLogs()},

                { "importcsv", () => TodoController.ImportFromCsv(_arguments)},
                { "exportcsv", () => TodoModel.ExportToCsv()},

                { "readfile", () => TodoController.readFile()},

                { "createuser", () => UserController.AddUser(_arguments, _readOnly)},

                { "addusertodo", () => TodoUserController.AddUserTodo(_arguments, _readOnly)},
                
            };
        }

        public static Menu GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Menu();
            }   
            return _instance;
        }

        public void AddNotifTodo(int id)
        {
            NotifList.Add(new Notif(id));
        }

        public void MenuTest()
        {
            Notif.TestNotifTime(NotifList);

            Print.PrintGetValue("Enter a command");
            string inputUser = Console.ReadLine();

            string[] command = inputUser.Split(' ');
            _chooseUser = command[0].ToLower();
            _arguments = command.Skip(1).ToArray();
            _readOnly = false;

            if (ChooseMenu.ContainsKey(_chooseUser))
            {
                ChooseMenu[_chooseUser.ToLower()](); // Here to execute command
                _logger.AddNewLogAction(DateTime.Now + " | " + _chooseUser + " " + string.Join(" ", _arguments));
            }
            else
            {
                Print.ErrorDisplay("Command not found, write : 'help' if you want");
            }
            MenuTest();
        }
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

        public void MenuFilter()
        {
            Print.PrintGetValue("Enter a command");
            string inputUser = Console.ReadLine();

            string[] command = inputUser.Split(' ');
            _chooseUser = command[0].ToLower();
            _arguments = command.Skip(1).ToArray();

            _ChooseMenu();

            Print.Display("");
            MenuTest();
        }

        public void Help()
        {
            Print.Display("\n ---------- Help ----------\n"
            + "You have the command then what is do and after the | you have command with :"
            + "{ XXXXXX } : obligator argument and"
            + "[ XXXXXX ] : optionnal argument"
            + "\n ---------- Todo ----------\n"
            + "createtodo : Use to create a todo | Createtodo {Title} {Priority} {DateDue} {UserId} [Description]"
            + "updatetodo: Use to update a todo | Update {Id} {Title} {Description} {Priority} {DateDue} "
            + "deletetodo : Use to delete a todo | Deletetodo {TodoId} OR Deletetodo {Priority} OR Deletetodo all"
            + "completedtodo : Use to complete Or not complete | completedtodo {TodoId}"
            + "adddesctodo : Use to add a description on a todo | adddesctodo {TodoId} {Description}"
            + "\n ---------- User ----------\n"
            + "createuser : Use to "
            + "\n ---------- Show ----------\n"
            + "showdetailtodos : Use to show a more detail on a todo | showdetailtodos {TodoId}"
            + "filtertodo: Use to update a todo | Update {Id} {Title} {Description} {Priority} {DateDue} "
            + "showtodos : Use to delete a todo | Deletetodo {TodoId} OR Deletetodo {Priority} OR Deletetodo all"
            + "showstats: Use to complete Or not complete | completedtodo {TodoId}"
            + "zip : Use to add a description on a todo | adddesctodo {TodoId} {Description}");

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
