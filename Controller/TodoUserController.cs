using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UltimateProject.Model;

namespace UltimateProject.Controller
{
    public class TodoUserController
    {
        public static void AddUserTodo(string[] args)
        {
            if(!Utils.VerifArgs(args,2)) return;

            int idUser = int.Parse(args[0]);
            int idTodo = int.Parse(args[1]);

            UserTodosModel.AddUserTodoModel(idUser,idTodo);
        }
    }
}
