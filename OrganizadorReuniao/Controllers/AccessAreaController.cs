using OrganizadorReuniao.Helper;
using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class AccessAreaController : BaseController
    {
        //
        // GET: /AccessArea/

        public ActionResult Index()
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                ViewBag.userId = loggedUser.Id;
                return View(new User().getWardUsers(loggedUser.Unit));
            }
        }

        public ActionResult New()
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult New(AccessViewModel model)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                if (ModelState.IsValid)
                {
                    if (!new User().emailExists(model.Email))
                    {
                        Common common = new Common();
                        string newPassword = common.generatePassword(6);
                        bool emailSent = common.sendEmail(model.Email, string.Format("Seu acesso foi criado no Agenda SUD\n\nEmail: {0}\nSenha: {1}\n\nAgenda SUD", model.Email, newPassword), "Agenda SUD - Acesso Criado");
                        Result result = new User().addUser(model.Email, newPassword, loggedUser.Unit);

                        if (emailSent && result.Success)
                            return RedirectToAction("Index");
                        else
                        {
                            ModelState.AddModelError("", "Ocorreu um erro ao criar novo acesso");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Endereço de email já está sendo utilizado por outro usuário");
                    }
                }
                return View(model);
            }
        }

        public ActionResult Feedback(string message)
        {
            ViewBag.message = message;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                new User().deleteUser(id);
                return RedirectToAction("Index");
            }
        }
    }
}
