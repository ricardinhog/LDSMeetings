using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool addAdmin(string email, string password, int userId)
        {
            return true;
        }

        public bool updateAdmin(int id, string email, string password)
        {
            return true;
        }

        public bool deleteAdmin(int id)
        {
            return true;
        }

        public Admin getAdmin(int id)
        {
            return new Admin();
        }

        public Admin getAdmin(string email, string password, int userId)
        {
            return new Admin();
        }
    }
}