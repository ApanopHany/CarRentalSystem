using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using SystemOfCars;

namespace Project
{
    internal class Database
    {
        private static string connectionString = "Server=.\\SQLEXPRESS;Database=SystemOfCars;Trusted_Connection=True;TrustServerCertificate=True;"; public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
        public static List<Customer> LoadCustomers()
        {
            List<Customer> customersFromDb = new List<Customer>();
            string query = "SELECT Id, NameCus, Phone, JoinDate, Email FROM Customer";

            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader["Id"].ToString();
                                string name = reader["NameCus"].ToString();
                                string phone = reader["Phone"].ToString();
                                DateTime joinDate = Convert.ToDateTime(reader["JoinDate"]);
                                string email = reader["Email"].ToString();
                                Customer customer = new Customer(name, phone, joinDate, email);
                                customersFromDb.Add(customer);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("System ERROR ! " + ex.Message);
                    }
                }
            }
            return customersFromDb;
        }
        public static List<Cars> LoadCars()
        {
            List<Cars> carsFromDb = new List<Cars>();
            string query = "SELECT Id, Model, Condition, Available, DueTo FROM Cars";

            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string model = reader["Model"].ToString();
                                string condition = reader["Condition"].ToString();
                                string available = reader["Available"].ToString();

                                Cars car = new Cars(model, condition);
                                if (available == "Not Available" || available == "Rented")
                                {
                                    car.IsNotAvailable();
                                }
                                else
                                {
                                    car.IsAvailable();
                                }

                                if (reader["DueTo"] != DBNull.Value)
                                {
                                    car.DueTo = Convert.ToDateTime(reader["DueTo"]);
                                }

                                carsFromDb.Add(car);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("System ERROR ! " + ex.Message);
                    }
                }
            }
            return carsFromDb;
        }
        public static List<Transactoins> LoadTransactions()
        {
            List<Transactoins> transFromDb = new List<Transactoins>();
            string query = "SELECT Id, CUSID, CARID, FromDay, ToDay, Status, Fee FROM Transactions";

            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string cusId = reader["CUSID"].ToString();
                                string carId = reader["CARID"].ToString();
                                DateTime fromDay = Convert.ToDateTime(reader["FromDay"]);
                                string status = reader["Status"].ToString();
                                int transId = Convert.ToInt32(reader["Id"]);
                                Transactoins tran = new Transactoins(cusId, carId, null, null, transFromDb);
                                tran.Id = transId;
                                tran.From = fromDay;
                                tran.To = fromDay.AddDays(14);

                                if (reader["ToDay"] != DBNull.Value)
                                {
                                    tran.To = Convert.ToDateTime(reader["ToDay"]);
                                }

                                transFromDb.Add(tran);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("System ERROR ! " + ex.Message);
                    }
                }
            }
            return transFromDb;
        }
    }
}