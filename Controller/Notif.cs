using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<string> notifsDeleted = new List<string>();
            for (int i = 0; i < notifs.Count - 1; i++)
            {
                if (notifs[i]._date >= DateTime.Now)
                {
                    notifsDeleted.Add(notifs[i].ToString());
                    notifs.RemoveAt(i); 
                }
            }

            return notifs;

        }



        public override string ToString()
        {
            return $"Id";
        }


    }
}
