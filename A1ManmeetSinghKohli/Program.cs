using ConsoleTables;
namespace A1ManmeetSinghKohli
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string m = "Assignment-1 by Manmeet Singh Kohli";
            Console.WriteLine($"{m,80}");
            Console.WriteLine("\n\n");



            Vehicle v1 = new Car(1, "Honda Civic", 69.9, "Sedan", "Standard", "Yes");
            Vehicle v2 = new Car(2, "Toyota Corolla", 69.9, "Sedan", "Standard", "Yes");
            Vehicle v3 = new Car(3, "Ford Explorer", 99.9, "SUV", "Standard", "Yes");
            Vehicle v4 = new Car(4, "Nissan Versa", 49.9, "Hatchback", "Standard", "Yes");
            Vehicle v5 = new Car(5, "Hyundai Tucson", 89.0, "SUV", "Standard", "Yes");
            Vehicle v6 = new Car(6, "Lamborghini Aventador", 189.9, "Sports", "Exotic", "Yes");
            Vehicle v7 = new Car(7, "Ferrari 488 GTB", 179.9, "Sports", "Exotic", "Yes");
            Vehicle v8 = new Car(8, "McLaren P1", 199.9, "Sports", "Exotic", "Yes");
            Vehicle v9 = new Motorcycle(9, "Suzuki Boulevard M109R", 49.9, "Cruiser", "Bike", "Yes");
            Vehicle v10 = new Motorcycle(10, "Harley-Davidson Street Glide", 79.9, "Cruiser", "Bike", "Yes");
            Vehicle v11 = new Motorcycle(11, "Honda CRF125", 39.9, "Dirt", "Bike", "Yes");
            Vehicle v12 = new Motorcycle(12, "Ducati Monster", 69.9, "Sports", "Bike", "Yes");
            Vehicle v13 = new Motorcycle(13, "Can-Am Spyder ", 59.9, "Cruiser", "Trike", "Yes");
            Vehicle v14 = new Motorcycle(14, "Polaris Slingshot ", 69.9, "Cruiser", "Trike", "Yes");


            List<Vehicle> vechiles = new List<Vehicle> {

            v1,v2,v3, v4, v5, v6, v7, v8, v9, v10, v11,v13,v14
            };
            Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-\n");

            int choice = 0;

            while (choice != 6)
            {
                try
                {
                    string options = "\n 1 - View all Vehicle\n 2 - View Available Vehicle\n 3 - View reserved Vehicle \n 4 - Reserve a Vehicle \n 5 - Cancel reservation \n 6 - Exit \n\n Enter  your choices : \n";
                    Console.WriteLine($"{options,0}");
                    choice = int.Parse(Console.ReadLine());
                    var table = new ConsoleTable("Id", "Name", "Rental Price", "Category", "Type", "Available");
                    Console.Clear();
                    Console.WriteLine(" ");
                    switch (choice)
                    {
                        case 1:

                            foreach (var vechile in vechiles)
                            {

                                if (vechile is Car car)
                                {
                                    table.AddRow(vechile.Id, vechile.Name, vechile.RentalPrice, car.Category, car.Type, vechile.Available);
                                }
                                else if (vechile is Motorcycle motorcycle)
                                {
                                    table.AddRow(vechile.Id, vechile.Name, vechile.RentalPrice, motorcycle.Category, motorcycle.Type, vechile.Available);
                                }
                                else
                                {

                                    table.AddRow(vechile.Id, vechile.Name, vechile.RentalPrice.ToString("C"), "N/A", "N/A", vechile.Available);
                                }

                            }

                            table.Write(Format.Minimal);
                            break;

                        case 2:


                            var onlyavailable = from vec in vechiles
                                                where vec.Available == "Yes"
                                                select vec;

                            foreach (var v in onlyavailable)
                            {
                                if (v is Car car)
                                {
                                    table.AddRow(v.Id, v.Name, v.RentalPrice, car.Category, car.Type, v.Available);
                                }
                                else if (v is Motorcycle motorcycle)
                                {
                                    table.AddRow(v.Id, v.Name, v.RentalPrice, motorcycle.Category, motorcycle.Type, v.Available);
                                }

                                else
                                {

                                    table.AddRow(v.Id, v.Name, v.RentalPrice.ToString("C"), "N/A", "N/A", v.Available);
                                }


                            }
                            Console.Clear();
                            table.Write(Format.Minimal);
                            break;
                        case 3:

                            var checkreservedvechile = from v in vechiles
                                                       select v;

                            if (checkreservedvechile.Any())//not use null here linq query null nhi dendi 
                            {
                                foreach (var v in checkreservedvechile)
                                {
                                    if (v is Car car)
                                    {

                                        if (v.Available == "No")
                                            table.AddRow(v.Id, v.Name, v.RentalPrice, car.Category, car.Type, v.Available);
                                    }
                                    else if (v is Motorcycle motorcycle)
                                    {
                                        if (v.Available == "No")
                                            table.AddRow(v.Id, v.Name, v.RentalPrice, motorcycle.Category, motorcycle.Type, v.Available);
                                    }

                                }
                            }


                            else
                            {
                                Console.WriteLine("Sorry their is no reserved vehicle");
                            }
                            table.Write(Format.Minimal);

                            break;


                        case 4:


                            Console.WriteLine("Please Enter The Id You Want To Reserve");
                            int rid = int.Parse(Console.ReadLine());

                              var reservevehicle = from v in vechiles
                                                     where v.Id == rid
                                                     select v;

                            
                                var reservedv = reservevehicle.First();
                                if (reservedv.Available.Equals("Yes") && reservedv != null)
                                {
                                    reservedv.Available = "No";
                                    Console.WriteLine("\nVehicle Reservation Successful\n");
                                    if (reservedv is Car car)
                                    {
                                        table.AddRow(reservedv.Id, reservedv.Name, reservedv.RentalPrice, car.Category, car.Type, reservedv.Available);
                                    }
                                    else if (reservedv is Motorcycle motorcycle)
                                    {
                                        table.AddRow(reservedv.Id, reservedv.Name, reservedv.RentalPrice, motorcycle.Category, motorcycle.Type, reservedv.Available);
                                    }
                                    table.Write(Format.Minimal);

                                }
                            
                            else
                            {
                                Console.WriteLine("Please enter a valid id");

                            }
                            break;

                        case 5:

                            Console.WriteLine("Please Enter The Id You Want To Cancel");
                            int cid = int.Parse(Console.ReadLine());
                            
                                var cancelvehicle = from v in vechiles
                                                    where v.Id == cid
                                                    select v;
                                                            
                                    var rvehicl = cancelvehicle.First();
                            if (rvehicl.Available.Equals("No") && rvehicl != null)
                            { 
                                rvehicl.Available = "Yes";
                                    Console.WriteLine("\nVehicle Cancellation Successful\n");
                                    if (rvehicl is Car car)
                                    {
                                        table.AddRow(rvehicl.Id, rvehicl.Name, rvehicl.RentalPrice, car.Category, car.Type, rvehicl.Available);
                                    }
                                    else if (rvehicl is Motorcycle motorcycle)
                                    {
                                        table.AddRow(rvehicl.Id, rvehicl.Name, rvehicl.RentalPrice, motorcycle.Category, motorcycle.Type, rvehicl.Available);
                                    }
                                    table.Write(Format.Minimal);

                                }

                                else
                                {
                                    Console.WriteLine("Please enter a valid id");

                                }
                            break;




                        case 6:
                            {
                                Console.WriteLine("You have exited the program.");

                                break;
                            }

                    }



                }
                catch  (FormatException e)
                {
                    Console.WriteLine("Please enter a valid input integer between 1-4");
                }

            }
        }
    }
}
    
