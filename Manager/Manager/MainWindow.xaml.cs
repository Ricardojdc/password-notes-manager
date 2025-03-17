using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Manager.Views;

namespace Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Welcome welcome;
        List<User> users = new List<User>();
        public MainWindow()
        {
            InitializeComponent();

            
            users.Add(new User() { Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23) });
            users.Add(new User() { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17) });
            users.Add(new User() { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2) });

            LoadWelcome();
       
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {


            LoadWelcome();
           
        }

        private void LoadWelcome()
        {
            ContentArea.Content = null;

            // Create a new instance of HomeControl
            welcome = new Welcome();

            // Set it as the content of the ContentControl in column 1
            ContentArea.Content = welcome;
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            // Clear existing content
            ContentArea.Content = null;

            // Create and load a new instance of SettingsControl UserControl
           
        }

        private void About(object sender, RoutedEventArgs e)
        {

            welcome.TextBlockWelcome.Text = "asdasdas";

        }
    }
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }
    }
}