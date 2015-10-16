using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class FrequencyController : BaseController
    {
        public ActionResult Index(int frequencyType)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                return View();
            }
        }

    }
}
