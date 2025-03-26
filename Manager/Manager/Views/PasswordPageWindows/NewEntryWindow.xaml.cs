using System;
using System.Collections.Generic;
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
        }

        public NewEntryWindow(List<Password> list) : this()
        {
           
            this._list = list;
        }

        public NewEntryWindow(List<Password> list, string id): this(list)
        {
            
            this._id = id;
        }

      
    }
}
