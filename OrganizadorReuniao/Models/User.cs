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
        public int Unit { get; set; }
        public string UnitName { get; set; }

        // private variables
        private Database database = new Database();
        private Common common = new Common();

        public Result addUser(string email, string password)
        {
            MySqlCommand cmd = new MySqlCommand("insert into lds_user (email, password, created_by) values (@email, @password, now())");
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("password", common.hash(password)); 

            return database.executeQuery(cmd);
        }

        public Result addUser(string email, string password, int unitId)
        {
            MySqlCommand cmd = new MySqlCommand("insert into lds_user (email, password, created_by) values (@email, @password, now())");
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("password", common.hash(password));

            Result result = database.executeQuery(cmd);
            if (result.Success)
            {
                cmd = new MySqlCommand("insert into lds_user_unit (user_id, unit_id) values (@user_id, @unit_id)");
                cmd.Parameters.AddWithValue("user_id", result.Id);
                cmd.Parameters.AddWithValue("unit_id", unitId);

                result = database.executeQuery(cmd);
            }
            return result;
        }

        public Result updateUser(int id, string email, string password)
        {
            MySqlCommand cmd = new MySqlCommand("update lds_user set email = @email, password = @password where id = @id");
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("password", common.hash(password));
            cmd.Parameters.AddWithValue("id", id);

            return database.executeQuery(cmd);
        }

        public Result deleteUser(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from lds_user_unit where user_id = @id");
            cmd.Parameters.AddWithValue("id", id);
            Result result = database.executeQuery(cmd);

            if (result.Success)
            {
                cmd = new MySqlCommand("delete from lds_user where id = @id");
                cmd.Parameters.AddWithValue("id", id);

                result = database.executeQuery(cmd);
            }

            return result;
        }

        public List<User> getWardUsers(int unitId)
        {
            List<User> users = new List<User>();
            foreach (List<string> data in database.retrieveData("select us.id, us.email from lds_user us, lds_unit un, lds_user_unit uu " +
                " where unit_id = @unit_id " +
                "   and uu.unit_id = un.id " +
                "   and uu.user_id = us.id " +
                " order by us.email", unitId))
            {
                User user = new User();
                user.Id = common.convertNumber(data[0]);
                user.Email = data[1];
                users.Add(user);
            }
            return users;
        }

        public User getUser(int id)
        {
            User user = new User();
            foreach(List<string> data in database.retrieveData("select email, password from lds_user where id = @id", id))
            {
                user.Id = id;
                user.Email = data[0];
                user.Password = data[1];
                Unit unit = new Unit().getUnit(id);
                user.Unit = unit.Id;
                user.UnitName = unit.Name;
            }
            return user;
        }

        public User getUser(string email, string password)
        {
            User user = new User();
            foreach (List<string> data in database.retrieveData("select id from lds_user where email = @email and password = @password", email, common.hash(password)))
            {
                user.Id = Convert.ToInt32(data[0]);
                user.Email = email;
                user.Password = password;
                Unit unit = new Unit().getUnit(user.Id);
                user.Unit = unit.Id;
                user.UnitName = unit.Name;
            }
            return user;
        }

        public User getUser(string email)
        {
            User user = new User();
            foreach (List<string> data in database.retrieveData("select id from lds_user where email = @email", email))
            {
                user.Id = Convert.ToInt32(data[0]);
                user.Email = email;
                Unit unit = new Unit().getUnit(user.Id);
                user.Unit = unit.Id;
                user.UnitName = unit.Name;
            }
            return user;
        }

        public bool emailExists(string email)
        {
            bool exists = false;
            foreach (List<string> data in database.retrieveData("select id from lds_user where email = @email", email))
            {
                exists = true;
            }
            return exists;
        }
    }
}