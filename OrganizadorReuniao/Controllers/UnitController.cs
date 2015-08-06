using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class UnitController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UnitViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = (User)Session["user"];

                Unit unit = new Unit();
                Result result = unit.addUnit(model.Name, model.Phone, model.Email, user.Id, model.Number);

                if (result.Success)
                    return RedirectToAction("Success");
            }
            return View(model);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
