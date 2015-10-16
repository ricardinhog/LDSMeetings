using MySql.Data.MySqlClient;
using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime Date { get; set; }
        public string MemberRecord { get; set; }
        public bool Actived { get; set; }
        public bool Restricted { get; set; }
        public bool isUnitMember { get; set; }
        public List<Frequency> Frequencies { get; set; }
        public Member Father { get; set; }
        public Member Mother { get; set; }
        public List<Member> Children { get; set; }
        public List<Calling> Callings { get; set; }
        public int priesthood { get; set; }
        public string Gender { get; set; }
        public string FormatedDate
        {
            get
            {
                return string.Format("{0}/{1}/{2}", BirthDate.Day, BirthDate.Month, BirthDate.Year);
            }
        }
        public bool Present { get; set; }

        public enum memberType
        {
            active = 1,
            absent = 2,
            elder = 3,
            highPriest = 4,
            reliefSociety = 5,
            deacon = 6,
            teacher = 7,
            priest = 8,
            beehive = 9,
            miaMaid = 10,
            laurel = 11,
            sundaySchoolYouth = 12,
            sundaySchool = 13,
            primary = 14,
            sacramental = 15,
            youngMen = 16,
            youngWomen = 17,
            adultsMen = 18
        }

        // private variables
        private Database database = new Database();
        private Common common = new Common();

        public Result addMember(string firstName, string lastName, DateTime birthDate, string gender, bool isUnitMember, string priesthood, int unitId)
        {
            firstName = firstName.Trim();
            lastName = lastName.Trim();

            MySqlCommand cmd = new MySqlCommand("insert into lds_member (first_name, last_name, birthdate, unit_id, created_by, member_record, active, restricted, gender, unit_member) values " +
                "(@first_name, @last_name, @birthdate, @unit_id, now(), null, 1, 0, @gender, @unit_member)");
            cmd.Parameters.AddWithValue("first_name", firstName);
            cmd.Parameters.AddWithValue("last_name", lastName);
            cmd.Parameters.AddWithValue("birthdate", common.convertDate(birthDate, true));
            cmd.Parameters.AddWithValue("unit_id", unitId);
            cmd.Parameters.AddWithValue("gender", gender);
            cmd.Parameters.AddWithValue("unit_member", common.convertBool(isUnitMember));
            Result result = database.executeQuery(cmd);
            if (result.Success)
            {
                if (priesthood != "0" && gender == "M")
                    result = new Priesthood().addPriesthood(result.Id, common.convertNumber(priesthood));
            }
            return result;
        }

        public Result updateMember(int id, string firstName, string lastName, DateTime birthDate, string gender, bool isUnitMember, string priesthood, int unitId)
        {
            firstName = firstName.Trim();
            lastName = lastName.Trim();

            MySqlCommand cmd = new MySqlCommand("update lds_member set first_name = @first_name, last_name = @last_name, birthdate = @birthdate, gender = @gender, unit_member = @unit_member " +
                " where id = @id and unit_id = @unit_id");
            cmd.Parameters.AddWithValue("first_name", firstName);
            cmd.Parameters.AddWithValue("last_name", lastName);
            cmd.Parameters.AddWithValue("birthdate", common.convertDate(birthDate, true));
            cmd.Parameters.AddWithValue("gender", gender);
            cmd.Parameters.AddWithValue("unit_member", common.convertBool(isUnitMember));
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("unit_id", unitId);
            Result result = database.executeQuery(cmd);
            if (result.Success)
            {
                result = new Priesthood().deletePriesthood(id);
                if (priesthood != "0" && gender == "M" && result.Success)
                    result = new Priesthood().addPriesthood(id, common.convertNumber(priesthood));
            }
            return result;
        }

        public Result deleteMember(int id)
        {
            Result result = null;

            MySqlCommand cmd = new MySqlCommand("delete from lds_calling_member where member_id = @id");
            cmd.Parameters.AddWithValue("id", id);
            result = database.executeQuery(cmd);

            cmd = new MySqlCommand("delete from lds_frequency where member_id = @id");
            cmd.Parameters.AddWithValue("id", id);
            result = database.executeQuery(cmd);

            cmd = new MySqlCommand("delete from lds_priesthood where member_id = @id");
            cmd.Parameters.AddWithValue("id", id);
            result = database.executeQuery(cmd);

            if (result.Success)
            {
                cmd = new MySqlCommand("delete from lds_member where id = @id");
                cmd.Parameters.AddWithValue("id", id);
                result = database.executeQuery(cmd);
            }

            return result;
        }

        public Member getMember(int id, int unitId)
        {
            Member member = new Member();

            foreach (List<string> data in database.retrieveData("SELECT id, first_name, last_name, " + common.formatDate("birthdate") + ", unit_id, " + common.formatDate("created_by") + ", member_record, active, restricted, gender, unit_member " +
                "FROM bakeappdb.lds_member  " +
                "where id = @id and unit_id = @unit_id " +
                "order by last_name, first_name", id, unitId))
            {
                member.Id = common.convertNumber(data[0]);
                member.FirstName = data[1];
                member.LastName = data[2];
                member.BirthDate = common.convertDate(data[3]);
                member.Date = common.convertDate(data[5]);
                member.MemberRecord = data[6];
                member.Actived = common.convertBool(data[7]);
                member.Restricted = common.convertBool(data[8]);
                member.Gender = data[9];
                member.isUnitMember = common.convertBool(data[10]);
                member.priesthood = new Priesthood().getMemberPriesthood(member.Id);
            }
            return member;
        }

        public List<Member> getMembers(int unitId)
        {
            return getMembers(memberType.sacramental, unitId);
        }

        public List<Member> getMembers(string keyword, int unitId)
        {
            keyword = "%" + keyword + "%";
            List<Member> members = new List<Member>();
            foreach (List<string> data in database.retrieveData("SELECT id, first_name, last_name, " + common.formatDate("birthdate") + ", unit_id, " + common.formatDate("created_by") + ", member_record, active, restricted, gender, unit_member " +
                "FROM bakeappdb.lds_member  " +
                "where unit_id = @unit " +
                "  and (first_name like @first_name " +
                "   or last_name like @last_name " +
                "   or birthdate like @birthdate " +
                "   or member_record like @member_record) " +
                "order by last_name, first_name", unitId, keyword, keyword, keyword, keyword))
            {
                Member member = new Member();
                member.Id = common.convertNumber(data[0]);
                member.FirstName = data[1];
                member.LastName = data[2];
                member.BirthDate = common.convertDate(data[3]);
                member.Date = common.convertDate(data[5]);
                member.MemberRecord = data[6];
                member.Actived = common.convertBool(data[7]);
                member.Restricted = common.convertBool(data[8]);
                member.Gender = data[9];
                member.isUnitMember = common.convertBool(data[10]);
                members.Add(member);
            }
            return members;
        }

        public List<Member> getMembers(memberType type, int unitId, DateTime date = new DateTime())
        {
            List<Member> members = new List<Member>();
            string sql = "SELECT id, first_name, last_name, " + common.formatDate("birthdate") +
                ", unit_id, " + common.formatDate("created_by") + ", member_record, (select count(0) from lds_frequency where created_by >= (NOW() - INTERVAL 3 MONTH) and member_id = m.id) presencas, restricted, gender, unit_member " +
                "FROM bakeappdb.lds_member m where unit_id = @unit ";

            string sqlCont = string.Empty;

            switch (type)
            {
                case memberType.active:
                    // check last three months
                    sqlCont += " and id in (select member_id from lds_frequency where created_by >= (NOW() - INTERVAL 3 MONTH)) ";
                    break;
                case memberType.absent:
                    // if absent last sunday
                    DateTime sunday = common.getLastSundayDate();
                    sqlCont += string.Format(" and id not in (select member_id from lds_frequency where created_by >= '{0}-{1}-{2}') ",
                        sunday.Year, sunday.Month.ToString().PadLeft(2, '0'), sunday.Day.ToString().PadLeft(2, '0'));
                    break;
                case memberType.beehive:
                    // female ages 12 to 13
                    sqlCont += " and gender = 'F' and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) >= (12 * 365.25) and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) < (14 * 365.25) ";
                    break;
                case memberType.miaMaid:
                    // female ages 14 to 15
                    sqlCont += " and gender = 'F' and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) >= (14 * 365.25) and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) < (16 * 365.25) ";
                    break;
                case memberType.laurel:
                    // female ages 16 to 17
                    sqlCont += " and gender = 'F' and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) >= (16 * 365.25) and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) < (18 * 365.25) ";
                    break;
                case memberType.deacon:
                    // male ages 12 to 13
                    sqlCont += " and id in (select member_id from lds_priesthood where reference = 1) ";
                    break;
                case memberType.teacher:
                    // male ages 14 to 15
                    sqlCont += " and id in (select member_id from lds_priesthood where reference = 2) ";
                    break;
                case memberType.priest:
                    // male ages 16 to 17
                    sqlCont += " and id in (select member_id from lds_priesthood where reference = 3) ";
                    break;
                case memberType.elder:
                    // male ages 18+ minus high priest or elder
                    sqlCont += " and gender = 'M' and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) >= (18 * 365.25) and id not in (select member_id from lds_priesthood where reference = 5) ";
                    break;
                case memberType.highPriest:
                    // male with high priest
                    sqlCont += " and id in (select member_id from lds_priesthood where reference = 5) ";
                    break;
                case memberType.reliefSociety:
                    // female ages 18+
                    sqlCont += " and gender = 'F' and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) >= (18 * 365.25) ";
                    break;
                case memberType.sundaySchool:
                    // members 18+
                    sqlCont += " and DATEDIFF(CURRENT_DATE, birthdate) >= (18 * 365.25) ";
                    break;
                case memberType.sundaySchoolYouth:
                    // members ages 12 to 17
                    sqlCont += " and DATEDIFF(CURRENT_DATE, birthdate) >= (12 * 365.25) and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) < (18 * 365.25) ";
                    break;
                case memberType.primary:
                    // members age 0 to 11
                    sqlCont += " and DATEDIFF(CURRENT_DATE, birthdate) < (12 * 365.25) ";
                    break;
                case memberType.youngMen:
                    // men age 12 to 17
                    sqlCont += " and gender = 'M' and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) >= (12 * 365.25) and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) < (18 * 365.25) ";
                    break;
                case memberType.youngWomen:
                    // women age 12 to 17
                    sqlCont += " and gender = 'F' and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) >= (12 * 365.25) and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) < (18 * 365.25) ";
                    break;
                case memberType.adultsMen:
                    // men age 18+ 
                    sqlCont += " and gender = 'M' and " +
                    "DATEDIFF(CURRENT_DATE, birthdate) >= (18 * 365.25) ";
                    break;
            }
            sql += sqlCont + " order by last_name, first_name";

            foreach (List<string> data in database.retrieveData(sql, unitId))
            {
                Member member = new Member();
                member.Id = common.convertNumber(data[0]);
                member.FirstName = data[1];
                member.LastName = data[2];
                member.BirthDate = common.convertDate(data[3]);
                member.Date = common.convertDate(data[5]);
                member.MemberRecord = data[6];
                member.Actived = (data[7] != "0");
                member.Restricted = common.convertBool(data[8]);
                member.Gender = data[9];
                member.Present = new Frequency().wasPresent(member.Id, date, type);
                member.isUnitMember = common.convertBool(data[10]);
                members.Add(member);
            }

            members = members.OrderByDescending(m => m.Actived).ThenBy(m => m.LastName).ThenBy(m => m.FirstName).ToList();
            return members;
        }
    }
}