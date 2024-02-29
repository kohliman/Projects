using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1ManmeetSinghKohli
{
    public class Car: Vehicle
    {
        public string Category { get; set; }
        public string Type { get; set; }
        public Car(int id, string name,double rentalprice,string category,string type, string available) : base(id,name,rentalprice,available)
        {
            Category = category;
            Type = type;
        }

    }
}
