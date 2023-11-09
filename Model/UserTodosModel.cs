using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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
                Print.SucessDisplay("Todo Created with id : " + model.Id);
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
            }
        }

    }
}

