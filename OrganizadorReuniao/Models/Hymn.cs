using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Hymn
    {
        public int Number { get; set; }
        public string Name { get; set; }

        // private variables
        private Database database = new Database();

        public List<Hymn> getAll()
        {
            List<Hymn> hymns = new List<Hymn>();
            foreach (List<string> data in database.retrieveData("SELECT id, name " +
                "FROM bakeappdb.lds_hymn  " +
                " order by name, id"))
            {
                Hymn hymn = new Hymn();
                hymn.Number = Convert.ToInt32(data[0]);
                hymn.Name = data[1];
                hymns.Add(hymn);
            }
            return hymns;
        }

        public Hymn getHymn(int id)
        {
            return new Hymn();
        }

        public List<Hymn> getHymns(string keyword)
        {
            keyword = "%" + keyword + "%";
            List<Hymn> hymns = new List<Hymn>();
            foreach (List<string> data in database.retrieveData("SELECT id, name " +
                "FROM bakeappdb.lds_hymn  " +
                "where name like @name " +
                "   or id like @id " +
                "order by name, id", keyword, keyword))
            {
                Hymn hymn = new Hymn();
                hymn.Number = Convert.ToInt32(data[0]);
                hymn.Name = data[1];
                hymns.Add(hymn);
            }
            return hymns;
        }
    }
}