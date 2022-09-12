using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment5._2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Contact> contacts = new ObservableCollection<Contact>();
        public MainWindow()
        {
            InitializeComponent();
            ContactList.ItemsSource = contacts;

            CollectionView search = (CollectionView)CollectionViewSource.GetDefaultView(ContactList.ItemsSource);
            search.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(Search.Text))
            {
                return true;
            }
            else
            {
                return ((item as Contact).FirstName.IndexOf(Search.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        public class Contact
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            contacts.Add(new Contact() { FirstName = FirstName.Text, LastName = LastName.Text, Phone = Phone.Text, Address = Address.Text });           
            ContactList.ItemsSource = contacts;
            CollectionViewSource.GetDefaultView(ContactList.ItemsSource).Refresh();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            contacts.Remove(ContactList.SelectedItem as Contact);
            ContactList.ItemsSource = null;
            ContactList.ItemsSource = contacts;
            ContactList.Items.Refresh();

        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ContactList.ItemsSource).Refresh();
        }        
    }
}