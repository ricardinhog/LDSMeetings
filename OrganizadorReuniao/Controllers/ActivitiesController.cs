using OrganizadorReuniao.Helper;
using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class ActivitiesController : Controller
    {
        //
        // GET: /Activities/

        public ActionResult Index()
        {
            new Activity().deleteOld();
            return View(new Activity().getAll());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            new Activity().delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ActivityViewModel model = new ActivityViewModel();
            Activity activity = new Activity().get(id);
            model.Id = activity.Id;
            model.Name = activity.Name;
            model.Date = activity.Date;
            model.Place = activity.Place;
            model.Obs = activity.Obs;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ActivityViewModel model, string hour, string minute)
        {
            if (ModelState.IsValid)
            {
                Activity activity = new Activity();
                Result result = activity.update(model.Id, model.Name,
                    new DateTime(model.Date.Year, model.Date.Month, model.Date.Day, new Common().convertNumber(hour), new Common().convertNumber(minute), 0),
                    model.Place,
                    model.Obs,
                    1);
                if (result.Success)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Ocorreu um erro ao atualizar atividade");
            }

            return View(model);
        }

        public ActionResult New()
        {
            ActivityViewModel model = new ActivityViewModel();
            model.Date = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public ActionResult New(ActivityViewModel model, string hour, string minute)
        {
            if (ModelState.IsValid)
            {
                Activity newActivity = new Activity();
                Result result = newActivity.add(model.Name,
                    new DateTime(model.Date.Year, model.Date.Month, model.Date.Day, new Common().convertNumber(hour), new Common().convertNumber(minute), 0),
                    model.Place,
                    model.Obs,
                    1);
                if (result.Success)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Ocorreu um erro ao criar nova atividade");
            }

            return View(model);
        }
    }
}
