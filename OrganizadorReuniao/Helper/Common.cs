using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OrganizadorReuniao.Helper
{
    public class Common
    {
        private DefaultConfig config = new DefaultConfig();

        public bool convertBool(int flag)
        {
            return (flag == 1);
        }

        public int convertBool(bool flag)
        {
            return (flag) ? 1 : 0;
        }

        public DateTime convertDate(string datetime)
        {
            string[] chunks = datetime.Split(' ');
            string[] date = chunks[0].Split('-');
            string[] time = chunks[1].Split(':');
            return new DateTime(
                convertNumber(date[0]), convertNumber(date[1]), convertNumber(date[2]), 
                convertNumber(time[0]), convertNumber(time[1]), convertNumber(time[2]));
        }

        public string convertDate(DateTime dateTime)
        {
            return string.Format("{0}-{1}-{2} {3}:{4}:{5}", 
                dateTime.Year, dateTime.Month.ToString().PadLeft(2, '0'), dateTime.Day.ToString().PadLeft(2, '0'),
                dateTime.Hour.ToString().PadLeft(2, '0'), dateTime.Minute.ToString().PadLeft(2, '0'), dateTime.Second.ToString().PadLeft(2, '0'));
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