using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            openFileDialog.Filter = "JSON files (*.json)|*.json|Text files (*.txt)|*.txt";
            openFileDialog.Multiselect = false; // Allow only one file

            if (openFileDialog.ShowDialog() == true)
            {

                return openFileDialog.FileName;

            }
            return null;
        }
       /// <summary>
       /// Creates a new file of the specified type
       /// </summary>
       /// <returns>
       /// - The first item is the name of the file plus extension type
       /// - The second one is the path to the file. 
       /// - The third one is the extension type</returns>
        public static (string,string,string)? CreateNewFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|Text files (*.txt)|*.txt";
            string selectedFileType = "";

            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    
                    switch (saveFileDialog.FilterIndex)
                    {
                        case 1:
                            selectedFileType = FileExtensions.TXT;
                            break;
                        case 2:
                            selectedFileType = FileExtensions.JSON;
                            break;
                        default:
                            selectedFileType = FileExtensions.UNKNOWN;
                            break;
                    }

                    
                    FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                    using (fileInfo.Create()) 
                    {
                        
                    }

                    
                    return (saveFileDialog.SafeFileName, saveFileDialog.FileName, selectedFileType);
                }
            }
            catch (UnauthorizedAccessException)
            {
                
                MessageBox.Show(ErrorMessages.ACCESS_DENIED, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException)
            {
                
                MessageBox.Show(ErrorMessages.FILE_CREATION_ERROR, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception )
            {
                
                MessageBox.Show(ErrorMessages.GENERIC_ERROR, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;
        }
  
    }
}
