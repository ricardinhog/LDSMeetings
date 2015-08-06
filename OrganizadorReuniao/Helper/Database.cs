using MySql.Data.MySqlClient;
using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Helper
{
    public class Database
    {
        /// <summary>
        /// Insert, update or delete data on the database
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public Result executeQuery(MySqlCommand cmd)
        {
            MySqlConnection cn = new MySqlConnection(DefaultConfig.ConnectionString);
            Result result = new Result(true);

            try
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", cn);
                result.Id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                result = new Result(false);
                result.ErrorDetails = ex;
                Log.error(ex);
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Retrieve data from database
        /// </summary>
        /// <param name="SQL">Select Command</param>
        /// <param name="parameters">Parameters if any</param>
        /// <returns>List of data</returns>
        public List<List<string>> retrieveData(string SQL, params object[] parameters)
        {
            List<List<string>> result = new List<List<string>>();
            MySqlConnection cn = null;
            try
            {
                cn = new MySqlConnection(DefaultConfig.ConnectionString);
                MySqlCommand cmd = new MySqlCommand(SQL, cn);
                if (parameters != null && parameters.Count() > 0)
                {
                    List<string> parametersNames = getParametersNames(SQL);
                    for (int i = 0; i < parameters.Count(); i++)
                        cmd.Parameters.AddWithValue(parametersNames[i], parameters[i]);
                }
                MySqlDataReader dr;

                cn.Open();

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    List<string> row = new List<string>();
                    for (int i = 0; i < dr.FieldCount; i++)
                        row.Add(dr[i].ToString());
                    result.Add(row);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Log.error(ex);
                result = null;
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Separate the parameters from the rest of the query
        /// </summary>
        /// <param name="SQL">SQL query</param>
        /// <returns>List of parameters</returns>
        private static List<string> getParametersNames(string SQL)
        {
            List<string> list = SQL.Split('@').ToList();
            for (int i = 1; i < list.Count; i++)
            {
                list[i] = list[i].Replace(',', ' ').Replace(')', ' ');
                if (list[i].IndexOf(' ') > -1)
                    list[i] = "@" + list[i].Substring(0, list[i].IndexOf(' '));
            }
            if (list.Count > 0)
                list.RemoveAt(0);
            return list;
        }
    }
}