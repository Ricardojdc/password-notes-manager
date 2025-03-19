using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Manager.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {

        public event Action ToggleMenu;
        public event Action CloseProgram;
        public event Action WelcomePage;
        public event Action PasswordPage;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void ResizeMenu(object sender, RoutedEventArgs e)
        {
            ToggleMenu?.Invoke();
        }

        private void ExitProgram(object sender, RoutedEventArgs e)
        {

           CloseProgram?.Invoke();
          
        }

        private void MainPageRedirect(object sender, RoutedEventArgs e)
        {
            WelcomePage?.Invoke();
        }

        private void PasswordManagerRedirect(object sender, RoutedEventArgs e)
        {
            PasswordPage?.Invoke();
        }
    }
}
