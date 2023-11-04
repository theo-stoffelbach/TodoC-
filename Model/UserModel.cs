using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateProject.Model
{
    [Keyless]
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        List<int> _userIds { get; set; }

        public UserModel(string name)
        {
            Name = name;
        }
    }
}
