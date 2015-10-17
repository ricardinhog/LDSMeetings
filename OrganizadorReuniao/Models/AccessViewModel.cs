using OrganizadorReuniao.Languages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class AccessViewModel
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(pt_br), ErrorMessageResourceName = "EmailInvalidMessage")]
        [Required(ErrorMessage = "Email é requerido")]
        public string Email { get; set; }
    }
}