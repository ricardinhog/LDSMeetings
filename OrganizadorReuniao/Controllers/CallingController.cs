using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class CallingController : Controller
    {
        public ActionResult Create()
        {
            Calling calling = new Calling();
            ViewBag.callings = calling.getAll();

            return View();
        }

    }
}
