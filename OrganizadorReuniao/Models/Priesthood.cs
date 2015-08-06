using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Priesthood
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Priesthood> getAll()
        {
            return new List<Priesthood>();
        }

        public Priesthood getPriesthood(int id)
        {
            return new Priesthood();
        }
    }
}