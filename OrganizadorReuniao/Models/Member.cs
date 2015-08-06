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

        // private variables
        private Database database = new Database();

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
                member.Id = Convert.ToInt32(data[0]);
                member.FirstName = data[1];
                member.LastName = data[2];
                member.BirthDate = new DateTime(); // TODO: convert date
                member.Date = new DateTime(); // TODO: convert date
                member.MemberRecord = data[6];
                member.Actived = (data[7] == "1");
                member.Restricted = (data[8] == "1");
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
                member.Id = Convert.ToInt32(data[0]);
                member.FirstName = data[1];
                member.LastName = data[2];
                member.BirthDate = new DateTime(); // TODO: convert date
                member.Date = new DateTime(); // TODO: convert date
                member.MemberRecord = data[6];
                member.Actived = (data[7] == "1");
                member.Restricted = (data[8] == "1");
                member.Gender = data[9];
                members.Add(member);
            }
            return members;
        }
    }
}