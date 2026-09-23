namespace Project
{
    internal class Branch : IDisplay
    {
        public string Id { get; private set; }
        static int count = 0;
        public string Name { get; set; }
        public string Hours {  get; set; }
        public string Address { get; set; }
        public string Manager { get; set; }

        public Branch(string name, string address, string phone, string hours, string manager)
        {
            Id = $"BR-{(++count).ToString("D2")}";
            Name = name;
            Address = address;
            Phone = phone;
            Manager = manager;
            Hours = hours;
        }
        string phone;
        public string Phone
        {
            get => phone;
            set { if (value !=null && value.Length == 11) {  phone = value; } else { phone = "N/A"; }  }
        }
        
        public void print()
        {
            Console.WriteLine("Rental Branch Info");
            Console.WriteLine("----------------------------------------------------");

            Console.WriteLine($"ID                        : {Id}");
            Console.WriteLine($"Name                      : {Name}");
            Console.WriteLine($"Address                   : {Address}");
            Console.WriteLine($"Phone                     : {Phone}");
            Console.WriteLine($"Opening Hours             : {Hours}");
            Console.WriteLine($"Manager                   : {Manager}");
            Console.WriteLine($"Total Customers           : {Customer.BranchCount}");
            Console.WriteLine($"Total Cars                : {Cars.BranchCount}");

        }
    }
}
