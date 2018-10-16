using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U22
{
    class Branch
    {
        const int MaxNumberOfFridges = 500;                 //Maximum number of heroes
        public string ShopsName { get; set; }                  //Assign heroes race
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public FridgeContainer fridges { get; private set; } //Heroes container for specific race
        public Branch(string shopsName, string address, string phoneNumber)

        {
            ShopsName = shopsName;
            Address = address;
            PhoneNumber = phoneNumber;
            fridges = new FridgeContainer(MaxNumberOfFridges);
        }
    }
}
