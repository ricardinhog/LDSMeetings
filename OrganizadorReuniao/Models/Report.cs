using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Report
    {
        public string lastName { get; set; }
        public string firstName { get; set; }
        public int age { get; set; }
        public int jan { get; set; }
        public int feb { get; set; }
        public int mar { get; set; }
        public int apr { get; set; }
        public int may { get; set; }
        public int jun { get; set; }
        public int jul { get; set; }
        public int aug { get; set; }
        public int sep { get; set; }
        public int oct { get; set; }
        public int nov { get; set; }
        public int dec { get; set; }
        public int year { get; set; }

        // private variables
        private Database database = new Database();
        private Common common = new Common();

        public List<Report> getReport(Member.memberType type, int year)
        {
            string sql = "select " +
                "	m.last_name,  " +
                "   m.first_name, " +
                "	DATE_FORMAT(NOW(), '%Y') - DATE_FORMAT(m.birthdate, '%Y') - (DATE_FORMAT(NOW(), '00-%m-%d') < DATE_FORMAT(m.birthdate, '00-%m-%d')) AS age,  " +
                "	(select count(0) from lds_frequency where month(created_by) = 1 and year(created_by) = year(now()) and member_id = m.id and type_id = 15) jan, " +
                "	(select count(0) from lds_frequency where month(created_by) = 2 and year(created_by) = year(now()) and member_id = m.id and type_id = 15) feb, " +
                "	(select count(0) from lds_frequency where month(created_by) = 3 and year(created_by) = year(now()) and member_id = m.id and type_id = 15) mar, " +
                "	(select count(0) from lds_frequency where month(created_by) = 4 and year(created_by) = year(now()) and member_id = m.id and type_id = 15) apr, " +
                "	(select count(0) from lds_frequency where month(created_by) = 5 and year(created_by) = year(now()) and member_id = m.id and type_id = 15) may, " +
                "	(select count(0) from lds_frequency where month(created_by) = 6 and year(created_by) = year(now()) and member_id = m.id and type_id = 15) jun, " +
                "	(select count(0) from lds_frequency where month(created_by) = 7 and year(created_by) = year(now()) and member_id = m.id and type_id = 15) jul, " +
                "	(select count(0) from lds_frequency where month(created_by) = 8 and year(created_by) = year(now()) and member_id = m.id and type_id = 15) aug, " +
                "	(select count(0) from lds_frequency where month(created_by) = 9 and year(created_by) = year(now()) and member_id = m.id and type_id = 15) sep, " +
                "	(select count(0) from lds_frequency where month(created_by) = 10 and year(created_by) = year(now()) and member_id = m.id and type_id = 15) oct, " +
                "	(select count(0) from lds_frequency where month(created_by) = 11 and year(created_by) = year(now()) and member_id = m.id and type_id = 15) nov, " +
                "	(select count(0) from lds_frequency where month(created_by) = 12 and year(created_by) = year(now()) and member_id = m.id and type_id = 15) as \"dec\" " +
                "from lds_member m ";

            string sqlCont = " where ";
            if (type == Member.memberType.sacramental)
                sqlCont = "";

            switch (type)
            {
                case Member.memberType.elder:
                    // male ages 18+ minus high priest or elder
                    sqlCont += " gender = 'M' and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) >= (18 * 365.25) and id not in (select member_id from lds_priesthood where reference = 5) ";
                    break;
                case Member.memberType.highPriest:
                    // male with high priest
                    sqlCont += " id in (select member_id from lds_priesthood where reference = 5) ";
                    break;
                case Member.memberType.reliefSociety:
                    // female ages 18+
                    sqlCont += " gender = 'F' and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) >= (18 * 365.25) ";
                    break;
                case Member.memberType.sundaySchool:
                    // members 18+
                    sqlCont += " DATEDIFF(CURRENT_DATE, birthdate) >= (18 * 365.25) ";
                    break;
                case Member.memberType.sundaySchoolYouth:
                    // members ages 12 to 17
                    sqlCont += " DATEDIFF(CURRENT_DATE, birthdate) >= (12 * 365.25) and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) < (18 * 365.25) ";
                    break;
                case Member.memberType.primary:
                    // members age 0 to 11
                    sqlCont += " DATEDIFF(CURRENT_DATE, birthdate) < (12 * 365.25) ";
                    break;
                case Member.memberType.youngMen:
                    // men age 12 to 17
                    sqlCont += " gender = 'M' and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) >= (12 * 365.25) and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) < (18 * 365.25) ";
                    break;
                case Member.memberType.youngWomen:
                    // women age 12 to 17
                    sqlCont += " gender = 'F' and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) >= (12 * 365.25) and " +
                        "DATEDIFF(CURRENT_DATE, birthdate) < (18 * 365.25) ";
                    break;
            }
            

            sql += sqlCont + " order by m.last_name, m.first_name";

            List<Report> reportData = new List<Report>();
            foreach (List<string> data in database.retrieveData(sql))
            {
                Report report = new Report();
                report.lastName = data[0];
                report.firstName = data[1];
                report.age = common.convertNumber(data[2]);
                report.jan = common.convertNumber(data[3]);
                report.feb = common.convertNumber(data[4]);
                report.mar = common.convertNumber(data[5]);
                report.apr = common.convertNumber(data[6]);
                report.may = common.convertNumber(data[7]);
                report.jun = common.convertNumber(data[8]);
                report.jul = common.convertNumber(data[9]);
                report.aug = common.convertNumber(data[10]);
                report.sep = common.convertNumber(data[11]);
                report.oct = common.convertNumber(data[12]);
                report.nov = common.convertNumber(data[13]);
                report.dec = common.convertNumber(data[14]);
                reportData.Add(report);
            }

            return reportData;
        }
    }
}