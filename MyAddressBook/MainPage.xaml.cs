using MyAddressBook.Model;
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
using System.Threading.Tasks;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyAddressBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DataAccess dataAccess = new DataAccess();
        private List<AutoSuggestContact> contactsForAutosuggest;
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = dataAccess.GetAllContacts();
            contactsForAutosuggest = new List<AutoSuggestContact>();
            var contacts = this.DataContext as List<Contact>;
            foreach(var contact in contacts)
            {
                var autosuggestContact = new AutoSuggestContact();
                autosuggestContact.RealContact = contact;
                contactsForAutosuggest.Add(autosuggestContact);
            }
            txtSearchSuggest.ItemsSource = contactsForAutosuggest;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddOrUpdateAContact));
        }
        private void IconsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ContactCollectionView!= null)
            {
                if (HomeListBoxItem.IsSelected)
                {
                    this.DataContext = dataAccess.GetAllContacts();
                }
                else
                {
                    this.Frame.Navigate(typeof(AddOrUpdateAContact));
                }
            }
        }

        //Findcontact declace
        private List<AutoSuggestContact> FindContact(string search_term)
        {
            return contactsForAutosuggest.Where(i => i.RealContact.FirstName.IndexOf(search_term, StringComparison.OrdinalIgnoreCase)
            > -1 || i.RealContact.LastName.IndexOf(search_term, StringComparison.OrdinalIgnoreCase) > -1).ToList();

        }
        private void txtSearchSuggest_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.CheckCurrent() && args.Reason== AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var search_term = txtSearchSuggest.Text;
                var results = FindContact(search_term);
                txtSearchSuggest.ItemsSource = results;
            }
        }
        private void txtSearchSuggest_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var autosuggestContact = args.SelectedItem as AutoSuggestContact;
            var parameter = new Tuple<Contact, DataAccess>(autosuggestContact.RealContact, this.dataAccess);
            this.Frame.Navigate(typeof(AddOrUpdateAContact), parameter);
        }

        private void txtSearchSuggest_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var search_term = args.QueryText;
            var results = FindContact(search_term);
            txtSearchSuggest.ItemsSource = results;
            txtSearchSuggest.IsSuggestionListOpen = true;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
