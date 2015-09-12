using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace OrganizadorReuniao.Controllers
{
    public class ReportController : BaseController
    {
        //
        // GET: /Report/

        public ActionResult Index()
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                ViewBag.option = (int)Member.memberType.elder;
                return View(new Report().getReport(Member.memberType.elder, DateTime.Now.Year));
            }
        }

        [HttpPost]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Index(int reportId, int year)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                ViewBag.option = reportId;
                return View(new Report().getReport((Member.memberType)reportId, year));
            }
        }
    }
}
