using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    class Screen
    {
        public string Id { get; set; }
        public string Screenname { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Groupid { get; set; }
        public string Ip { get; set; }
        public DateTime Lastactive { get; set; }
    }
}
