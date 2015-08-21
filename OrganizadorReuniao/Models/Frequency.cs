using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Frequency
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private Database database = new Database();
        private Common common = new Common();

        public List<Frequency> getTypes()
        {
            List<Frequency> frequencies = new List<Frequency>();
            foreach (List<string> data in database.retrieveData("SELECT id, name from lds_frequency_type"))
            {
                Frequency frequency = new Frequency();
                frequency.Id = common.convertNumber(data[0]);
                frequency.Name = getDescription(data[1]);
                frequencies.Add(frequency);
            }
            frequencies = frequencies.OrderBy(f => f.Name).ToList();
            return frequencies;
        }

        private string getDescription(string key)
        {
            return common.readResourceValue(key);
        }
    }
}