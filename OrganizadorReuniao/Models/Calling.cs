using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Calling
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // private variables
        private Database database = new Database();

        public List<Calling> getAll()
        {
            List<Calling> callings = new List<Calling>();
            foreach (List<string> data in database.retrieveData("select c.id, o.name, c.name from lds_calling c, lds_organization o " +
                "where c.organization_id = o.id " +
                "order by o.name, c.name"))
            {
                Calling calling = new Calling();
                calling.Name = string.Format("{0} - {1}", data[1], data[2]);
                calling.Id = Convert.ToInt32(data[0]);
                callings.Add(calling);
            }
            return callings;
        }

        public List<Calling> getPerOrganization(int organizationId)
        {
            return new List<Calling>();
        }

        public Calling getCalling(int id)
        {
            return new Calling();
        }
    }
}