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

        public static List<UserModel> userModels(List<int> listUsersId)
        {
            return UserModel.SearchUserWithId(listUsersId);
        }
    }
}
