using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Soply.Prototype.Locations.ViewModel
{
    public class LocationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string KeyWords { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
