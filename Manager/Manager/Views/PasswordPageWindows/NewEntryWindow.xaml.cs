using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Views.PasswordPageWindows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NewEntryWindow : Window
    {
        private ObservableCollection<Password> _list;
        private string _id = null;
        private string _path;
        public NewEntryWindow()
        {
            InitializeComponent();
            SaveEntryBtn.IsEnabled = false;
           
        }

        public NewEntryWindow(ObservableCollection<Password> list,string path) : this()
        {
           
            this._list = list;
            this._path = path;
           
        }

        public NewEntryWindow(ObservableCollection<Password> list,string path, string id): this(list,path)
        {
            
            this._id = id;
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            VisiblePasswordBox.Text = PasswordBox.Password;
            VisiblePasswordBox.Visibility = Visibility.Visible;
            PasswordBox.Visibility = Visibility.Collapsed;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = VisiblePasswordBox.Text;
            PasswordBox.Visibility = Visibility.Visible;
            VisiblePasswordBox.Visibility = Visibility.Collapsed;
        }

        private void ValidateForm()
        {
            bool valid = !string.IsNullOrEmpty(SiteTextBox.Text) && !string.IsNullOrEmpty(LoginTextBox.Text) && !string.IsNullOrEmpty(PasswordBox.Password);

            SaveEntryBtn.IsEnabled = valid;
        }

        private void TextBox_TextChanged(object sender,TextChangedEventArgs e)
        {
            ValidateForm();
            PasswordBox.Password = VisiblePasswordBox.Text;
        }

        private void PasswordBox_TextChanged(object sender, RoutedEventArgs e)
        {
            ValidateForm();
            VisiblePasswordBox.Text = PasswordBox.Password;
        }

        private void CloseNewEntry(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }

        private void SaveNewEntry(object sender, RoutedEventArgs e)
        {

            try
            {

                int id = _list.Max(p => p.Id) + 1;

                Password NewPassword = new Password
                {
                    Id = id,
                    Login = LoginTextBox.Text,
                    Pass = PasswordBox.Password,
                    Site = SiteTextBox.Text,
                    Notes = NotesTextBox.Text
                };

                this._list.Add(NewPassword);

                if (File.Exists(_path))
                {                    

                    string json = JsonConvert.SerializeObject(_list,Formatting.Indented);

                    File.WriteAllText(this._path, json);

                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show($"Error opening file at: {_path}");
                    this.DialogResult= false;
                }

                    
            }
            catch (Exception)
            {

            }
        }

     
        private void CopyToClipboard(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(PasswordBox.Password);
        }
    }
}
