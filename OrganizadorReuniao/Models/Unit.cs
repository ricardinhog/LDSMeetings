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

        // private variables
        private Database database = new Database();
        private Common common = new Common();

        public Result addUnit(string name, int userId)
        {
            MySqlCommand cmd = new MySqlCommand("insert into lds_unit (name, created_by, user_id) values (@name, now(), @user_id)");
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("user_id", userId);

            return database.executeQuery(cmd);
        }

        public Result updateUnit(string name, int userId)
        {
            MySqlCommand cmd = new MySqlCommand("update lds_unit set name = @name where id = @id");
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("id", getUnit(userId).Id);

            return database.executeQuery(cmd);
        }

        public Unit getUnit(int userId)
        {
            Unit unit = new Unit();
            foreach (List<string> data in database.retrieveData("select id, name from lds_unit where user_id = @id", userId))
            {
                unit.Id = common.convertNumber(data[0]);
                unit.Name = data[1];
            }
            return unit;
        }
    }
}