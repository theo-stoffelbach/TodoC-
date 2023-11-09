using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class UserController
    {
        public static void AddUser(string[] args)
        {
            if (!Utils.VerifArgs(args, 1)) return;
            Print.Display("test");

            UserModel.AddUser(args[0]);
        }
    }
}
