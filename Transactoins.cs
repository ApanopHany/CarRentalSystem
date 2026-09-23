using Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace SystemOfCars 
{
    internal class Transactoins : IDisplay
    {
        public int Id {  get; set; }
        public string Returned {  get; private set; }
        public string Status { get; private set; }
        static int count = 1000;
        public string MODEL {  get; private set; }
        public string CUSID {  get; private set; }
        public string CARID {  get; private set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int deff {  get; private set; }
        public string Fee {  get; private set; }

        public Transactoins(string cusid,string carid,List<Customer>listCus,
            List<Cars> listCar, List<Transactoins> listTrans)
        {
            Id = ++count;
            CUSID= cusid;
            CARID= carid;
        }
        public void OpenTransaction( List<Customer> listCus, 
            List<Cars> listCar,List<Transactoins>listTrans,DateTime date)
        {
            foreach (var item in listCar)
            {
                if (CARID == item.Id)
                {
                    deff = 0;
                    Fee = "None";
                    From = date;
                    Returned = "Not returned yet";
                    Status = "Active";
                    MODEL = item.Model;
                    To = date.AddDays(14);
                    break;
                }
            }
        }
        public void CloseTransaction(List<Cars> listCar)
        {
            foreach (var item in listCar)
            {
                if (CARID == item.Id)
                {
                    deff = (DateTime.Now - To).Days;
                    Status = "Returned ";
                    Returned = $"{DateTime.Now:dd/MM/yyyy}";
                    if (deff <= 0)
                    {
                        deff = 0;
                        Fee = "None ";
                    }
                    if (deff > 0)
                    {
                        Fee = $"({(deff)*150:F2}) ُEGP";
                    }
                    break;
                }
            }
        }
        public void print() 
            {
            Console.WriteLine($"--- Transaction #{Id} ------------------ ");
            Console.WriteLine($"Car        : {MODEL}");
            Console.WriteLine($"Car ID     : {CARID} ");
            Console.WriteLine($"Rented     : {From:dd/MM/yyyy} ");
            Console.WriteLine($"Due        : {To:dd/MM/yyyy}");
            Console.WriteLine($"Returned   : {Returned}");
            Console.WriteLine($"Status     : {Status} ");
            Console.WriteLine($"Fee        : {Fee}");
        }
    }
}
