using Microsoft.EntityFrameworkCore;
using UltimateProject.View;

namespace UltimateProject.Model
{
    [Keyless]
    public class UserTodosModel
    {
        public static EFContext db = new EFContext();
        private int _id { get; set; }
        private int _userID { get; set; }
        private int _todoId { get; set; }

        public UserTodosModel() { }

        public static void AddDescTodo(int todoId, int userId)
        {
            try
            {
                UserTodosModel model = new UserTodosModel();
                model._userID = userId;
                model._todoId = todoId;

                db.Add(model);
                db.SaveChanges();
                Print.SucessDisplay("Todo Created with id : " + model._id);
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
            }
        }

    }
}

