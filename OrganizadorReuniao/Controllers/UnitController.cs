using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class UnitController : BaseController
    {
        public ActionResult Create()
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(UnitViewModel model)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
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
        }

        public ActionResult Success()
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
