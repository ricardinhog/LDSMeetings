using OrganizadorReuniao.Helper;
using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class MemberController : BaseController
    {
        public ActionResult Index()
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
                return View();
        }

        [HttpPost]
        public ActionResult Index(MemberViewModel model, string gender, string unitMember, string priesthood)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                if (ModelState.IsValid)
                {
                    //new Common().convertDate(date.Replace("/", "-")
                    Result result = new Member().addMember(model.FirstName, model.LastName, model.BirthDate, gender, new Common().convertBool(unitMember), priesthood);
                    if (result.Success)
                        return RedirectToAction("Success");
                    else
                        ModelState.AddModelError("", "Ocorreu um erro ao tentar cadastrar novo membro");
                }
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                Member member = new Member().getMember(id);
                MemberViewModel model = new MemberViewModel();
                model.Id = member.Id;
                model.BirthDate = member.BirthDate;
                model.FirstName = member.FirstName;
                model.Gender = member.Gender;
                model.IsUnitMember = (member.isUnitMember) ? "1" : "0";
                model.LastName = member.LastName;
                model.PriesthoodOption = member.priesthood.ToString();

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(MemberViewModel model, string gender, string unitMember, string priesthood)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                if (ModelState.IsValid)
                {
                    //new Common().convertDate(date.Replace("/", "-")
                    Result result = new Member().updateMember(model.Id, model.FirstName, model.LastName, model.BirthDate, gender, new Common().convertBool(unitMember), priesthood);
                    if (result.Success)
                        return RedirectToAction("Success");
                    else
                        ModelState.AddModelError("", "Ocorreu um erro ao tentar editar informação do membro");
                }
                return View(model);
            }
        }

        public ActionResult Success()
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
                return View();
        }
    }
}
