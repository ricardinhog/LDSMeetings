using MySql.Data.MySqlClient;
using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public string Obs { get; set; }

        // private variables
        private Database database = new Database();
        private Common common = new Common();

        public Result add(string name, DateTime date, string place, string obs, int unitId)
        {
            MySqlCommand cmd = new MySqlCommand("insert into lds_activity (name, scheduled_by, place, obs, unit_id) values (@name, @scheduled_by, @place, @obs, @unit_id)");
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("scheduled_by", date);
            cmd.Parameters.AddWithValue("place", place);
            cmd.Parameters.AddWithValue("obs", obs);
            cmd.Parameters.AddWithValue("unit_id", unitId);

            return database.executeQuery(cmd);
        }

        public Result update(int id, string name, DateTime date, string place, string obs, int unitId)
        {
            MySqlCommand cmd = new MySqlCommand("update lds_activity set name = @name, scheduled_by = @scheduled_by, place = @place, obs = @obs, unit_id = 1 where id = @id");
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("scheduled_by", date);
            cmd.Parameters.AddWithValue("place", place);
            cmd.Parameters.AddWithValue("obs", obs);
            cmd.Parameters.AddWithValue("unit_id", unitId);
            cmd.Parameters.AddWithValue("id", id);

            return database.executeQuery(cmd);
        }

        public Result delete(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from lds_activity where id = @id");
            cmd.Parameters.AddWithValue("id", id);

            return database.executeQuery(cmd);
        }

        public Result deleteOld()
        {
            MySqlCommand cmd = new MySqlCommand("delete from lds_activity where scheduled_by < now()");
            return database.executeQuery(cmd);
        }

        public List<Activity> getAll()
        {
            List<Activity> list = new List<Activity>();
            foreach (List<string> data in database.retrieveData("select id, name, " + common.formatDate("scheduled_by") + ", place, obs from lds_activity order by scheduled_by asc"))
            {
                Activity activity = new Activity();
                activity.Id = common.convertNumber(data[0]);
                activity.Name = data[1];
                activity.Date = common.convertDate(data[2]);
                activity.Place = data[3];
                activity.Obs = data[4];
                list.Add(activity);
            }
            return list;
        }

        public List<Activity> getNextNMonths(int nInterval)
        {
            List<Activity> list = new List<Activity>();
            foreach (List<string> data in database.retrieveData("select id, name, " + common.formatDate("scheduled_by") + ", place, obs from lds_activity where scheduled_by > now() and scheduled_by < DATE_ADD(now(), INTERVAL " + nInterval + " MONTH) order by scheduled_by asc"))
            {
                Activity activity = new Activity();
                activity.Id = common.convertNumber(data[0]);
                activity.Name = data[1];
                activity.Date = common.convertDate(data[2]);
                activity.Place = data[3];
                activity.Obs = data[4];
                list.Add(activity);
            }
            return list;
        }

        public Activity get(int id)
        {
            Activity activity = null;
            foreach (List<string> data in database.retrieveData("select id, name, " + common.formatDate("scheduled_by") + ", place, obs from lds_activity where id = @id", id))
            {
                activity = new Activity();
                activity.Id = common.convertNumber(data[0]);
                activity.Name = data[1];
                activity.Date = common.convertDate(data[2]);
                activity.Place = data[3];
                activity.Obs = data[4];
            }
            return activity;
        }
    }
}