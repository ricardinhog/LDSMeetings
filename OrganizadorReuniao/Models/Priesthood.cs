using MySql.Data.MySqlClient;
using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Priesthood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private Database database = new Database();

        public enum referenceType
        {
            deacon = 1,
            teacher = 2,
            priest = 3,
            elder = 4,
            highPriest = 5
        }

        public List<Priesthood> getAll()
        {
            return new List<Priesthood>();
        }

        public int getMemberPriesthood(int memberId)
        {
            int reference = 0;
            foreach (List<string> data in database.retrieveData("select reference from bakeappdb.lds_priesthood where member_id = @member_id order by id asc", memberId))
            {
                reference = new Common().convertNumber(data[0]);
            }
            return reference;
        }

        public Result deletePriesthood(int memberId)
        {
            MySqlCommand cmd = new MySqlCommand("delete from lds_priesthood where member_id = @member_id");
            cmd.Parameters.AddWithValue("member_id", memberId);
            return database.executeQuery(cmd);
        }

        public Result addPriesthood(int memberId, int reference)
        {
            deletePriesthood(memberId);

            MySqlCommand cmd = new MySqlCommand("insert into lds_priesthood (reference, member_id) values " +
                "(@reference, @member_id)");
            cmd.Parameters.AddWithValue("reference", reference);
            cmd.Parameters.AddWithValue("member_id", memberId);
            return database.executeQuery(cmd);
        }
    }
}