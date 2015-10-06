using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OrganizadorReuniao.Helper
{
    public class Common
    {
        private DefaultConfig config = new DefaultConfig();

        public void sendEmail(string email, string message)
        {

        }

        public List<Member> parseFile(string fileContent)
        {
            List<Member> members = new List<Member>();
            foreach (string line in fileContent.Replace("/r/n", "|").Split('|'))
            {
                string[] memberData = line.Split(',');
                if (memberData.Length > 2)
                {
                    Member member = new Member();
                    member.LastName = memberData[0].Split('-')[0];
                    member.FirstName = memberData[0].Split('-')[1];
                    member.Gender = memberData[1];
                    int day = Convert.ToInt32(memberData[2].Split('/')[0]);
                    int month = Convert.ToInt32(memberData[2].Split('/')[1]);
                    int year = Convert.ToInt32(memberData[2].Split('/')[2]);
                    member.BirthDate = new DateTime(year, month, day);
                    members.Add(member);
                }
            }
            return members;
        }

        public string readResourceValue(string key)
        {
            string resourceValue = key;
            try
            {
                resourceValue = Languages.pt_br.ResourceManager.GetString(key);
            }
            catch (Exception ex)
            {
                Log.error(ex);
                resourceValue = key;
            }
            return resourceValue;
        }

        public string formatDate(string fieldName)
        {
            return string.Format("date_format({0},'%Y-%m-%d %H:%i:%s')", fieldName);
        }

        public bool convertBool(int flag)
        {
            return (flag == 1);
        }

        public string convertZeroNull(int number)
        {
            if (number <= 0)
                return "null";
            else
                return number.ToString();
        }

        public bool convertBool(string flag)
        {
            return convertBool(Convert.ToInt32(flag));
        }

        public int convertBool(bool flag)
        {
            return (flag) ? 1 : 0;
        }

        public int sundaysPerMonth(int month, int year)
        {
            DateTime date = new DateTime(year, month, 1);
            int count = 0;
            while (date.Month == month)
            {
                if (date.DayOfWeek == DayOfWeek.Sunday)
                    count++;
                date = date.AddDays(1);
            }
            return count;
        }

        public DateTime convertDate(string datetime, bool onlyDate = false)
        {
            if (datetime != "")
            {
                int count = datetime.Split('-')[0].Length;

                if (onlyDate)
                {
                    string[] date = datetime.Split('-');
                    if (count == 2)
                    {
                        return new DateTime(convertNumber(date[2]), convertNumber(date[1]), convertNumber(date[0]));
                    }
                    else
                    {
                        return new DateTime(convertNumber(date[0]), convertNumber(date[1]), convertNumber(date[2]));
                    }
                }
                else
                {
                    string[] chunks = datetime.Split(' ');
                    string[] date = chunks[0].Split('-');
                    string[] time = chunks[1].Split(':');
                    return new DateTime(
                        convertNumber(date[0]), convertNumber(date[1]), convertNumber(date[2]),
                        convertNumber(time[0]), convertNumber(time[1]), convertNumber(time[2]));
                }
            }
            else
            {
                return new DateTime();
            }
        }

        public DateTime getLastSundayDate()
        {
            int weekDay = (int)DateTime.Now.DayOfWeek;
            if (weekDay == 0)
                weekDay = 7;
            return DateTime.Now.AddDays(-weekDay).Date;
        }

        public string convertDate(DateTime dateTime, bool onlyDate = false)
        {
            if (onlyDate)
            {
                return string.Format("{0}-{1}-{2}",
                    dateTime.Year, dateTime.Month.ToString().PadLeft(2, '0'), dateTime.Day.ToString().PadLeft(2, '0'));
            }
            else
            {
                return string.Format("{0}-{1}-{2} {3}:{4}:{5}",
                    dateTime.Year, dateTime.Month.ToString().PadLeft(2, '0'), dateTime.Day.ToString().PadLeft(2, '0'),
                    dateTime.Hour.ToString().PadLeft(2, '0'), dateTime.Minute.ToString().PadLeft(2, '0'), dateTime.Second.ToString().PadLeft(2, '0'));
            }
        }

        public int convertNumber(string number)
        {
            int convertedNum = 0;
            try
            {
                convertedNum = Convert.ToInt32(number);
            }
            catch
            {
                convertedNum = 0;
            }
            return convertedNum;
        }

        public string hash(string text)
        {
            StringBuilder sBuilder = new StringBuilder();
            try
            {
                string password = string.Format("{0}.{1}:{2}",
                    config.getAppSetting("passwordEnhancerText1"),
                    text,
                    config.getAppSetting("passwordEnhancerText2"));

                MD5 md5Hasher = MD5.Create();
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(password));

                for (int i = 0; i < data.Length; i++)
                    sBuilder.Append(data[i].ToString("x2"));
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
            return sBuilder.ToString();
        }

        public bool verifyHash(string text, string hashToCompare)
        {
            string textHash = hash(text);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return (0 == comparer.Compare(textHash, hashToCompare));
        }
    }
}