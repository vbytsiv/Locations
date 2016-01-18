using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Soply.Prototype.Locations.Model
{
    public class Location
    {
        public string Title { get; set; }
        public string[] KeyWords { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
