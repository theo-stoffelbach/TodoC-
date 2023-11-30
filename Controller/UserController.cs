using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class UserController
    {
        public static void AddUser(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 1)) return;
            Print.Display("test");

            UserModel.AddUser(args[0]);
        }

        public static void ReadUsers()
        {
            List<int>? users = UserModel.GetAllUser();

            if (users == null) return;

            Print.Display("");
            users.ForEach(user =>
            {
                Print.Display(user.ToString());
            });
            Print.Display("");
        }

        public static void DeleteUser(string[] args, bool readOnlyMode)
        {
            if (!Verif.HasArgsLength(args, 1)) return;
            if (readOnlyMode && Verif.IsInt(args[1])) return;

            int id = Convertor.ConvStringToInt(args[0]);

            UserModel.DeleteUser(id);
        }


    }
}
