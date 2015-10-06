using OrganizadorReuniao.Helper;
using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class CallingController : BaseController
    {
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                new Calling().delete(id);
                return RedirectToAction("Index");
            }
        }

        public ActionResult New()
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                CallingViewModel model = new CallingViewModel();
                model.Date = DateTime.Now;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult New(CallingViewModel model, int member, int calling, int callingFlag)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                if (ModelState.IsValid)
                {
                    Calling newCalling = new Calling();
                    Result result = newCalling.add(calling, member, string.Empty, model.Date, new Common().convertBool(callingFlag));
                    if (result.Success)
                        return RedirectToAction("Index");
                    else
                        ModelState.AddModelError("", "Ocorreu um erro ao criar novo chamado");
                }

                return View(model);
            }
        }

        public ActionResult Index()
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                return View(new Calling().getAllCallings());
            }
        }


        public ActionResult Edit(int id)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                CallingViewModel model = new CallingViewModel();
                Calling call = new Calling().get(id);
                model.CallingId = call.CallingId;
                model.MemberId = call.MemberId;
                model.CallingFlag = new Common().convertBool(call.CallingFlag);
                model.Date = call.Date;
                model.Id = call.Id;
                model.Other = call.Other;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(CallingViewModel model, int member, int calling, int callingFlag)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                if (ModelState.IsValid)
                {
                    Calling call = new Calling();
                    Result result = call.update(model.Id, calling, member, string.Empty, model.Date, new Common().convertBool(callingFlag));
                    if (result.Success)
                        return RedirectToAction("Index");
                    else
                        ModelState.AddModelError("", "Ocorreu um erro ao atualizar chamado/desobrigação");
                }

                return View(model);
            }
        }
    }
}
