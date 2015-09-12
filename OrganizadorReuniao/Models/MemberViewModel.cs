using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class MemberViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Primeiro nome")]
        [Required(ErrorMessage="Primeiro nome é requerido")]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "Sobrenome é requerido")]
        public string LastName { get; set; }

        [Display(Name="Data de nascimento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage="Data de nascimento é requerida")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Gênero")]
        public string Gender { get; set; }

        [Display(Name = "Membro da unidade?")]
        public string IsUnitMember { get; set; }
    }
}