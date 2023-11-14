using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class Notif 
    {
        private int _id;
        private DateTime _date;

        public Notif(int id)
        {
            _id = id;
            _date = DateTime.Now.AddMinutes(1);
        }

        public static List<Notif> TestNotifTime(List<Notif> notifs)
        {
            if (notifs.Count == 0) return notifs;
            List<string> notifsDeleted = new List<string>() { };
            for (int i = 0; i <= notifs.Count - 1; i++)
            {

                if (notifs[i]._date <= DateTime.Now)
                {
                    notifsDeleted.Add(notifs[i].ToString());
                    notifs.RemoveAt(i); 
                }
            }

            if (notifs.Count > 0) Print.Display(notifs[0]._date.ToString());

            Console.WriteLine(notifsDeleted.Count + $" : {notifs.Count - 1}");
            if (notifsDeleted.Count > 0)
            {
                Print.Display("\n----- Notif -----");
                foreach (var notif in notifsDeleted)
                {
                    Print.Display(notif.ToString());

                }
                Print.Display("-----------------\n");
            }


            return notifs;
        }



        public override string ToString()
        {
            return $"la todo {_id} n'as toujours pas de description ! merci d'en mettre une avec la commande : 'addDescTodo {_id} Description' ";
        }


    }
}
