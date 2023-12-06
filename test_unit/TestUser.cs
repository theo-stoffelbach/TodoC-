using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.test_unit
{
    public class TestUser
    {
        private static int _idUserCreadted;

        public static void TestProtocol()
        {
            List<string> errorList = new List<string>();
            Print.Display("");

            if (!_testUserCreated()) return;
            if (!_testUserUpdate()) return;
            if (!_testUserDelete()) return;
        }


        private static bool _testUserCreated()
        {
            try
            {
                UserModel? userCreated = UserModel.AddUser("testUnit");

                if (userCreated == null) throw new Exception("To Create a user Because is Null");
                if (userCreated.Name != "testUnit") throw new Exception($"To Create a user Because is not the good name ( is {userCreated.Name})");

                Print.SuccessDisplay("To Create a user");
                _idUserCreadted = userCreated.Id;
                return true;
            }catch(Exception err)
            {
                Print.ErrorDisplay($"{err}");
                return false;
            }   
        }


        private static bool _testUserUpdate()
        {
            try
            {
                UserModel? user = UserModel.UpdateUser(_idUserCreadted, "testUnit2");

                if (user == null) throw new Exception("To Update a user Because is Null");
                if (user.Name != "testUnit2") throw new Exception($"To Update a user Because is not the good name ( is {user.Name})");

                Print.SuccessDisplay($"User {_idUserCreadted} is update");
                return true;
            }catch(Exception err)
            {
                Print.ErrorDisplay($"{err}");
                return false;
            }   
        }

        private static bool _testUserDelete()
        {
            try
            {
                UserModel? user = UserModel.DeleteUser(_idUserCreadted);

                if (user == null) throw new Exception("To Delete a user Because is Null");
                
                Print.SuccessDisplay($"User {_idUserCreadted} is delete");
                return true;
            }catch(Exception err)
            {
                Print.ErrorDisplay($"{err}");
                return false;
            }   
        }

    }
}
