using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAddressBook.Model
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public DateTime BirthDate { get; set; }
        public string Notes { get; set; }

    }
}
