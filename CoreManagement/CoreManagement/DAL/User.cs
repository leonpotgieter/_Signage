using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    class User
    {
        public string Id { get; set; }
        public string Loginid { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public bool Enabled { get; set; }
        public DateTime Lastlogin { get; set; }
        public string Groupid { get; set; }
    }
}
