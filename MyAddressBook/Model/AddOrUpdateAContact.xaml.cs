using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MyAddressBook.Model

{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddOrUpdateAContact : Page
    {
        //to use filepath
        const string filePath = "AddressBook.json";
        //to use filehelper
        FileHelper fileHelper = new FileHelper();
        public Contact contact { get; set; }
        private bool isNewContact;
        private DataAccess dataAccess;
        //use usercontent
        string userContent;
        public AddOrUpdateAContact()
       
        {
            contact = new Contact();
            isNewContact = true;
            this.InitializeComponent();
        }
//go back to main page after cancel
        private void backbt_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        //save new contact
        private void savebt_Click(object sender, RoutedEventArgs e)
        {
            contact.FirstName = FName.Text;
            contact.LastName = LName.Text;

            contact.Address = Address.Text;

            contact.EmailAddress = Email.Text;

            contact.PhoneNo = PhoneNumber.Text;
            if (isNewContact)
            {
                dataAccess = new DataAccess();
                dataAccess.CreateContact(contact);
            }
            else
            {
                dataAccess.SaveContacts();
            }
            //after saving go back to main page
            this.Frame.Navigate(typeof(MainPage));
        }

            protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);
            var parameter = e.Parameter as Tuple<Contact, DataAccess>;
            if (parameter != null)
            {
                this.contact = parameter.Item1;
                this.dataAccess = parameter.Item2;
            }
            isNewContact = parameter == null;
        }      
        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            userContent = await fileHelper.ReadTextFile(filePath);
            contact.FirstName = FName.Text;
            contact.LastName = LName.Text;
            contact.Address = Address.Text;
            contact.EmailAddress = Email.Text;
            contact.PhoneNo = PhoneNumber.Text;

            string fname = FName.Text;
            string lname = LName.Text;
            string address = Address.Text;
            string email = Email.Text;
            string phone = PhoneNumber.Text;

            dataAccess.update(userContent, fname, lname, address, email, phone);

        }
    }
    
}
