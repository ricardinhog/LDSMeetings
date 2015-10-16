using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class BaseController : Controller
    {
        protected bool isAuthenticated()
        {
            return !(Session["user"] == null);
        }

        public User loggedUser
        {
            get
            {
                if (!isAuthenticated())
                {
                    return null;
                }
                else
                {
                    return (User)Session["user"];
                }
            }
        }
    }
}
