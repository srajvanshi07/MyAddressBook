using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAddressBook.Model
{
    public class AutoSuggestContact
    {
        public Contact RealContact { get; set; }
        public override string ToString()
        {
            //return base.ToString();
            return RealContact.FirstName + " " + RealContact.LastName;
        }



    }
}
