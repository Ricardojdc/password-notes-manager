using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Manager.Models;
using Newtonsoft.Json;

namespace Manager.Views.PasswordPageWindows
{
    /// <summary>
    /// Interaction logic for NewEntryWindow.xaml
    /// </summary>
    public partial class NewEntryWindow : Window
    {
        private ObservableCollection<Password> _list;
        private Password _password = null;
        private string _path;
        public NewEntryWindow()
        {
            InitializeComponent();
            SaveEntryBtn.IsEnabled = false;       
        }

        public NewEntryWindow(ObservableCollection<Password> list,string path) : this()
        {    
            _list = list;
            _path = path;           
        }

        public NewEntryWindow(ObservableCollection<Password> list,string path, Password p): this(list,path)
        {
            _password = p;

            SiteTextBox.Text = p.Site;
            LoginTextBox.Text = p.Login;
            PasswordBox.Password = p.Pass;
            VisiblePasswordBox.Text = p.Pass;
            NotesTextBox.Text = p.Notes;

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
