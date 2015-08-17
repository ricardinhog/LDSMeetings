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
        public List<Frequency> Frequencies { get; set; }
        public Member Father { get; set; }
        public Member Mother { get; set; }
        public List<Member> Children { get; set; }
        public List<Calling> Callings { get; set; }
        public Priesthood priesthood { get; set; }
        public string Gender { get; set; }

        public enum memberType
        {
            active,
            absent,
            elder,
            highPriest,
            reliefSociety,
            deacon,
            teacher,
            priest,
            beehive,
            miaMaid,
            laurel,
            sundaySchoolYouth,
            sundaySchool
        }

        // private variables
        private Database database = new Database();
        private Common common = new Common();

        public bool addMember(string email, string password)
        {
            return true;
        }

        public bool updateMember(int id, string email, string password)
        {
            return true;
        }

        public bool deleteMember(int id)
        {
            return true;
        }

        public Member getMember(int id)
        {
            return new Member();
        }

        public List<Member> getMembers(int unitId)
        {
            List<Member> members = new List<Member>();
            foreach (List<string> data in database.retrieveData("SELECT id, first_name, last_name, birthdate, unit_id, created_by, member_record, active, restricted, gender FROM bakeappdb.lds_member order by last_name, first_name"))
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
                members.Add(member);
            }
            return members;
        }

        public List<Member> getMembers(string keyword, int unitId)
        {
            keyword = "%" + keyword + "%";
            List<Member> members = new List<Member>();
            foreach (List<string> data in database.retrieveData("SELECT id, first_name, last_name, birthdate, unit_id, created_by, member_record, active, restricted, gender " +
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
                members.Add(member);
            }
            return members;
        }

        public List<Member> getMembers(memberType type, int unitId, DateTime date)
        {
            List<Member> members = new List<Member>();
            string sql = "SELECT id, first_name, last_name, " + common.formatDate("birthdate") +
                ", unit_id, " + common.formatDate("created_by") + ", member_record, active, restricted, gender " +
                "FROM bakeappdb.lds_member where unit_id = @unit ";
            switch (type)
            {
                case memberType.active:
                    // check last three months
                    break;
                case memberType.absent:
                    // if absent last sunday
                    int weekDay = (int)DateTime.Now.DayOfWeek;
                    if (weekDay == 0)
                        weekDay = 7;
                    DateTime sunday = DateTime.Now.AddDays(-weekDay).Date;



                    break;
                case memberType.beehive:
                    // female ages 12 to 13
                    break;
                case memberType.miaMaid:
                    // female ages 14 to 15
                    break;
                case memberType.laurel:
                    // female ages 16 to 17
                    break;
                case memberType.deacon:
                    // male ages 12 to 13
                    break;
                case memberType.teacher:
                    // male ages 14 to 15
                    break;
                case memberType.priest:
                    // male ages 16 to 17
                    break;
                case memberType.elder:
                    // male ages 18+ minus high priest or elder
                    break;
                case memberType.highPriest:
                    // male with high priest
                    break;
                case memberType.reliefSociety:
                    // female ages 18+
                    break;
                case memberType.sundaySchool:
                    // members 18+
                    break;
                case memberType.sundaySchoolYouth:
                    // members ages 12 to 17
                    break;
            }
            sql += " order by last_name, first_name";

            foreach (List<string> data in database.retrieveData(sql, unitId))
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
                members.Add(member);
            }
            return members;
        }
    }
}