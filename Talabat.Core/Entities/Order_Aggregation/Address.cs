using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregation
{
    // this addres that written in order (not in database (no table) )

    // 1 to 1 total 
    public class Address
    {
        public string FName { get; set; }

        public string LName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Address()
        {
                
        }

        public Address(string fName, string lName, string street, string city, string country)
        {
            FName = fName;
            LName = lName;
            Street = street;
            City = city;
            Country = country;
        }
    }
}
