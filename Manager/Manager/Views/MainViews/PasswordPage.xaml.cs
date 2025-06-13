
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Manager.Attributes;
using Manager.Models;
using Manager.Views.PasswordPageWindows;
using Newtonsoft.Json;

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
        private Password _password;
        
       
        public PasswordPage()
        {
            InitializeComponent();
            LatestFileCleanUp();
            EnableEntriesMenuItems(false);
            EnableFileMenuItems(false);
            EnableActiosnGrid(false);
            EnableSearchBar(false);
            //TimeOut();
          
        }

        private async void TimeOut()
        {        
            await Task.Delay(360000);
 
            RedirectWelcome?.Invoke();
            MessageBox.Show("Timed out", "Time out");

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

        /// <summary>
        /// Generates the column header based on the object Password, excluding confidential properties
        /// </summary>
        private void ColumnGeneration()
        {
        
            Type passType = typeof(Password);
            PropertyInfo[] properties = passType.GetProperties();

            PasswordGrid.Columns.Clear();

            foreach (PropertyInfo property in properties)
            {
 
                DataGridTextColumn newColumn = new DataGridTextColumn();

                newColumn.Header = property.Name;
                
                if (property.GetCustomAttribute(typeof(Confidential)) == null)
                {
                    property.GetCustomAttributes();
                    newColumn.Binding = new Binding(property.Name);
                    PasswordGrid.Columns.Add(newColumn);
                   
                }
            }
        }

        /// <summary>
        /// Loads the data from a JSON file viapath
        /// </summary>
        /// <param name="path">Path of the file</param>
        private void LoadData(string path)
        {
            var passwords = Utils.Utils.ReadFromJSON(path);

            if (passwords != null)
            {
                _passwordFile = new ObservableCollection<Password>(passwords);
                PasswordGrid.Visibility = Visibility.Visible;
                //PasswordGrid.PreviewMouseDown += (s, e) => e.Handled = true; // Makes the rows unclickable
                ColumnGeneration();
                PasswordGrid.AutoGenerateColumns = false; // Disable autogeneration of columns from the list
                PasswordGrid.SelectionMode = DataGridSelectionMode.Single; // Only one row selected at a time
                PasswordGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
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
                SecondaryStatusBarItem.Content = "New Entry canceled";
            }
            else
            {             
                SecondaryStatusBarItem.Content = "Entry created correctly";
               // PasswordGrid.ItemsSource = null; //Necesary if using a simple List instead of an ObservableCollection
                PasswordGrid.ItemsSource = _passwordFile;

            }

        }

        /// <summary>
        /// Enable or disable the entries menu
        /// </summary>
        /// <param name="status"></param>
        private void EnableEntriesMenuItems(bool status)
        {
            if (status)
            {
                New_Entry.IsEnabled = true;
            }
            else
            {
               New_Entry.IsEnabled = false;
            }
        }

        private void EnableFileMenuItems(bool status)
        {
            if (status)
            {
              
                Save.IsEnabled = true;
                SaveAsMenuItem.IsEnabled = true;
                Close_File.IsEnabled = true;   
            }
            else
            {
                Save.IsEnabled = false;
                SaveAsMenuItem.IsEnabled = false;
                Close_File.IsEnabled = false;

            }
        }

        private void EnableActiosnGrid(bool status)
        {
            if (status)
            {
                ActionsGrid.Visibility = Visibility.Visible;   
            }
            else
            {
                ActionsGrid.Visibility = Visibility.Hidden;
            }
        }

        private void EnableSearchBar(bool status) 
        {
            if (status)
            {
                SearchLbl1.Visibility = Visibility.Visible;
                SearchTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                SearchLbl1.Visibility = Visibility.Hidden;
                SearchTextBox.Visibility = Visibility.Hidden;
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
                EnableEntriesMenuItems(true);
                EnableFileMenuItems(true);
            }
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {

            var fileContent = Utils.Utils.OpenFile();

            if (fileContent != null)
            {
                LoadData(fileContent);
                _path = fileContent;
                EnableEntriesMenuItems(true);
                EnableFileMenuItems(true);
                EnableSearchBar(true);
                StatusText.Content = fileContent.ToString();
            }

        }

        private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseFile(object sender, RoutedEventArgs e)
        {
            LatestFileCleanUp();
            EnableEntriesMenuItems(false);
            EnableFileMenuItems(false);
            EnableActiosnGrid(false);
            EnableSearchBar(false);
        }

        /// <summary>
        /// When a row is selected in the password grid, another grid becomes visible and
        /// displays a series of buttons and information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PasswordGrid.SelectedItem is Password selectedPassword)
            {            
                EnableActiosnGrid(true);
                _password = selectedPassword;
                
                ActionGridLabel.Content = selectedPassword.Site;
                
            }
            
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            _passwordFile.Remove(_password);

            if (File.Exists(_path))
            {
                string json = JsonConvert.SerializeObject(_passwordFile, Formatting.Indented);

                File.WriteAllText(this._path, json);

              
            }
            else
            {
                MessageBox.Show($"Error opening file at: {_path}");
               
            }
            PasswordGrid.ItemsSource = _passwordFile;         
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            NewEntryWindow edit = new NewEntryWindow(_passwordFile,_path,_password);

            var result = edit.ShowDialog();

            if (result == false)
            {
                SecondaryStatusBarItem.Content = "Edit canceled";
            }
            else
            {
                SecondaryStatusBarItem.Content = "Edited correctly";
                // PasswordGrid.ItemsSource = null; //Necesary if using a simple List instead of an ObservableCollection
                PasswordGrid.ItemsSource = _passwordFile;

            }
        }

     
    }
}
