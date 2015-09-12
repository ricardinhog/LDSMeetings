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

        public Priesthood getPriesthood(int id)
        {
            return new Priesthood();
        }

        public Result addPriesthood(int memberId, int reference)
        {
            MySqlCommand cmd = new MySqlCommand("insert into lds_priesthood (reference, member_id) values " +
                "(@reference, @member_id)");
            cmd.Parameters.AddWithValue("reference", reference);
            cmd.Parameters.AddWithValue("member_id", memberId);
            return database.executeQuery(cmd);
        }
    }
}