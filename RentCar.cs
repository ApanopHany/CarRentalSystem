using Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace SystemOfCars
{
    internal class RentCar
    {
        public RentCar(string customerId, List<Customer> list1, List<Cars> list2, List<Transactoins> list3)
        {
            bool CUS_checker = false;
            foreach (var item in list1)
            {
                if (customerId == item.Id)
                {
                    CUS_checker = true;
                    bool CAR_checker = false;
                    Console.WriteLine("---------------------------------- ");
                    Console.WriteLine("Available Fleet:");
                    Console.WriteLine("---------------------------------- ");
                    foreach (var item2 in list2)
                    {
                        if (item2.Available == "Available")
                            item2.print();
                    }
                    Console.Write("Enter Car ID to rent:");
                    string carId = Console.ReadLine();
                    foreach (var item3 in list2)
                    {
                        if (carId == item3.Id)
                        {
                            CAR_checker = true;
                            if (item3.Available == "Available")
                            {
                                DateTime rentDate = DateTime.Now;
                                Transactoins tran = new Transactoins(customerId, carId, list1, list2, list3);
                                list3.Add(tran);
                                tran.OpenTransaction(list1, list2, list3, rentDate);
                                item3.IsNotAvailable();
                                item3.DueTo = rentDate.AddDays(14);
                                try
                                {
                                    using (var connection = Database.GetConnection())
                                    {
                                        connection.Open();
                                        string insertQuery = "INSERT INTO Transactions (CUSID, CARID, FromDay, ToDay, Status) VALUES (@CusId, @CarId, @FromDay, @ToDay, @Status)";
                                        using (var cmdInsert = new Microsoft.Data.SqlClient.SqlCommand(insertQuery, connection))
                                        {
                                            cmdInsert.Parameters.AddWithValue("@CusId", customerId);
                                            cmdInsert.Parameters.AddWithValue("@CarId", carId);
                                            cmdInsert.Parameters.AddWithValue("@FromDay", rentDate);
                                            cmdInsert.Parameters.AddWithValue("@ToDay", item3.DueTo);
                                            cmdInsert.Parameters.AddWithValue("@Status", "Active");
                                            cmdInsert.ExecuteNonQuery();
                                        }
                                        string updateCarQuery = "UPDATE Cars SET Available = 'Not Available', DueTo = @DueTo WHERE Id = @CarId";
                                        using (var cmdUpdate = new Microsoft.Data.SqlClient.SqlCommand(updateCarQuery, connection))
                                        {
                                            cmdUpdate.Parameters.AddWithValue("@DueTo", item3.DueTo);
                                            cmdUpdate.Parameters.AddWithValue("@CarId", carId);
                                            cmdUpdate.ExecuteNonQuery();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Database Error: " + ex.Message);
                                }

                                Console.WriteLine($"Car [{item3.Id}] {item3.Model} rented by {item.Name}.");
                                Console.WriteLine($"Due date : {item3.DueTo:dd/MM/yyyy}");
                            }
                            else { Console.WriteLine($"Car {carId} is not available "); }
                            break;
                        }
                    }
                    if (CAR_checker == false)
                    {
                        Console.WriteLine("Car not found.");
                    }
                    break;
                }
            }
            if (CUS_checker == false)
                Console.WriteLine("Customer not found ! ");
        }
    }
}