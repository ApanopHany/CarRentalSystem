using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    internal class User
    {
        public User(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }
        string phone;
        public string Name { get; set; }
        public string Phone
        {
            get => phone;
            set { if (value.Length == 11) { phone = value; } else { phone = "N/A"; } }
        }
    }

}
