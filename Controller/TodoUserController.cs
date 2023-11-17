﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class TodoUserController
    {
        public static void AddUserTodo(string[] args, bool readOnlyMode)
        {
            if(!Utils.VerifArgs(args,2)) return;

            int idTodo = int.Parse(args[0]);
            int idUser = int.Parse(args[1]);

            if (VerifAlreadyExist(idTodo,idUser)) return;

            UserTodosModel.AddUserTodoModel(idUser,idTodo);
        }

        public static bool VerifAlreadyExist(int todoId, int UserId) 
        {
            List<int> listUsersTodos = ReadUserTodosModelWithId(todoId);
            if (listUsersTodos.Contains(UserId))
            {
                Print.ErrorDisplay("L'utilisateur existe déjà pour cette todo ");
                return true;
            }
            return false;
        }

        public static List<int> ReadUserTodosModelWithId(int id)
        {
            List<UserTodosModel> listUserTodosModel = UserTodosModel.ReadIdTodoModel(id);
            return listUserTodosModel.Select(userstodo => userstodo.UserId).ToList();
        }
        public static List<int> DeleteAllRefOfTodo(int todoId)
        {
            List<UserTodosModel> listUserTodosModel = UserTodosModel.ReadIdTodoModel(todoId);
            foreach (var userTodo in listUserTodosModel)
            {
                userTodo.delete();
            }
            return listUserTodosModel.Select(userstodo => userstodo.UserId).ToList();
        }


    }
}
