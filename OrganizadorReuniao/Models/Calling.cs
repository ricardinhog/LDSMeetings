using MySql.Data.MySqlClient;
using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Calling
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Member { get; set; }
        public string Other { get; set; }
        public DateTime Date { get; set; }
        public bool CallingFlag { get; set; }

        public int MemberId { get; set; }
        public int CallingId { get; set; }

        // private variables
        private Database database = new Database();
        private Common common = new Common();

        public List<Calling> getAll()
        {
            List<Calling> callings = new List<Calling>();
            foreach (List<string> data in database.retrieveData("select c.id, o.name, c.name from lds_calling c, lds_organization o " +
                "where c.organization_id = o.id " +
                "order by o.name, c.name"))
            {
                Calling calling = new Calling();
                calling.Name = string.Format("{0} - {1}", data[1], data[2]);
                calling.Id = Convert.ToInt32(data[0]);
                callings.Add(calling);
            }
            return callings;
        }

        private string getCallingName(int id)
        {
            string callingName = string.Empty;
            foreach (List<string> data in database.retrieveData("select o.name, c.name from lds_calling c, lds_organization o " +
                "where c.organization_id = o.id " +
                " and c.id = @id " +
                "order by o.name, c.name", id))
            {
                callingName = data[0] + " - " + data[1];
            }
            return callingName;
        }

        public Result add(int callingId, int memberId, string other, DateTime date, bool calling)
        {
            MySqlCommand cmd = new MySqlCommand("insert into lds_calling_member (calling_id, member_id, other, calling_date, calling_flag) " + 
                "values (@calling_id, @member_id, @other, @calling_date, @calling_flag)");
            cmd.Parameters.AddWithValue("calling_id", callingId);
            cmd.Parameters.AddWithValue("member_id", memberId);
            cmd.Parameters.AddWithValue("other", other);
            cmd.Parameters.AddWithValue("calling_date", date);
            cmd.Parameters.AddWithValue("calling_flag", common.convertBool(calling));

            return database.executeQuery(cmd);
        }

        public Result update(int id, int callingId, int memberId, string other, DateTime date, bool calling)
        {
            MySqlCommand cmd = new MySqlCommand("update lds_calling_member set calling_id = @calling_id, member_id = @member_id, other = @other, calling_date = @calling_date, calling_flag = @calling_flag where id = @id");
            cmd.Parameters.AddWithValue("calling_id", callingId);
            cmd.Parameters.AddWithValue("member_id", memberId);
            cmd.Parameters.AddWithValue("other", other);
            cmd.Parameters.AddWithValue("calling_date", date);
            cmd.Parameters.AddWithValue("calling_flag", common.convertBool(calling));
            cmd.Parameters.AddWithValue("id", id);

            return database.executeQuery(cmd);
        }

        public Result delete(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from lds_calling_member where id = @id");
            cmd.Parameters.AddWithValue("id", id);

            return database.executeQuery(cmd);
        }

        public Result deleteOld()
        {
            MySqlCommand cmd = new MySqlCommand("delete from lds_calling_member where calling_date < now()");
            return database.executeQuery(cmd);
        }

        public List<Calling> getAllCallings(int unit_id)
        {
            List<Calling> list = new List<Calling>();
            foreach (List<string> data in database.retrieveData("select cm.id, cm.calling_id, cm.member_id, cm.other, " + common.formatDate("cm.calling_date") + ", cm.calling_flag " +
                "	from lds_calling_member cm, lds_member m " +
                "	where cm.member_id = m.id " +
                "	  and m.unit_id = @unit_id " +
                "order by cm.calling_date asc", unit_id))
            {
                Calling call = new Calling();
                call.Id = common.convertNumber(data[0]);
                call.Name = getCallingName(common.convertNumber(data[1]));
                Member member = new Member().getMember(common.convertNumber(data[2]), unit_id);
                call.Member = member.FirstName + " " + member.LastName;
                call.Other = data[3];
                call.Date = common.convertDate(data[4]);
                call.CallingFlag = common.convertBool(data[5]);
                list.Add(call);
            }
            return list;
        }

        public List<Calling> getNextNMonths(int nInterval, int unitId)
        {
            List<Calling> list = new List<Calling>();
            foreach (List<string> data in database.retrieveData("select cm.id, cm.calling_id, cm.member_id, cm.other, " + common.formatDate("cm.calling_date") + ", cm.calling_flag " +
                "from lds_calling_member cm, lds_member m where cm.member_id = m.id and cm.calling_date > now() and cm.calling_date < DATE_ADD(now(), INTERVAL " + nInterval + " MONTH) " + 
                " and m.unit_id = @unit_id order by cm.calling_date asc", unitId))
            {
                Calling call = new Calling();
                call.Id = common.convertNumber(data[0]);
                call.Name = getCallingName(common.convertNumber(data[1]));
                Member member = new Member().getMember(common.convertNumber(data[2]), unitId);
                call.Member = member.FirstName + " " + member.LastName;
                call.Other = data[3];
                call.Date = common.convertDate(data[4]);
                call.CallingFlag = common.convertBool(data[5]);
                list.Add(call);
            }
            return list;
        }

        public Calling get(int id, int unitId)
        {
            Calling call = null;
            foreach (List<string> data in database.retrieveData("select cm.id, cm.calling_id, cm.member_id, cm.other, " + common.formatDate("cm.calling_date") + ", cm.calling_flag " + 
                "from lds_calling_member cm, lds_member m where cm.member_id = m.id and cm.id = @id and m.unit_id = @unit_id", id, unitId))
            {
                call = new Calling();
                call.Id = common.convertNumber(data[0]);
                call.Name = getCallingName(common.convertNumber(data[1]));
                call.CallingId = common.convertNumber(data[1]);
                Member member = new Member().getMember(common.convertNumber(data[2]), unitId);
                call.MemberId = common.convertNumber(data[2]);
                call.Member = member.FirstName + " " + member.LastName;
                call.Other = data[3];
                call.Date = common.convertDate(data[4]);
                call.CallingFlag = common.convertBool(data[5]);
            }
            return call;
        }
    }
}