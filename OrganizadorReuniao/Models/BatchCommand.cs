using MySql.Data.MySqlClient;
using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class BatchCommand
    {
        // private variables
        private Database database = new Database();
        private Common common = new Common();

        /// <summary>
        /// File format: first;last;1982-12-23;M
        ///              first;last;1989-07-12;F
        /// </summary>
        /// <param name="file"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public Result execute(StreamReader file, int unitId)
        {
            Result result = new Result(false);

            if (file != null)
            {
                string line;
                List<MySqlCommand> commands = new List<MySqlCommand>();

                // Read the file and display it line by line.
                while ((line = file.ReadLine()) != null)
                {
                    string[] data = line.Split(';');

                    if (data.Length >= 4)
                    {
                        string firstName = data[0];
                        string lastName = data[1];
                        DateTime birthDate = common.convertDate(data[2], true);
                        string gender = data[3];
                        int priesthood = 0;
                        if (data.Length > 4)
                            priesthood = common.convertNumber(data[4]);

                        MySqlCommand cmd = new MySqlCommand("insert into lds_member (first_name, last_name, birthdate, unit_id, created_by, active, restricted, gender, unit_member) values " +
                            "(@first_name, @last_name, @birthdate, @unit_id, now(), 1, 0, @gender, 1)");
                        cmd.Parameters.AddWithValue("first_name", firstName);
                        cmd.Parameters.AddWithValue("last_name", lastName);
                        cmd.Parameters.AddWithValue("birthdate", birthDate);
                        cmd.Parameters.AddWithValue("unit_id", unitId);
                        cmd.Parameters.AddWithValue("gender", gender);
                        commands.Add(cmd);

                        //if (gender == "M" && priesthood != 0)
                        //{
                        //    MySqlCommand cmd2 = new MySqlCommand
                        //}
                    }
                }

                file.Close();

                // execute batch commands
                result = database.executeBatch(commands);
            }

            return result;
        }
    }
}