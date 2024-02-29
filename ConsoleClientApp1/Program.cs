using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClientApp1
{
    public class Program
    {
        static void Main(string[] args)
        {

            using (ServiceReference1.Service1Client client = new ServiceReference1.Service1Client())
            {
                Console.WriteLine(client.GetData(5));
                int sum = client.Add(1, 2);
                Console.WriteLine(sum);
            }
           // client.Close();
        }
    }
}
