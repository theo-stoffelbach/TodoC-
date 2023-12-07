using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class UserController
    {

        /// <summary>
        /// To add a user
        /// </summary>
        /// <param name="args"> name of user</param>
        /// <param name="readOnlyMode"> If it ReadOnly </param>
        public static void AddUser(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 1)) return;

            UserModel? user = UserModel.AddUser(args[0]);

            if (user == null)
            {
                Print.ErrorDisplay("User not created");
                return;
            }

            Print.SuccessDisplay($"{user.Name} Created with id : {user.Id}");

        }

        /// <summary>
        /// To read a user
        /// </summary>
        public static void ReadUsers()
        {
            List<UserModel>? users = UserModel.GetAllUser();

            if (users == null) return;

            Print.Display("");
            users.ForEach(user =>
            {
                Print.Display(user.ToString());
            });
            Print.Display("");
        }

        /// <summary>
        /// To update a user
        /// </summary>
        /// <param name="args"> 
        /// 0 : id of user
        /// 1 : name of user
        /// </param>
        /// <param name="readOnlyMode"> If it ReadOnly </param>
        public static void UpdateTodo(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 2)) return;
            if (readOnlyMode && Verif.IsInt(args[0])) return;

            int userId = Convertor.ConvStringToInt(args[0]);

            UserModel? user = UserModel.UpdateUser(userId, args[1]);
    
            if (user == null)
            {
                Print.Display("User not updated");
                return;
            };
            Print.SuccessDisplay("Todo Updated with id : " + user.Id);
        }

        /// <summary>
        /// To delete a user
        /// </summary>
        /// <param name="args"> Id to delete </param>
        /// <param name="readOnlyMode"> If it ReadOnly </param>
        public static void DeleteUser(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 1)) return;
            if (readOnlyMode && Verif.IsInt(args[1])) return;

            int id = Convertor.ConvStringToInt(args[0]);

            UserModel? user = UserModel.DeleteUser(id);

            if (user == null) Print.ErrorDisplay($"Il n'y pas de user avec l'Id : {args[0]}");

            Print.SuccessDisplay($"User {args[0]} is delete");
        }


    }
}
