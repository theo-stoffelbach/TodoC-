using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using UltimateProject.Controller;
using UltimateProject.View;

namespace UltimateProject.Model
{
    public class UserTodosModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TodoId { get; set; }

        public static EFContext db = new EFContext();


        public UserTodosModel() { }

        public static void AddUserTodoModel(int todoId, int userId)
        {
            try
            {
                UserTodosModel model = new UserTodosModel();
                model.UserId = userId;
                model.TodoId = todoId; 

                db.Add(model);
                db.SaveChanges();
                //Print.SucessDisplay("TodoUser Created with id : " + model.Id);
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"with bdd to : {err}");
            }
        }

        public static List<UserTodosModel> ReadIdTodoModel(int todoId)
        {
            try
            {
                var resultat = db.UserTodosModels.Where(e => e.TodoId == todoId).ToList();
                return resultat;
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"with bdd to : {err}");
                return null;
            }
        }
        public static List<UserTodosModel> ReadIdUserModel(int userId)
        {
            try
            {
                return db.UserTodosModels.Where(e => e.UserId == userId).ToList();
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"with bdd to : {err}");
                return null;
            }
        }

        public static List<UserTodosModel> DeleteAllRefOfTodo(int userId)
        {
            try
            {
                return db.UserTodosModels.Where(e => e.UserId == userId).ToList();
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"with bdd to : {err}");
                return null;
            }
        }

        public static void DeleteAllRefOfTodoModel(int todoId)
        {
            List<UserTodosModel> listUserTodoId = db.UserTodosModels.Where(e => e.TodoId == todoId).ToList();
            Print.Display("test : " + listUserTodoId.Count);
            foreach (var userTodo in listUserTodoId)
            {
                db.Remove(userTodo);
            }
            db.SaveChanges();
        }

    }
}

