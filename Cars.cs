using System.Transactions;
using SystemOfCars;

namespace Project
{
    internal class Cars : IDisplay
    {
        public string Id { get; private set; }
        static int count = 0;
        public Cars (string model, string condition)
        {
            Id = $"CAR-{(++count).ToString("D3")}";
            Model = model;
            Condition = condition;
            BranchCount++;
            IsAvailable();
        }
        public DateTime DueTo { get; set; }
        public string Available { get; private set; }
        public string Model { get; private set; }
        public string Condition { get; private set; }
        public static int BranchCount = 0;
        public void IsAvailable()
        {
            Available = "Available";
        }
        public void IsNotAvailable()
        {
            Available = "Rented";
        }
        public void IsMaintenance()
        {
            Available = "Maintenance";
        }
        public void print()
        {
            Console.WriteLine($"Car [{Id}] - {Model} | Condition : {Condition} | {Available}");
        }
    }
}
