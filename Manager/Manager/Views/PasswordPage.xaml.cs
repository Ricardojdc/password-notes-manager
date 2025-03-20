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

namespace Manager.Views
{
    /// <summary>
    /// Interaction logic for PasswordPage.xaml
    /// </summary>
    public partial class PasswordPage : UserControl
    {
        List<Test> t;
        public PasswordPage()
        {
            InitializeComponent();
            LoadData();
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

        private void LoadData()
        {

            ColumnGeneration();
            
            t = Test.GeneratePeople(150);
           
            var namesOnly = t.Select(person => new Test {Name = person.Name , Age = person.Age, City = person.City }).ToList();
           // var nameColumn = PasswordGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "Name");
            string nameCol = null;

            foreach(var col in PasswordGrid.Columns)
            {

                if(col.Header == "Name")
                {
                    col.IsReadOnly = true;
                    break;
                }

            }





            //if (nameColumn != null)
            //{
            //    nameColumn.IsReadOnly = true;
            //}

            PasswordGrid.ItemsSource = namesOnly;

        }

     
    }
}
