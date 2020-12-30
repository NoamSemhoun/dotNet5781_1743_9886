using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ID_user { get; set; } // רץ אוטומטי
        public bool is_Admin { get; set; }
    }
}
