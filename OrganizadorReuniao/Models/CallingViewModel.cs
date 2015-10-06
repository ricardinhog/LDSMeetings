using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class CallingViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Chamado")]
        public int CallingId { get; set; }

        [Display(Name = "Membro")]
        public int MemberId { get; set; }

        [Display(Name = "Outro")]
        public string Other { get; set; }

        [Display(Name = "Data do chamado")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Data do chamado é requerida")]
        public DateTime Date { get; set; }

        [Display(Name = "É um chamado?")]
        public int CallingFlag { get; set; }
    }
}