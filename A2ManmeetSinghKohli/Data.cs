using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ManmeetSinghKohli
{
    public class Data
    {

        private static string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial " +
            "Catalog=LawFirmDB;Integrated Security=True";
                    public static string ConnectionString { get { return connStr; } }

    }
}
