using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    class Schedule
    {
        public string Id { get; set; }
        public string Loopid { get; set; }
        public string Loopname { get; set; }
        public string Groupid { get; set; }
        public string Groupname { get; set; }
        public string Screenid { get; set; }
        public string Screenname { get; set; }
        public string Looptype { get; set; }
        public DateTime Loopstart { get; set; }
        public DateTime Loopend { get; set; }
        public bool Active { get; set; }
        public string Createdby { get; set; }
        public DateTime Createdon { get; set; }
    }
}
