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

        public enum referenceType
        {
            deacon = 1,
            teacher = 2,
            priest = 3,
            elder = 4,
            highPriest = 5
        }

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