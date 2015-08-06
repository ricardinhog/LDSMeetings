using MySql.Data.MySqlClient;
using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedBy { get; set; }
        public List<Admin> Administrators { get; set; }
        public List<Unit> Units { get; set; }

        // private variables
        private Database database = new Database();

        public Result addUser(string email, string password)
        {
            MySqlCommand cmd = new MySqlCommand("insert into lds_user (email, password, created_by) values (@email, @password, now())");
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("password", password); // TODO: Add encryption

            return database.executeQuery(cmd);
        }

        public Result updateUser(int id, string email, string password)
        {
            MySqlCommand cmd = new MySqlCommand("update lds_user set email = @email, password = @password where id = @id");
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("password", password);
            cmd.Parameters.AddWithValue("id", id);

            return database.executeQuery(cmd);
        }

        public Result deleteUser(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from lds_user where id = @id");
            cmd.Parameters.AddWithValue("id", id);

            return database.executeQuery(cmd);
        }

        public User getUser(int id)
        {
            User user = new User();
            foreach(List<string> data in database.retrieveData("select email, password from lds_user where id = @id", id))
            {
                user.Id = id;
                user.Email = data[0];
                user.Password = data[1];
            }
            return user;
        }

        public User getUser(string email, string password)
        {
            User user = new User();
            foreach (List<string> data in database.retrieveData("select id from lds_user where email = @email and password = @password", email, password))
            {
                user.Id = Convert.ToInt32(data[0]);
                user.Email = email;
                user.Password = password;
                user.Units = new List<Unit>();
                user.Units.Add(new Unit().getUnit(user.Id));
            }
            return user;
        }

    }
}