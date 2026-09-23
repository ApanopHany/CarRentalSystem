using Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace SystemOfCars
{
    internal class ReturnCar
    {
        public ReturnCar(string ReturnedCarId, List<Cars> list, List<Transactoins> listTrans)
        {
            bool checker1 = false;
            bool checker2 = false;
            foreach (var item in list)
            {
                if (ReturnedCarId == item.Id)
                {
                    checker1 = true;
                    if (item.Available == "Rented" || item.Available == "Not Available")
                    {
                        foreach (var item2 in listTrans)
                        {
                            if (ReturnedCarId == item2.CARID && item2.Status == "Active")
                            {
                                checker2 = true;
                                item2.CloseTransaction(list);
                                break;
                            }
                        }
                        if (checker2 == false)
                        {
                            Console.WriteLine("No active transaction for this car. ");
                            break;
                        }

                        item.IsAvailable();
                        int difference = (DateTime.Now.Date - item.DueTo.Date).Days;
                        try
                        {
                            using (var connection = Database.GetConnection())
                            {
                                connection.Open();
                                string updateCarQuery = "UPDATE Cars SET Available = 'Available', DueTo = NULL WHERE Id = @CarId";
                                using (var cmdCar = new Microsoft.Data.SqlClient.SqlCommand(updateCarQuery, connection))
                                {
                                    cmdCar.Parameters.AddWithValue("@CarId", ReturnedCarId);
                                    cmdCar.ExecuteNonQuery();
                                }
                                decimal fee = difference > 0 ? difference * 150 : 0;
                                string updateTransQuery = "UPDATE Transactions SET Status = 'Closed', ToDay = @ReturnDate, Fee = @Fee WHERE CARID = @CarId AND Status = 'Active'";
                                using (var cmdTrans = new Microsoft.Data.SqlClient.SqlCommand(updateTransQuery, connection))
                                {
                                    cmdTrans.Parameters.AddWithValue("@ReturnDate", DateTime.Now);
                                    cmdTrans.Parameters.AddWithValue("@Fee", fee);
                                    cmdTrans.Parameters.AddWithValue("@CarId", ReturnedCarId);
                                    cmdTrans.ExecuteNonQuery();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Database Error: " + ex.Message);
                        }

                        Console.WriteLine($"Car [{item.Id}]: {item.Model} returned.");
                        if (difference <= 0)
                        {
                            Console.WriteLine("Returned on time. No late fee. ");
                        }
                        if (difference > 0)
                        {
                            Console.WriteLine($"Late return fee: {difference * 150:F2} EGP");
                        }
                        break;
                    }
                    break;
                }
            }
            if (checker1 == false)
            {
                Console.WriteLine("Car not found.");
            }
        }
    }
}