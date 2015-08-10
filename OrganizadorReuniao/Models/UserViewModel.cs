using OrganizadorReuniao.Languages;
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

        [Required(ErrorMessageResourceType = typeof(pt_br), ErrorMessageResourceName = "EmailRequiredMessage")]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(pt_br), ErrorMessageResourceName = "EmailInvalidMessage")]
        [Display(ResourceType = typeof(pt_br), Name = "EmailField")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(pt_br), ErrorMessageResourceName = "PasswordRequiredMessage")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(pt_br), Name = "PasswordField")]
        [MinLength(6, ErrorMessageResourceType = typeof(pt_br), ErrorMessageResourceName = "PasswordAtLeast6characteres")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(pt_br), ErrorMessageResourceName = "ConfirmPasswordRequiredMessage")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(pt_br), Name = "ConfirmPasswordField")]
        [Compare("Password", ErrorMessageResourceType = typeof(pt_br), ErrorMessageResourceName = "PasswordDoesNotMatch")]
        public string Confirm { get; set; }
    }
}