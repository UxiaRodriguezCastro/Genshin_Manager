using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Genshintool
{
    public class account
    {
        //account
        private String account_name;
        private int account_world_level;
        private bool account_gender;
        private String account_description;
        private String account_email;
        private String account_password;
        private List<character> account_characters;
        private List<weapon> account_weapons;
        private List<artifact> account_artifacts;

        public account() {
            this.account_characters = new List<character>();
            this.account_weapons = new List<weapon>();
            this.account_artifacts = new List<artifact>();
        }

        public account(string account_name, int account_world_level, string account_description, string account_email, string account_password, List<character> account_characters, List<weapon> account_weapons, List<artifact> account_artifacts, bool account_gender)
        {
            this.account_name = account_name;
            this.account_world_level = account_world_level;
            this.account_description = account_description;
            this.account_email = account_email;
            this.account_password = account_password;
            this.account_gender = account_gender;
            this.account_characters = account_characters;
            this.account_weapons = account_weapons;
            this.account_artifacts = account_artifacts;
            
        }

        public string Account_name { get => account_name; set => account_name = value; }
        public int Account_world_level { get => account_world_level; set => account_world_level = value; }
        public string Account_description { get => account_description; set => account_description = value; }
        public string Account_email { get => account_email; set => account_email = value; }
        public string Account_password { get => account_password; set => account_password = value; }
        public bool Account_gender { get => account_gender; set => account_gender = value; }
        public List<character> Account_characters { get => account_characters; set => account_characters = value; }
        public List<weapon> Account_weapons { get => account_weapons; set => account_weapons = value; }
        public List<artifact> Account_artifacts { get => account_artifacts; set => account_artifacts = value; }
        
    }


}
