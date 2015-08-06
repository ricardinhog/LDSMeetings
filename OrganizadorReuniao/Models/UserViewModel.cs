using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Email obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage="Email inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha obrigatória")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [MinLength(6, ErrorMessage="A senha deve possuir pelo menos 6 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmar senha é obrigatório")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage="Confirmação de senha não confere")]
        public string Confirm { get; set; }
    }
}