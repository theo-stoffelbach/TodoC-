using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UltimateProject.View;

namespace UltimateProject.Model
{
    public class UserModel
    {

        private static EFContext _db = EFContext.Instance;
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped] public List<int> _userIds { get; set; }

        public UserModel(string name)
        {
            Name = name;
        }

        public static UserModel? AddUser(string str)
        {
            try
            {
                UserModel model = new UserModel("theo");
                _db.Add(model);
                _db.SaveChanges();
                return model;
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
                return null;
            }
        }

        public static List<TodoModel>? GetUserIds(int idUser) {
            List<TodoModel> todos = _db.TodoModels.Where(todo => todo.UserId == idUser).ToList();
            
            if (todos.Count == 0)
            {
                Print.ErrorDisplay($"Il n'y pas de todo avec l'Id : {idUser}");
                return null;
            }   

            return todos;
        }

        public static List<int>? GetAllUser()
        {
            try
            {
                List<UserModel>? userProfiles = _db.UserModels.ToList();
                List<int> listId = new List<int>() { };

                if (userProfiles.Count == 0)
                {
                    Print.ErrorDisplay($"Il n'y a pas d'utilisateur.");
                    return null;
                }

                foreach (var user in userProfiles)
                {
                    listId.Add(user.Id);
                }

                return listId;
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
                return null;
            }
        }

        public static bool IsUserIdToTodos(int userId)
        {
            try
            {
                bool hasTodo = _db.TodoModels.Any(todo => todo.UserId == userId);
                return hasTodo;
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
                return false;
            }
        }

        public static UserModel? SearchUserWithId(int id)
        {
            try
            {
                UserModel? userModel = _db.UserModels.Find(id);
                if (userModel == null)
                {
                    Print.ErrorDisplay($"Il n'y pas de user avec l'Id : {id}");
                    return null;
                }
                return userModel;
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
            }

            return null;
        }

        public static UserModel? DeleteUser(int todoId)
        {
            try
            {
                UserModel? userModel = _db.UserModels.Find(todoId);
                if (userModel == null)
                {
                    Print.ErrorDisplay($"Il n'y pas de user avec l'Id : {todoId}");
                    return null;
                }

                if (_hasDeleteTodosWithUserId(todoId)) return null;

                _db.UserModels.Remove(userModel);
                _db.SaveChanges();
                Print.SuccessDisplay($"User {todoId} is delete");
                return userModel;
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
            }
            return null;
        }


        private static bool _hasDeleteTodosWithUserId(int todoId)
        {
            List<TodoModel>? todos = TodoModel.GetTodosWithUserId(todoId);

            if (todos == null)
            {
                Print.ErrorDisplay($"Il n'y pas de todo avec l'Id : {todoId}");
                return false;
            }

            foreach (var todo in todos)
            {
                todo.UserId = null;
            }

            return true;
        }
    }
}
