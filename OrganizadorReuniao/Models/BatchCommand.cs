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
            Result result = new Result(true);
            int errors = 0;

            if (file != null)
            {
                string line;
                
                // Read the file and display it line by line.
                while ((line = file.ReadLine()) != null)
                {
                    string[] data = line.Split(';');

                    if (data.Length >= 4)
                    {
                        string firstName = data[0].Trim();
                        string lastName = data[1].Trim();
                        DateTime birthDate = common.convertDate(data[2].Trim(), true);
                        string gender = data[3].Trim();
                        
                        int priesthood = 0;
                        if (data.Length > 4)
                            priesthood = common.convertNumber(data[4].Trim());

                        Member member = new Member().findMember(lastName, firstName, birthDate, gender, unitId);

                        if (member == null || member.Id == 0)
                        {
                            MySqlCommand cmd = new MySqlCommand("insert into lds_member (first_name, last_name, birthdate, unit_id, created_by, active, restricted, gender, unit_member) values " +
                                "(@first_name, @last_name, @birthdate, @unit_id, now(), 1, 0, @gender, 1)");
                            cmd.Parameters.AddWithValue("first_name", firstName);
                            cmd.Parameters.AddWithValue("last_name", lastName);
                            cmd.Parameters.AddWithValue("birthdate", birthDate);
                            cmd.Parameters.AddWithValue("unit_id", unitId);
                            cmd.Parameters.AddWithValue("gender", gender);
                            result = database.executeQuery(cmd);

                            if (result.Success)
                            {
                                if (gender == "M" && priesthood != 0)
                                {
                                    result = new Priesthood().addPriesthood(result.Id, priesthood);
                                    if (!result.Success)
                                        errors++;
                                }
                            }
                            else
                                errors++;
                        }
                        else
                        {
                            int priestH = new Priesthood().getMemberPriesthood(member.Id);
                            if (priestH != priesthood)
                            {
                                result = new Priesthood().deletePriesthood(member.Id);
                                if (result.Success)
                                {
                                    result = new Priesthood().addPriesthood(member.Id, priesthood);
                                    if (!result.Success)
                                        errors++;
                                }
                                else
                                    errors++;
                            }
                        }
                    }
                }

                file.Close();
            }

            if (errors > 0)
                result.Success = false;

            return result;
        }
    }
}