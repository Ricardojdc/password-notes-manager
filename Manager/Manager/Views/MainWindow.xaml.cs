using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using Manager.Views;


namespace Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Welcome welcome;
        private MainMenu mainMenu;
        private PasswordPage passwordPage;
 
        public MainWindow()
        {
          
            InitializeComponent();
            LoadWelcome();

        }

        private void LoadWelcome()
        {
            mainMenu = new MainMenu();
            welcome = new Welcome();
 
            Grid.SetColumn(mainMenu, 0); 
            MainGrid.Children.Add(mainMenu);
        
            Grid.SetColumn(welcome, 2); 
            MainGrid.Children.Add(welcome);

            // Subscribe to the main menu events
            mainMenu.ToggleMenu += MainMenuToggle;
            mainMenu.CloseProgram += CloseProgram;
            mainMenu.WelcomePage += WelcomePageRedirect;
            mainMenu.PasswordPage += PasswordPageRedirect;
            
        }

        private void WelcomePageRedirect()
        {
            MainGrid.Children.Remove(welcome);
            welcome = new Welcome();
            Grid.SetColumn(welcome, 2);
            MainGrid.Children.Add(welcome);
          
        }

        private void PasswordPageRedirect()
        {
            MainGrid.Children.Remove(passwordPage);
            passwordPage = new PasswordPage();
            Grid.SetColumn(passwordPage, 2);
            MainGrid.Children.Add(passwordPage);
            passwordPage.RedirectWelcome += WelcomePageRedirect;
        }

        private void MainMenuToggle()
        {
            
            ColumnDefinition menuColumn = MainGrid.ColumnDefinitions[0]; 

            if (menuColumn.Width.Value == 150) 
            {
                
                menuColumn.Width = new GridLength(30);
               

                foreach(UIElement el in mainMenu.MainOperations.Children)
                {
                   
                    if(el is Button button)
                    {
                        button.Visibility = Visibility.Collapsed;
                        button.IsEnabled = false;
                    }

                }

                foreach(UIElement el in mainMenu.BottomOperations.Children)
                {

                    if(el is Button button)
                    {
                        if (!button.Name.Equals("Resize"))
                        {
                            button.Visibility = Visibility.Collapsed;
                            button.IsEnabled = false;

                        }
                    }
                    else if (el is Separator s)
                    {
                        s.Visibility = Visibility.Collapsed;
                    }
                }

                
            }
            else
            {
              
                menuColumn.Width = new GridLength(150);

                foreach (UIElement el in mainMenu.MainOperations.Children)
                {

                    if (el is Button button)
                    {
                        button.Visibility = Visibility.Visible;
                        button.IsEnabled = true;
                    }

                }

                foreach (UIElement el in mainMenu.BottomOperations.Children)
                {

                    if (el is Button button)
                    {
                        if (!button.Name.Equals("Resize"))
                        {
                            button.Visibility = Visibility.Visible;
                            button.IsEnabled = true;

                        }
                        
                    }
                    else if (el is Separator s)
                    {
                        s.Visibility = Visibility.Visible;
                    }
                }

            }
        }

        private void CloseProgram()
        {

            this.Close();
        }

    }

}