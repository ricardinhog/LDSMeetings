using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizadorReuniao.Models
{
    public class Result
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public Exception ErrorDetails { get; set; }

        public Result(bool success)
        {
            Success = success;
        }
    }
}