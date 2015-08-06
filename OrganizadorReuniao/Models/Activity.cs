using MySql.Data.MySqlClient;
using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public string Obs { get; set; }

        // private variables
        private Database database = new Database();

        public Result addActivity(string name, DateTime date, string place, string obs, int unitId)
        {
            MySqlCommand cmd = new MySqlCommand("insert into lds_activity (name, scheduled_by, place, obs, unit_id) values (@name, @scheduled_by, @place, @obs, @unit_id)");
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("scheduled_by", date);
            cmd.Parameters.AddWithValue("place", place);
            cmd.Parameters.AddWithValue("obs", obs);
            cmd.Parameters.AddWithValue("unit_id", unitId); 

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
            foreach (List<string> data in database.retrieveData("select email, password from lds_user where id = @id", id))
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
            }
            return user;
        }

    }
}