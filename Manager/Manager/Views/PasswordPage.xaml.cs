using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
using Manager.Models;
using Manager.Utils;
using Manager.Views.PasswordPageWindows;

namespace Manager.Views
{
    /// <summary>
    /// Interaction logic for PasswordPage.xaml
    /// </summary>
    public partial class PasswordPage : UserControl
    {
        public event Action RedirectWelcome;
        List<Test> t;
        Password p;
        public PasswordPage()
        {
            InitializeComponent();
            LatestFileCleanUp();
           // EnableEntriesMenu(false);
          
        }

        private void ClosePasswordMnager(object sender, RoutedEventArgs e)
        {

            RedirectWelcome?.Invoke();
        }
        private void LatestFileCleanUp()
        {
            PasswordGrid.Columns.Clear();
            PasswordGrid.Visibility = Visibility.Collapsed;
        }

        private void ColumnGeneration()
        {
            PasswordGrid.AutoGenerateColumns = false;
            var name = new DataGridTextColumn();
            name.Header = "Name";
            name.Binding = new Binding("Name");
            name.IsReadOnly = true;

            PasswordGrid.Columns.Add(name);

            var age = new DataGridTextColumn();
            age.Header = "Age";
            age.Binding = new Binding("Age");
            age.IsReadOnly = false;

            PasswordGrid.Columns.Add(age);


        }

        private void ColumnOrder()
        {

            PasswordGrid.Columns[1].DisplayIndex = 2;
        }

        private void LoadData(string fileContent)
        {

            p = new Password();
            //var fileContent = p.ReadFromFile(Utils.Utils.OpenFile());
            var content = p.ReadFromJSON(fileContent);

            if (content != null)
            {
                PasswordGrid.Visibility = Visibility.Visible;
                PasswordGrid.PreviewMouseDown += (s, e) => e.Handled = true; // Makes the rows unclickable
                PasswordGrid.ItemsSource = content;

            }
            else
            {
               
                PasswordGrid.Visibility = Visibility.Collapsed;
            }

        }

        /// <summary>
        /// Event in the File menu for a new password entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPasswordEntry(object sender, RoutedEventArgs e)
        {
            NewEntryWindow newEntryWindow = new();

            var result = newEntryWindow.ShowDialog();

            if (result == false)
            {
                StatusText.Content = "New Entry canceled";
            }
            else
            {
                StatusText.Content = "Entry created correctly";
                
            }

        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            LatestFileCleanUp();
            var fileContent = Utils.Utils.OpenFile();

            //if (fileContent != null) EnableEntriesMenu(true);

            if (fileContent != null)
            {
                LoadData(fileContent);
                StatusText.Content = fileContent.ToString();
            }
            //  ColumnOrder();
        }

        /// <summary>
        /// Enable
        /// </summary>
        /// <param name="status"></param>
        private void EnableEntriesMenu(bool status)
        {
            if (status)
            {
                Entries.IsEnabled = true;
            }
            else
            {
                Entries.IsEnabled = false;
            }
        }      
    
       

        private void CreateNewFile(object sender, RoutedEventArgs e)
        {
            var result = Utils.Utils.CreateNewFile();
            if (result != null)
            {
                LoadData(result?.Item2);
                StatusText.Content = result?.Item2;
            }
        }

     
    }
}
