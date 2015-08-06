using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class UnitViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Número de unidade obrigatório")]
        [Display(Name = "Número unidade")]
        [RegularExpression(@"^\d+$", ErrorMessage="Somente números")]
        public int Number { get; set; }
    }
}