using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Speaker
    {
        public string Name { get; set; }
        public string Theme { get; set; }
        public DateTime Date { get; set; }
        public int Position { get; set; }

        private Database database = new Database();
        private Common common = new Common();

        public Speaker() { }

        public Speaker(string name, string theme, DateTime date, int position)
        {
            Name = name;
            Theme = theme;
            Date = date;
            Position = position;
        }

        public List<Speaker> getAll(int unitId)
        {
            string sql = "select " +
                "	" + common.formatDate("s.date") + ",  " +
                "	s.speaker1, (select concat(first_name, ' ', last_name) from lds_member where id = s.speaker1) Speaker1, speaker1_theme, " +
                "	s.speaker2, (select concat(first_name, ' ', last_name) from lds_member where id = s.speaker2) Speaker2, speaker2_theme, " +
                "	s.speaker3, (select concat(first_name, ' ', last_name) from lds_member where id = s.speaker3) Speaker3, speaker3_theme, " +
                "	s.speaker5, (select concat(first_name, ' ', last_name) from lds_member where id = s.speaker5) Speaker5, speaker5_theme " +
                "from lds_sacramental s " +
                "where " +
                "	((s.speaker1 > 0 and s.speaker1 is not null) or  " +
                "	(s.speaker2 > 0 and s.speaker2 is not null) or  " +
                "	(s.speaker3 > 0 and s.speaker3 is not null) or  " +
                "	(s.speaker5 > 0 and s.speaker5 is not null)) and date >= curdate() and s.unit_id = @unit_id " +
                "	order by s.date asc";

            List<Speaker> speakers = new List<Speaker>();

            foreach (List<string> data in database.retrieveData(sql, unitId))
            {
                DateTime date = common.convertDate(data[0]);

                // check speaker 1
                if (common.convertNumber(data[1]) > 0)
                    speakers.Add(new Speaker(data[2], data[3], date, 1));

                // check speaker 2
                if (common.convertNumber(data[4]) > 0)
                    speakers.Add(new Speaker(data[5], data[6], date, 2));

                // check speaker 3
                if (common.convertNumber(data[7]) > 0)
                    speakers.Add(new Speaker(data[8], data[9], date, 3));

                // check speaker 4
                if (common.convertNumber(data[10]) > 0)
                    speakers.Add(new Speaker(data[11], data[12], date, 4));
            }
            return speakers;
        }
    }
}