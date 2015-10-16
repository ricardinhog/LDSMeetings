using MySql.Data.MySqlClient;
using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Memo
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int conductedBy { get; set; }
        public int presidedBy { get; set; }
        public string recognitions { get; set; }
        public int openingHymn { get; set; }
        public int firstPrayer { get; set; }
        public string stake { get; set; }
        public int stakeFlag { get; set; }
        public string ward { get; set; }
        public int wardFlag { get; set; }
        public int sacramentalHymn { get; set; }
        public int speaker1 { get; set; }
        public int speaker2 { get; set; }
        public int speaker3 { get; set; }
        public int speaker4 { get; set; }
        public int speaker5 { get; set; }
        public string speaker1Theme { get; set; }
        public string speaker2Theme { get; set; }
        public string speaker3Theme { get; set; }
        public string speaker4Theme { get; set; }
        public string speaker5Theme { get; set; }
        public int intermediateHymn { get; set; }
        public int pianistBy { get; set; }
        public int hymnConductedBy { get; set; }
        public string otherSubjects { get; set; }
        public int lastHymn { get; set; }
        public int lastPrayer { get; set; }

        // private variables
        private Database database = new Database();
        private Common common = new Common();

        public void loadData(DateTime date, int unitId)
        {
            string sql = "select `id`, " + //0
                "`conducted_by`, " + //1
                "`presided_by`, " + //2
                "`recognitions`, " + //3
                "`opening_hymn`, " + //4
                "`first_prayer`, " + //5
                "`stake`, " + //6
                "`stake_flag`, " + //7
                "`ward`, " + //8
                "`ward_flag`, " + //9
                "`sacramental_hymn`, " + //10
                "`speaker1`, " + //11
                "`speaker2`, " + //12
                "`speaker3`, " + //13
                "`speaker4`, " + //14
                "`speaker5`, " + //15
                "`speaker1_theme`, " + //16
                "`speaker2_theme`, " + //17
                "`speaker3_theme`, " + //18
                "`speaker4_theme`, " + //19
                "`speaker5_theme`, " + //20
                "`intermediate_hymn`, " + //21
                "`pianist_by`, " + //22
                "`hymn_conducted_by`, " + //23
                "`other_subjects`, " + //24
                "`last_hymn`, " + //25
                "`last_prayer` from lds_sacramental where date = @date and unit_id = @unit_id"; //26

            foreach (List<string> data in database.retrieveData(sql, date, unitId))
            {
                id = common.convertNumber(data[0]);
                this.date = date;
                conductedBy = common.convertNumber(data[1]);
                presidedBy = common.convertNumber(data[2]);
                recognitions = data[3];
                openingHymn = common.convertNumber(data[4]);
                firstPrayer = common.convertNumber(data[5]);
                stake = data[6];
                stakeFlag = common.convertNumber(data[7]);
                ward = data[8];
                wardFlag = common.convertNumber(data[9]);
                sacramentalHymn = common.convertNumber(data[10]);
                speaker1 = common.convertNumber(data[11]);
                speaker2 = common.convertNumber(data[12]);
                speaker3 = common.convertNumber(data[13]);
                speaker4 = common.convertNumber(data[14]);
                speaker5 = common.convertNumber(data[15]);
                speaker1Theme = data[16];
                speaker2Theme = data[17];
                speaker3Theme = data[18];
                speaker4Theme = data[19];
                speaker5Theme = data[20];
                intermediateHymn = common.convertNumber(data[21]);
                pianistBy = common.convertNumber(data[22]);
                hymnConductedBy = common.convertNumber(data[23]);
                otherSubjects = data[24];
                lastHymn = common.convertNumber(data[25]);
                lastPrayer = common.convertNumber(data[26]);
            }
        }

        public Result updateOrAdd(int unitId)
        {
            if (id == 0)
                return add(unitId);
            else
                return update(unitId);
        }

        public Result update(int unitId)
        {
            string sql = "UPDATE `bakeappdb`.`lds_sacramental` " +
                "SET " +
                "`date` = @date, " +
                "`conducted_by` = @conducted_by, " +
                "`presided_by` = @presided_by, " +
                "`recognitions` = @recognitions, " +
                "`opening_hymn` = @opening_hymn, " +
                "`first_prayer` = @first_prayer, " +
                "`stake` = @stake, " +
                "`stake_flag` = @stake_flag, " +
                "`ward` = @ward, " +
                "`ward_flag` = @ward_flag, " +
                "`sacramental_hymn` = @sacramental_hymn, " +
                "`speaker1` = @speaker1, " +
                "`speaker2` = @speaker2, " +
                "`speaker3` = @speaker3, " +
                "`speaker4` = @speaker4, " +
                "`speaker5` = @speaker5, " +
                "`speaker1_theme` = @speaker1_theme, " +
                "`speaker2_theme` = @speaker2_theme, " +
                "`speaker3_theme` = @speaker3_theme, " +
                "`speaker4_theme` = @speaker4_theme, " +
                "`speaker5_theme` = @speaker5_theme, " +
                "`intermediate_hymn` = @intermediate_hymn, " +
                "`pianist_by` = @pianist_by, " +
                "`hymn_conducted_by` = @hymn_conducted_by, " +
                "`other_subjects` = @other_subjects, " +
                "`last_hymn` = @last_hymn, " +
                "`last_prayer` = @last_prayer " +
                "WHERE id = @id and unit_id = @unit_id";

            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Parameters.AddWithValue("date", date);
            cmd.Parameters.AddWithValue("conducted_by", conductedBy);
            cmd.Parameters.AddWithValue("presided_by", presidedBy);
            cmd.Parameters.AddWithValue("recognitions", recognitions);
            cmd.Parameters.AddWithValue("opening_hymn", openingHymn);
            cmd.Parameters.AddWithValue("first_prayer", firstPrayer);
            cmd.Parameters.AddWithValue("stake", stake);
            cmd.Parameters.AddWithValue("stake_flag", stakeFlag);
            cmd.Parameters.AddWithValue("ward", ward);
            cmd.Parameters.AddWithValue("ward_flag", wardFlag);
            cmd.Parameters.AddWithValue("sacramental_hymn", sacramentalHymn);
            cmd.Parameters.AddWithValue("speaker1", speaker1);
            cmd.Parameters.AddWithValue("speaker2", speaker2);
            cmd.Parameters.AddWithValue("speaker3", speaker3);
            cmd.Parameters.AddWithValue("speaker4", speaker4);
            cmd.Parameters.AddWithValue("speaker5", speaker5);
            cmd.Parameters.AddWithValue("speaker1_theme", speaker1Theme);
            cmd.Parameters.AddWithValue("speaker2_theme", speaker2Theme);
            cmd.Parameters.AddWithValue("speaker3_theme", speaker3Theme);
            cmd.Parameters.AddWithValue("speaker4_theme", speaker4Theme);
            cmd.Parameters.AddWithValue("speaker5_theme", speaker5Theme);
            cmd.Parameters.AddWithValue("intermediate_hymn", intermediateHymn);
            cmd.Parameters.AddWithValue("pianist_by", pianistBy);
            cmd.Parameters.AddWithValue("hymn_conducted_by", hymnConductedBy);
            cmd.Parameters.AddWithValue("other_subjects", otherSubjects);
            cmd.Parameters.AddWithValue("last_hymn", lastHymn);
            cmd.Parameters.AddWithValue("last_prayer", lastPrayer);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("unit_id", unitId);

            return database.executeQuery(cmd);
        }

        public Result add(int unitId)
        {
            string sql = "INSERT INTO `bakeappdb`.`lds_sacramental` " +
                "(`date`, " +
                "`conducted_by`, " +
                "`presided_by`, " +
                "`recognitions`, " +
                "`opening_hymn`, " +
                "`first_prayer`, " +
                "`stake`, " +
                "`stake_flag`, " +
                "`ward`, " +
                "`ward_flag`, " +
                "`sacramental_hymn`, " +
                "`speaker1`, " +
                "`speaker2`, " +
                "`speaker3`, " +
                "`speaker4`, " +
                "`speaker5`, " +
                "`speaker1_theme`, " +
                "`speaker2_theme`, " +
                "`speaker3_theme`, " +
                "`speaker4_theme`, " +
                "`speaker5_theme`, " +
                "`intermediate_hymn`, " +
                "`pianist_by`, " +
                "`hymn_conducted_by`, " +
                "`other_subjects`, " +
                "`last_hymn`, " +
                "`last_prayer`, unit_id) " +
                "VALUES " +
                "(@date, " +
                "@conducted_by, " +
                "@presided_by, " +
                "@recognitions, " +
                "@opening_hymn, " +
                "@first_prayer, " +
                "@stake, " +
                "@stake_flag, " +
                "@ward, " +
                "@ward_flag, " +
                "@sacramental_hymn, " +
                "@speaker1, " +
                "@speaker2, " +
                "@speaker3, " +
                "@speaker4, " +
                "@speaker5, " +
                "@speaker1_theme, " +
                "@speaker2_theme, " +
                "@speaker3_theme, " +
                "@speaker4_theme, " +
                "@speaker5_theme, " +
                "@intermediate_hymn, " +
                "@pianist_by, " +
                "@hymn_conducted_by, " +
                "@other_subjects, " +
                "@last_hymn, " +
                "@last_prayer, @unit_id)";

            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.Parameters.AddWithValue("date", date);
            cmd.Parameters.AddWithValue("conducted_by", conductedBy);
            cmd.Parameters.AddWithValue("presided_by", presidedBy);
            cmd.Parameters.AddWithValue("recognitions", recognitions);
            cmd.Parameters.AddWithValue("opening_hymn", openingHymn);
            cmd.Parameters.AddWithValue("first_prayer", firstPrayer);
            cmd.Parameters.AddWithValue("stake", stake);
            cmd.Parameters.AddWithValue("stake_flag", stakeFlag);
            cmd.Parameters.AddWithValue("ward", ward);
            cmd.Parameters.AddWithValue("ward_flag", wardFlag);
            cmd.Parameters.AddWithValue("sacramental_hymn", sacramentalHymn);
            cmd.Parameters.AddWithValue("speaker1", speaker1);
            cmd.Parameters.AddWithValue("speaker2", speaker2);
            cmd.Parameters.AddWithValue("speaker3", speaker3);
            cmd.Parameters.AddWithValue("speaker4", speaker4);
            cmd.Parameters.AddWithValue("speaker5", speaker5);
            cmd.Parameters.AddWithValue("speaker1_theme", speaker1Theme);
            cmd.Parameters.AddWithValue("speaker2_theme", speaker2Theme);
            cmd.Parameters.AddWithValue("speaker3_theme", speaker3Theme);
            cmd.Parameters.AddWithValue("speaker4_theme", speaker4Theme);
            cmd.Parameters.AddWithValue("speaker5_theme", speaker5Theme);
            cmd.Parameters.AddWithValue("intermediate_hymn", intermediateHymn);
            cmd.Parameters.AddWithValue("pianist_by", pianistBy);
            cmd.Parameters.AddWithValue("hymn_conducted_by", hymnConductedBy);
            cmd.Parameters.AddWithValue("other_subjects", otherSubjects);
            cmd.Parameters.AddWithValue("last_hymn", lastHymn);
            cmd.Parameters.AddWithValue("last_prayer", lastPrayer);
            cmd.Parameters.AddWithValue("unit_id", unitId);

            return database.executeQuery(cmd);
        }
    }
}