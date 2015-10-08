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

        public List<Speaker> getAll()
        {
            string sql = "select " +
                "	" + common.formatDate("s.date") + ",  " +
                "	s.speaker1, (select concat(first_name, ' ', last_name) from lds_member where id = s.speaker1) Speaker1, speaker1_theme, " +
                "	s.speaker2, (select concat(first_name, ' ', last_name) from lds_member where id = s.speaker2) Speaker2, speaker2_theme, " +
                "	s.speaker3, (select concat(first_name, ' ', last_name) from lds_member where id = s.speaker3) Speaker3, speaker3_theme, " +
                "	s.speaker5, (select concat(first_name, ' ', last_name) from lds_member where id = s.speaker5) Speaker5, speaker5_theme " +
                "from lds_sacramental s " +
                "where " +
                "	(s.speaker1 > 0 and s.speaker1 is not null) or  " +
                "	(s.speaker2 > 0 and s.speaker2 is not null) or  " +
                "	(s.speaker3 > 0 and s.speaker3 is not null) or  " +
                "	(s.speaker5 > 0 and s.speaker5 is not null) and date >= now()" +
                "	order by s.date asc";

            List<Speaker> speakers = new List<Speaker>();

            foreach (List<string> data in database.retrieveData(sql))
            {
                Speaker speaker = new Speaker();
                speaker.Date = common.convertDate(data[0]);

                // check speaker 1
                if (common.convertNumber(data[1]) > 0)
                {
                    speaker.Name = data[2];
                    speaker.Theme = data[3];
                    speaker.Position = 1;
                    speakers.Add(speaker);
                }

                // check speaker 2
                if (common.convertNumber(data[4]) > 0)
                {
                    speaker.Name = data[5];
                    speaker.Theme = data[6];
                    speaker.Position = 2;
                    speakers.Add(speaker);
                }

                // check speaker 3
                if (common.convertNumber(data[7]) > 0)
                {
                    speaker.Name = data[8];
                    speaker.Theme = data[9];
                    speaker.Position = 3;
                    speakers.Add(speaker);
                }

                // check speaker 4
                if (common.convertNumber(data[10]) > 0)
                {
                    speaker.Name = data[11];
                    speaker.Theme = data[12];
                    speaker.Position = 4;
                    speakers.Add(speaker);
                }
            }
            return speakers;
        }
    }
}