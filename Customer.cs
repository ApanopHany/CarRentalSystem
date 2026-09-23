using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    internal class Customer : User, IDisplay
    {
        public string Id { get; private set; }
        public DateTime JoinDate { get; set; }
        public string Email { get; set; }
        public static int BranchCount = 0;
        static int counter = 0;
        public Customer(string name, string phone, DateTime dateTime, string email) : base(name, phone)
        {
            Id = $"CUST-{(++counter).ToString("D3")}";
            JoinDate = dateTime;
            BranchCount++;
            if (email != null && email.Contains("@") && email.Contains("."))
                Email = email;
            else
            {
                Console.WriteLine("Invalid email format. Must contain '@' and '.'. ");
                Email = "N/A";
            }
        }
        static public void Register(string name, string phone, string Email, List<Customer> listCus)
        {
            bool Checkphone = false;
            if (Email != null && Email.Contains("@") && Email.Contains("."))
            {
                if (phone != null)
                {
                    foreach (var item in phone)
                    {
                        if (char.IsDigit(item))
                        {
                            Checkphone = true;
                            break;
                        }
                    }
                }
                if (Checkphone == true)
                {
                    Customer news = new Customer(name, phone, DateTime.Now, Email);
                    listCus.Add(news);
                    try
                    {
                        using (var connection = Database.GetConnection())
                        {
                            connection.Open();
                            string query = "INSERT INTO Customer (Id, NameCus, Phone, JoinDate, Email) VALUES (@Id, @Name, @Phone, @JoinDate, @Email)";

                            using (var command = new Microsoft.Data.SqlClient.SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Id", news.Id);
                                command.Parameters.AddWithValue("@Name", news.Name);
                                command.Parameters.AddWithValue("@Phone", news.Phone);
                                command.Parameters.AddWithValue("@JoinDate", news.JoinDate);
                                command.Parameters.AddWithValue("@Email", news.Email);

                                command.ExecuteNonQuery();
                            }
                        }
                        Console.WriteLine($"Customer: {name} - [{news.Id}] registered and saved to Database successfully! ");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("System ERROR ! " + ex.Message);
                    }
                }
                else Console.WriteLine("Phone number must contain at least one digit.");
            }
            else Console.WriteLine("Invalid email format. Must contain '@' and '.'. ");
        }
        public void print()
        {
            Console.WriteLine($"Id : {Id} | Name : {Name} | Joined {JoinDate:dd / MM / yyyy}");
            Console.WriteLine($"Phone : {Phone} | Email : {Email} ");
        }
    }
}
