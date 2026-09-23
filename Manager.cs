using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    internal class Manager : User, IDisplay
    {
        public decimal Salary { get; set; }
        public DateTime Hired { get; set; }
        public string Id { get; private set; }
        static int count = 0;

        public Manager(string name, string phone, decimal salary, DateTime hired) : base(name, phone)
        {
            Id = $"MGR-{(++count).ToString("D2")}";
            Salary = salary;
            Hired = hired;
        }
        public void print()
        {
            Console.WriteLine($"Id : {Id} | Name : {Name} | Phone : {Phone}");
            Console.WriteLine($"Salary : {Salary} | Hired : {Hired:dd/MM/yyyy} ");
        }
    }
}
