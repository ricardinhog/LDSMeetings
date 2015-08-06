using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Speaker
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string InvitedSpeaker { get; set; }
        public Topic topic { get; set; }
        public int Position { get; set; }
        public int Time { get; set; }
    }
}