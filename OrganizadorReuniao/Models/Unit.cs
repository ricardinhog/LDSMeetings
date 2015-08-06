using MySql.Data.MySqlClient;
using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
        public List<Member> Members { get; set; }
        public List<Activity> Activities { get; set; }
        public List<Topic> Topics { get; set; }

        // private variables
        private Database database = new Database();

        public Result addUnit(string name, string phone, string email, int userId, int number)
        {
            MySqlCommand cmd = new MySqlCommand("insert into lds_unit (name, phone, email, created_by, number, user_id) values (@name, @phone, @email, now(), @number, @user_id)");
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("phone", phone);
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("number", number);
            cmd.Parameters.AddWithValue("user_id", userId);

            return database.executeQuery(cmd);
        }

        public bool updateUnit(string name, string phone, string email, int number)
        {
            return true;
        }

        public bool deleteUnit(int id)
        {
            return true;
        }

        public Unit getUnit(int userId)
        {
            Unit unit = new Unit();
            foreach (List<string> data in database.retrieveData("select name, phone, email, created_by, number from lds_unit where user_id = @id", userId))
            {
                unit.Id = userId;
                unit.Name = data[0];
                unit.Phone = data[1];
                unit.Email = data[2];
                unit.Date = new DateTime(); // TODO: convert date
                unit.Number = Convert.ToInt32(data[4]);
            }
            return unit;
        }
    }
}