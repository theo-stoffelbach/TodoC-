using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.Controller
{

    public record Menu
    {
        private static string _chooseUser;
        private static string[] _arguments;
        public List<Notif> NotifList = new List<Notif>();
        public static Dictionary<string, Action> ChooseMenu;

        private static Menu _instance = null;

        private Menu()
        {
            ChooseMenu = new Dictionary<string, Action>
            {
                { "exit", () => Environment.Exit(0)},
                { "createtodo", () => TodoController.AddTodo(_arguments)},
                { "readtodos", () => TodoController.ReadTodos(_arguments)},
                { "updatetodo", () => TodoController.UpdateTodos(_arguments)},
                { "deletetodo", () => TodoController.DeleteTodo(_arguments)},
                { "activatetodo", () => TodoController.ActivateTodo(_arguments)},
                { "adddesctodo", () => TodoController.AddDescTodo(_arguments)},
                { "filtertodo", () => TodoController.FilterTodo(_arguments)}
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

            if (ChooseMenu.ContainsKey(_chooseUser))
            {
                Action valeur = ChooseMenu[_chooseUser.ToLower()];
                valeur();
            }
            else
            {
                Print.ErrorDisplay("Command not found, write : 'help' if you want");
            }

            MenuTest();
        }

        public void MenuFilter()
        {
            Print.PrintGetValue("Enter a command");
            string inputUser = Console.ReadLine();

            string[] command = inputUser.Split(' ');
            _chooseUser = command[0].ToLower();
            _arguments = command.Skip(1).ToArray();

            if (ChooseMenu.ContainsKey(_chooseUser))
            {
                Action valeur = ChooseMenu[_chooseUser.ToLower()];
                valeur();
            }
            else
            {
                Print.ErrorDisplay("Command not found, write : 'help' if you want");
            }

            Print.Display("");
            MenuTest();
        }


    }
}
