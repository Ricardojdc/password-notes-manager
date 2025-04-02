using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        private ObservableCollection<Password> _passwordFile;
        private string _path;
       
        public PasswordPage()
        {
            InitializeComponent();
            LatestFileCleanUp();
            EnableEntriesMenu(false);
            EnableFileMenuItems(false);
          
        }

        private void ClosePasswordManager(object sender, RoutedEventArgs e)
        {
                RedirectWelcome?.Invoke();  
            
        }
        private void LatestFileCleanUp()
        {
            PasswordGrid.Columns.Clear();
            PasswordGrid.Visibility = Visibility.Collapsed;
            StatusText.Content = string.Empty;  
        }

        private void ColumnGeneration()
        {
        
            Type passType = typeof(Password);
            PropertyInfo[] properties = passType.GetProperties();

            PasswordGrid.Columns.Clear();

            foreach (PropertyInfo property in properties)
            {
 
                DataGridTextColumn newColumn = new DataGridTextColumn();

                newColumn.Header = property.Name;

                newColumn.Binding = new Binding(property.Name);

                PasswordGrid.Columns.Add(newColumn);
            }

            DataGridTextColumn actions = new DataGridTextColumn();

            actions.Header = "Action";

            PasswordGrid.Columns.Add(actions);

        }

        private void LoadData(string fileContent)
        {
            var passwords = Utils.Utils.ReadFromJSON(fileContent);

            if (passwords != null)
            {
                _passwordFile = new ObservableCollection<Password>(passwords);
                PasswordGrid.Visibility = Visibility.Visible;
                //PasswordGrid.PreviewMouseDown += (s, e) => e.Handled = true; // Makes the rows unclickable
                ColumnGeneration();
                PasswordGrid.AutoGenerateColumns = false;
                PasswordGrid.ItemsSource = _passwordFile;
               
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
        private async void NewPasswordEntry(object sender, RoutedEventArgs e)
        {
            NewEntryWindow newEntryWindow = new(_passwordFile,_path);

            var result = newEntryWindow.ShowDialog();

            if (result == false)
            {
                StatusText.Content = "New Entry canceled";
            }
            else
            {             
                StatusText.Content = "Entry created correctly";
               // PasswordGrid.ItemsSource = null; //Necesary if using a simple List instead of an ObservableCollection
                PasswordGrid.ItemsSource = _passwordFile;
 
            }

        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            LatestFileCleanUp();
            var fileContent = Utils.Utils.OpenFile();
           
            if (fileContent != null)
            {
                LoadData(fileContent);
                _path = fileContent;
                EnableEntriesMenu(true);
                EnableFileMenuItems(true);
                StatusText.Content = fileContent.ToString();
            }
           
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

        private void EnableFileMenuItems(bool status)
        {
            if (status)
            {
                Save.IsEnabled = true;
                Close_File.IsEnabled = true;   
            }
            else
            {
                Save.IsEnabled = false;
                Close_File.IsEnabled = false;

            }
        }

        private void CreateNewFile(object sender, RoutedEventArgs e)
        {
            var result = Utils.Utils.CreateNewFile();
            if (result != null)
            {
                LoadData(result?.Item2);
                _path = result?.Item2;
                StatusText.Content ="File created at: " + result?.Item2;
                EnableEntriesMenu(true);
                EnableFileMenuItems(true);
            }
        }

        private void CloseFile(object sender, RoutedEventArgs e)
        {
            LatestFileCleanUp();
            EnableEntriesMenu(false);
            EnableFileMenuItems(false);
        }
    }
}
