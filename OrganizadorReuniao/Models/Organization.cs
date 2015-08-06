using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Calling> Callings { get; set; }

        public List<Organization> getAll()
        {
            return new List<Organization>();
        }

        public List<Organization> getOrganization(int id)
        {
            return new List<Organization>();
        }
    }
}