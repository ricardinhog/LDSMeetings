using MySql.Data.MySqlClient;
using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Frequency
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private Database database = new Database();
        private Common common = new Common();

        public List<Frequency> getTypes()
        {
            List<Frequency> frequencies = new List<Frequency>();
            foreach (List<string> data in database.retrieveData("SELECT id, name from lds_frequency_type"))
            {
                Frequency frequency = new Frequency();
                frequency.Id = common.convertNumber(data[0]);
                frequency.Name = getDescription(data[1]);
                frequencies.Add(frequency);
            }
            frequencies = frequencies.OrderBy(f => f.Name).ToList();
            return frequencies;
        }

        private string getDescription(string key)
        {
            return common.readResourceValue(key);
        }

        public bool wasPresent(int memberId, DateTime date, Member.memberType type)
        {
            bool present = false;
            foreach (List<string> data in database.retrieveData("select 1 from lds_frequency where member_id = @member_id and created_by = @date and type_id = @type", memberId, common.convertDate(date, true), (int)type))
            {
                present = true;
            }
            return present;
        }

        public bool setPresent(int memberId, DateTime date, Member.memberType listType, bool present)
        {
            MySqlCommand cmd = null;
            if (present)
            {
                cmd = new MySqlCommand("insert into lds_frequency (member_id, created_by, type_id) values (@id, @date, @type)");
                cmd.Parameters.AddWithValue("id", memberId);
                cmd.Parameters.AddWithValue("date", common.convertDate(date, true));
                cmd.Parameters.AddWithValue("type", (int)listType);
            }
            else
            {
                cmd = new MySqlCommand("delete from lds_frequency where member_id = @id and created_by = @date and type_id = @type");
                cmd.Parameters.AddWithValue("id", memberId);
                cmd.Parameters.AddWithValue("date", common.convertDate(date, true));
                cmd.Parameters.AddWithValue("type", (int)listType);
            }
            return database.executeQuery(cmd).Success;
        }
    }
}