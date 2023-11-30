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

        public static void AddUser(string str)
        {
            try
            {
                UserModel model = new UserModel("theo");
                _db.Add(model);
                _db.SaveChanges();
                Print.SuccessDisplay($"{model.Name} Created with id : {model.Id}");
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
            }
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

        public static UserModel? DeleteUser(int id)
        {
            try
            {
                UserModel? userModel = _db.UserModels.Find(id);
                if (userModel == null)
                {
                    Print.ErrorDisplay($"Il n'y pas de user avec l'Id : {id}");
                    return null;
                }

                _db.UserModels.Remove(userModel);
                _db.SaveChanges();
                Print.SuccessDisplay($"User {id} is delete");
                return userModel;
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
            }

            return null;
        }

    }
}
