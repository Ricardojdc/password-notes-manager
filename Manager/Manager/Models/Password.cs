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
    public class Password
    {
        //Class variables
        private int _id;
        private string _login;
        private string _password;
        private string _site;
        private string _notes;


        //Getters and setters. Json property to identify private fields
        [JsonRequired]
        [JsonProperty("_id")]
        public int Id
        {

            get { return _id; }
            set { _id = value; }
        }
        [JsonRequired]
        [JsonProperty("_login")]
        public string Login
        {

            get { return _login; }
            set { _login = value; }
        }
        [JsonRequired]
        [JsonProperty("_password")]
        public string Pass
        {

            get { return _password; }
            set { _password = value; }
        }
        [JsonRequired]
        [JsonProperty("_site")]
        public string Site
        {
            get { return _site; }
            set { _site = value; }
        }

        [JsonProperty("_notes")]
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

    }
}
