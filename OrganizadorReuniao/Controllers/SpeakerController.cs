using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class SpeakerController : BaseController
    {
        //
        // GET: /Speaker/

        public ActionResult Index()
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                return View(new Speaker().getAll());
            }
        }

    }
}
