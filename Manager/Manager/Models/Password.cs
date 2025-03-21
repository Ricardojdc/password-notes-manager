using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Manager.Models
{
    internal class Password
    {

        private string _id;
        private string _login;
        private string _password;

        public string Id
        {

            get { return _id; }
            set { _id = value; }
        }
        public string Login
        {

            get { return _login; }
            set { _login = value; }
        }
        public string Pass
        {

            get { return _password; }
            set { _password = value; }
        }

        public List<Password> ReadFromFile(string path)
        {
            List<Password> result = new List<Password>();   
            
            if(path == null)
            {
                return null;
            }

            try
            {
                FileInfo file = new FileInfo(path);

                if (file.Exists)
                {

                    using (StreamReader sr = file.OpenText())
                    {

                        string line;

                        while((line = sr.ReadLine()) != null)
                        {

                            string[] recompose = line.Split(" ");
                            if (recompose.Length == 3)
                            {
                                result.Add(new Password { Id = recompose[0], Login = recompose[1], Pass = recompose[2] });
                            }
                            else
                            {
                                MessageBox.Show("Invalid format");
                                return null;
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file");
            }

            return result;
        }

        public void TransformToFile()
        {


        }

        public List<Password> ReadFromJSON(string path)
        {

            return null;
        }
    }
}
