﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1ManmeetSinghKohli
{
    public class Motorcycle: Vehicle
    {
        public string Category { get; set; }
        public string Type { get; set; }
        public Motorcycle(int id, string name, double rentalprice, string category, string type, string available) : base(id, name, rentalprice, available)
        {
            Category = category;
            Type = type;
        }
    }
}
