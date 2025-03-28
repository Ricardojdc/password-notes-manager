using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace Manager.Views.PasswordPageWindows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NewEntryWindow : Window
    {
        private List<Password> _list;
        private string _id = null;
        public NewEntryWindow()
        {
            InitializeComponent();
            SaveEntryBtn.IsEnabled = false;
        }

        public NewEntryWindow(List<Password> list) : this()
        {
           
            this._list = list;
        }

        public NewEntryWindow(List<Password> list, string id): this(list)
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
        }

        private void PasseordBox_TextChanged(object sender, RoutedEventArgs e)
        {
            ValidateForm();
        }

        private void CloseNewEntry(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }

        private void SaveNewEntry(object sender, RoutedEventArgs e)
        {
            //Password NewPassword = { }

            this.DialogResult = true;
           
        }
    }
}
