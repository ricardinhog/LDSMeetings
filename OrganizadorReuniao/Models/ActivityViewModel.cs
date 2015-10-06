using MySql.Data.MySqlClient;
using OrganizadorReuniao.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class ActivityViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Evento")]
        [Required(ErrorMessage = "Nome do evento é requerido")]
        public string Name { get; set; }

        [Display(Name = "Data da atividade")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Data da atividade é requerida")]
        public DateTime Date { get; set; }

        [Display(Name = "Local")]
        [Required(ErrorMessage = "Local é requerido")]
        public string Place { get; set; }

        [Display(Name = "Obs")]
        public string Obs { get; set; }
    }
}