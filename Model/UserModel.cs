using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UltimateProject.View;

namespace UltimateProject.Model
{
    public class UserModel
    {
 
        public static EFContext db = new EFContext();
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
                db.Add(model);
                db.SaveChanges();
                Print.SuccessDisplay($"{model.Name} Created with id : {model.Id}");
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
            }
        }

        public static UserModel? SearchUserWithId(int id)
        {
            try
            {
                UserModel? userModel = db.UserModels.Find(id);
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

        public static List<UserModel>? SearchUserWithId(List<int> listId)
        {
            try
            {
                List<UserModel> userProfiles = db.UserModels.Where(user => listId.Contains(user.Id)).ToList();

                if (userProfiles.Count == 0)
                {
                    Print.ErrorDisplay($"Il n'y a pas d'utilisateur avec les IDs mentionnés.");
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

    }
}
