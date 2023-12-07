using UltimateProject.View;

namespace UltimateProject.Controller
{
    public class Notif
    {
        private int _id;
        private DateTime _date;


        /// <summary>
        /// To create a new notif
        /// </summary>
        /// <param name="id"></param>
        public Notif(int id)
        {
            _id = id;
            _date = DateTime.Now.AddMinutes(1);
        }

        /// <summary>
        /// To test if the notif is out of date
        /// </summary>
        /// <param name="notifs"> the notifications concern </param>
        /// <returns> the new list of notifications </returns>
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

            _PrintDeleteNotif(notifsDeleted);
            return notifs;
        }

        /// <summary>
        /// To add a new notif ( and string him )
        /// </summary>
        /// <returns> To string the notif </returns>
        public override string ToString()
        {
            return $"the todo {_id} still has no description! please add one with the command: 'addDescTodo {_id} Description' \"";
        }

        /// <summary>
        /// To check and print the notif
        /// </summary>
        /// <param name="notifsDeleted"> the list of notif to print </param>
        private static void _PrintDeleteNotif(List<string> notifsDeleted)
        {
            if (notifsDeleted.Count > 0)
            {
                Print.Display("\n----- Notif -----");
                foreach (var notif in notifsDeleted)
                {
                    Print.Display(notif.ToString());

                }
                Print.Display("-----------------\n");
            }
        }

    }
}