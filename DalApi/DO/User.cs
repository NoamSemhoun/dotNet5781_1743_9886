﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ID_user { get; set; } // רץ אוטומטי
        public bool is_Admin { get; set; }
    }
}
