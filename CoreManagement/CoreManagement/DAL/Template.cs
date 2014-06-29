using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    class Template
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string Zonename { get; set; }
        public string Zonedescription { get; set; }
        public string ZoneType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Opacity { get; set; }
    }
}
