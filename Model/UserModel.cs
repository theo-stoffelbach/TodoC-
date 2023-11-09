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
                Print.SucessDisplay($"{model.Name} Created with id : {model.Id}");
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
            }
        }



    }
}
