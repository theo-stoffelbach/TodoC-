using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using UltimateProject.View;

namespace UltimateProject.Model
{
    public class UserModel
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        private static EFContext _db = EFContext.Instance;
        
        public UserModel(string name)
        {
            Name = name;
        }

        /// <summary>
        /// To create a user Id
        /// </summary>
        /// <param name="user"> name </param>
        /// <returns></returns>
        public static UserModel? AddUser(string user)
        {
            try
            {
                UserModel model = new UserModel(user);
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

        /// <summary>
        /// To get all users
        /// </summary>
        /// <returns></returns>
        public static List<UserModel>? GetAllUser()
        {
            try
            {
                List<UserModel>? userProfiles = _db.UserModels.ToList();

                if (userProfiles.Count == 0)
                {
                    Print.ErrorDisplay($"Il n'y a pas d'utilisateur.");
                    return null;
                }

                return userProfiles;
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
                return null;
            }
        }
        /// <summary>
        /// To get all users Id
        /// </summary>
        /// <returns></returns>
        public static List<int>? GetAllUserId()
        {
            try
            {
                List<UserModel>? userProfiles = _db.UserModels.ToList();
                List<int> listId = new List<int>() { };

                if (userProfiles.Count == 0)
                {
                    Print.ErrorDisplay($"There is no user.");
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

        /// <summary>
        /// To get a user with his id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// To get a user with his id
        /// </summary>
        /// <param name="id"> User id </param>
        /// <returns></returns>
        public static UserModel? SearchUserWithId(int id)
        {
            try
            {
                UserModel? userModel = _db.UserModels.Find(id);
                if (userModel == null)
                {
                    Print.ErrorDisplay($"There is no user.");
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

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id"> User id </param>
        /// <param name="name"> string of the name </param>
        /// <returns></returns>
        public static UserModel? UpdateUser(int id,string name)
        {
            UserModel? userdb = _db.UserModels.Find(id);
            if (userdb != null)
            {
                try
                {
                    userdb.Name = name;
                    _db.Update(userdb);
                    _db.SaveChanges();
                    return userdb;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="todoId"> the int of todo</param>
        /// <returns></returns>
        public static UserModel? DeleteUser(int todoId)
        {
            try
            {
                UserModel? userModel = _db.UserModels.Find(todoId);
                if (userModel == null)
                {
                    return null;
                }

                if (!_hasDeleteTodosWithUserId(todoId)) return null;

                _db.UserModels.Remove(userModel);
                _db.SaveChanges();
                return userModel;
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
            }
            return null;
        }

        /// <summary>
        /// Has delete todos with user id
        /// </summary>
        /// <param name="todoId"> is a user id </param>
        /// <returns></returns>
        private static bool _hasDeleteTodosWithUserId(int todoId)
        {
            List<TodoModel>? todos = TodoModel.GetTodosWithUserId(todoId);

            if (todos == null)
            {
                Print.ErrorDisplay($"There is no user with {todoId}");
                return false;
            }

            foreach (var todo in todos)
            {
                todo.UserId = null;
            }

            return true;
        }

        public  string ToString()
        {
            return @$"Id : {this.Id}, and his name is : {this.Name}, ";
        }
    }
}
