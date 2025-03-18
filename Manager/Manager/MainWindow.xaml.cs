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

        public MainWindow()
        {
            InitializeComponent();

            LoadWelcome();



        }

        private void LoadWelcome()
        {
            welcome = new Welcome();
            mainMenu = new MainMenu();

          
            Grid.SetColumn(mainMenu, 0); 
            MainGrid.Children.Add(mainMenu);
        
            Grid.SetColumn(welcome, 1); 
            MainGrid.Children.Add(welcome);

            // Subscribe to the toggle event
            mainMenu.ToggleMenu += MainMenuToggle;
            mainMenu.CloseProgram += CloseProgram;
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