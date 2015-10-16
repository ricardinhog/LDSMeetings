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
    }
}