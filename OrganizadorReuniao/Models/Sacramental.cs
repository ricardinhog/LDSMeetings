using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Sacramental
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }
        public string PresidedBy { get; set; }
        public string DirectedBy { get; set; }
        public string Mentions { get; set; }
        public Hymn FirstHymn { get; set; }
        public Hymn LastHymn { get; set; }
        public Hymn IntermidiateHymn { get; set; }
        public Hymn SacramentalHymn { get; set; }
        public Member Conductor { get; set; }
        public Member Pianist { get; set; }
        public Member FirstPrayer { get; set; }
        public Member LastPrayer { get; set; }
        public string Confirmation { get; set; }
        public string ChildBlessing { get; set; }
        public string Obs { get; set; }
        public List<Speaker> Speakers { get; set; }
    }
}