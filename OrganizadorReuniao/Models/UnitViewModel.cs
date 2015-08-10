using OrganizadorReuniao.Languages;
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

        [Required(ErrorMessageResourceType = typeof(pt_br), ErrorMessageResourceName = "NameRequiredMessage")]
        [Display(ResourceType = typeof(pt_br), Name = "NameField")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(pt_br), Name = "PhoneField")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(pt_br), ErrorMessageResourceName = "EmailRequiredMessage")]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(pt_br), ErrorMessageResourceName = "EmailInvalidMessage")]
        [Display(ResourceType = typeof(pt_br), Name = "EmailField")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(pt_br), ErrorMessageResourceName = "UnitNumberRequiredMessage")]
        [Display(ResourceType = typeof(pt_br), Name = "UnitNumberField")]
        [RegularExpression(@"^\d+$", ErrorMessageResourceType = typeof(pt_br), ErrorMessageResourceName = "OnlyNumbersAllowed")]
        public int Number { get; set; }
    }
}