using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace Manager.Utils
{
    internal static class Utils
    {

        public static string OpenFile()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.txt)|*.*"; // Set file types
            openFileDialog.Multiselect = false; // Allow only one file

            if (openFileDialog.ShowDialog() == true)
            {
               
                return openFileDialog.FileName;
               
            }
            return null;
        }

    }
}
