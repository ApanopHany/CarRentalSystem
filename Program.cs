using SystemOfCars;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Transactoins> listTrans = Database.LoadTransactions();

            List<Manager> listM = new List<Manager>
            {
                new Manager("Sara Ahmed","01099887766",19000,new DateTime(2026,09,01))
            };
            List<Customer> listCus = Database.LoadCustomers();
            List<Cars> listCar = Database.LoadCars();
            bool choose = true;
            while (choose)
            {
                Console.WriteLine("CAR RENTAL SYSTEM - MAIN MENU ");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("1. Branch Information");
                Console.WriteLine("2. Show All Users ");
                Console.WriteLine("3. Show Available Cars");
                Console.WriteLine("4. Show All Fleet ");
                Console.WriteLine("5. Rent a Car ");
                Console.WriteLine("6. Return a Car");
                Console.WriteLine("7. Customer Rental History ");
                Console.WriteLine("8. Register New Customer ");
                Console.WriteLine("0. Exit ");
                Console.WriteLine();
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    switch (result)
                    {
                        case 0:
                            choose = false;
                            break;

                        case 1:
                            Branch br = new Branch("Elite Auto Rental - Nasr City Branch ",
                           "45 Abbas El Akkad St, Nasr City, Cairo", "01099887766", "Sat-Thu: 08:00 AM - 10:00 PM ", "Sara Ahmed");
                            br.print();
                            Console.WriteLine();
                            break;

                        case 2:
                            Console.WriteLine("All Registered Users");
                            Console.WriteLine("---------------------------------- ");
                            foreach (var item in listM)
                            {
                                Console.WriteLine("--- MANAGER PROFILE --- ");
                                item.print();
                            }
                            foreach (var item2 in listCus)
                            {
                                Console.WriteLine("--- CUSTOMER PROFILE --");
                                item2.print();
                            }
                            Console.WriteLine();
                            break;

                        case 3:
                            foreach (var item3 in listCar)
                            {
                                if (item3.Available == "Available")
                                {
                                    item3.print();
                                }
                            }
                            Console.WriteLine();
                            break;

                        case 4:
                            foreach (var item3 in listCar)
                            {
                                item3.print();
                            }
                            Console.WriteLine();
                            break;

                        case 5:
                            Console.Write("enter customer id:");
                            string? CusId = Console.ReadLine();
                            RentCar rent = new RentCar(CusId, listCus, listCar, listTrans);
                            Console.WriteLine();
                            break;

                        case 6:
                            Console.Write("Enter Car ID: ");
                            string? CarId = Console.ReadLine();
                            ReturnCar returnCAR = new ReturnCar(CarId, listCar, listTrans);
                            Console.WriteLine();
                            break;

                        case 7:
                            Console.Write("Enter Customer ID:");
                            string? CustomerId = Console.ReadLine();
                            bool hasTrans = false;
                            foreach (var item in listTrans)
                            {
                                if (CustomerId == item.CUSID)
                                {
                                    hasTrans = true;
                                    item.print();
                                }
                            }
                            if (hasTrans == false) Console.WriteLine("No rental history found.");
                            Console.WriteLine();
                            break;

                        case 8:
                            Console.WriteLine("Enter Full Name :");
                            string? name = Console.ReadLine();
                            Console.WriteLine("Enter Phone Number :");
                            string? phone = Console.ReadLine();
                            Console.WriteLine("Enter Email Address :");
                            string? email = Console.ReadLine();
                            Customer.Register(name, phone, email, listCus);
                            Console.WriteLine();
                            break;
                    }
                }
                else Console.WriteLine("The value is incorrect.");
            }
        }
    }
}