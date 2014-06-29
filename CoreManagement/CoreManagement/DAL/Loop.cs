using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    class Loop
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Createdon { get; set; }
        public string Createdby { get; set; }
        public DateTime Expiry { get; set; }
        public bool Active { get; set; }
        public string Templateid { get; set; }
        public string Templatename { get; set; }
    }
}
