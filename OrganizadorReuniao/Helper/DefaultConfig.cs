using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Helper
{
    public class DefaultConfig
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            }
        }

        public string getAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}