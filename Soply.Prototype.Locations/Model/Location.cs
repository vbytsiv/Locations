﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Soply.Prototype.Locations.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string Title1 { get; set; }
        public string KeyWords { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
