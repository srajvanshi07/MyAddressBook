using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using Windows.Storage.Streams;


namespace MyAddressBook.Model
{
    public class DataAccess
    {
        FileHelper fileHelper = new FileHelper();
        //Generating filePath
        public string filepath = ApplicationData.Current.LocalFolder.Path + "\\AddressBook.json";

        //generating new collection "contacts for list
        public List<Contact> Contacts;
        //constructor
        public DataAccess()
        {
            if (File.Exists(filepath))
            {
                JsonSerializer serializer = new JsonSerializer();
                //Read a file
                using (StreamReader file = new StreamReader(File.OpenRead(filepath)))
                {
                    Contacts = serializer.Deserialize(file, typeof(List<Contact>)) as List<Contact>;
                }
            }
            else
            {
                Contacts = new List<Contact>();
            }
        }
        /// After creating a new contact this method will add it to contacts collection and write it json file.
        public void CreateContact(Contact c)
        {
            Contacts.Add(c);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            //Write to json file
            using (StreamWriter file = File.CreateText(filepath))
            {
                serializer.Serialize(file, Contacts);
            }
        }
        /// this method will get all contacts fron json file
        public List<Contact> GetAllContacts()

        {
            return Contacts;
        }
        ///This method returns contacts by name
        public List<Contact> SearchAllContacts(string searchQuery)
        {
            return Contacts
                        .Where(item => (item.FirstName.ToLower().Contains(searchQuery.ToLower()) == true)).ToList();

        }
        /// this method will delete a contact from the contact list
        public void DeleteContact(Contact c)

        {
            Contacts.Remove(c);
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter file = File.CreateText(filepath))
            {
                serializer.Serialize(file, Contacts);
            }
        }
        public void SaveContacts()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            //Write to json file
            using (StreamWriter file = File.CreateText(filepath))
            {
                serializer.Serialize(file, Contacts);
            }
        }
        public async void update(string userContent, string fname, string lname, string address, string email, string phone)
        {
            List<Contact> myList = JsonConvert.DeserializeObject<List<Contact>>(userContent);
            // string new1= "new";
            if (myList == null)
                myList = new List<Contact>();

            foreach (var con in myList)
            {
                string uName = con.FirstName;
                string uLast = con.LastName;
                string uAddress = con.Address;
                string uemail = con.EmailAddress;
                string uphone = con.PhoneNo;
                if (uName == fname && uLast == lname)
                {
                    if (address != null)
                    {
                        con.Address = address;
                    }
                }
                if (email != null)
                {
                    con.EmailAddress = email;
                }
                if (phone != null)
                {
                    con.PhoneNo = phone;
                }



            }
            string data = JsonConvert.SerializeObject(myList);
            await fileHelper.WriteTextFile("AddressBook.json", data);
        }
    }
}
