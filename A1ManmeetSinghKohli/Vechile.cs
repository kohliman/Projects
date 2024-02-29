using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1ManmeetSinghKohli
{
    public class Vehicle
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public double RentalPrice { get; set; }
        public string Available { get; set; }

            public Vehicle(int id, string name, double rentalprice,string available)
        {
            Id = id;
            Name = name;
            RentalPrice = rentalprice;
            Available = available;

        }
    }
}
