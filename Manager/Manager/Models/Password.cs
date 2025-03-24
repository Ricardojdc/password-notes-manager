using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Manager.Models
{
    internal class Password
    {
        //Class variables
        private string _id;
        private string _login;
        private string _password;

        //Getters and setters. Json property to identify private fields
        [JsonProperty("_id")]
        public string Id
        {

            get { return _id; }
            set { _id = value; }
        }

        [JsonProperty("_login")]
        public string Login
        {

            get { return _login; }
            set { _login = value; }
        }

        [JsonProperty("_password")]
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
            string jsonString = File.ReadAllText(path);
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    
                    MissingMemberHandling = MissingMemberHandling.Error
                };
              
                List<Password> list = JsonConvert.DeserializeObject<List<Password>>(jsonString, settings);

                if (list != null)
                {
                    return list;
                }

            }           
            catch (Exception ex)
            {

                MessageBox.Show("Invalid Format");
            }
         
            

            return null;
        }
    }
}
