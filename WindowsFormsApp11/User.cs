using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp11
{
    public class User
    {
        public static List<User> allUser = new List<User>();
        public string username { get; set; }
        public string password { get; set; }
        public int points { get; set; }
        public int id { get; set; }
    }
}
