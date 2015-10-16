using OrganizadorReuniao.Languages;
using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class AccountController : BaseController
    {
        /// <summary>
        /// Create a new account
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Models.User newUser = new Models.User();
                
                // check if the email address is already being used
                if (!newUser.emailExists(model.Email))
                {
                    Result result = newUser.addUser(model.Email, model.Password);

                    if (result.Success)
                        result = new Unit().addUnit(model.UnitName, result.Id);

                    if (result.Success)
                        return RedirectToAction("Success");
                }
                else
                {
                    ModelState.AddModelError("", pt_br.EmailAlreadyBeingUsed);
                }
            }
            return View(model);
        }

        /// <summary>
        /// Success message when the account was created
        /// </summary>
        /// <returns></returns>
        public ActionResult Success()
        {
            return View();
        }

        /// <summary>
        /// Login page
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            if (Request.QueryString["ReturnUrl"] != null)
                ViewBag.url = Request.QueryString["ReturnUrl"];
            return View();

        }

        [HttpPost]
        public ActionResult Login(UserViewModel model, string url)
        {
            // remove validation errors
            foreach (var key in ModelState.Keys.ToList())
                ModelState[key].Errors.Clear(); 

            if (model.Email != string.Empty && model.Password != string.Empty)
            {
                Models.User checkUser = new Models.User();
                checkUser = checkUser.getUser(model.Email, model.Password);

                if (checkUser.Id != 0)
                {
                    Session["user"] = checkUser;
                    if (url == "")
                        return RedirectToAction("Index", "Home");
                    else
                        return RedirectPermanent(url);
                }
                else
                {
                    ModelState.AddModelError("", "Usuário não encontrado");
                }
            }
            else
            {
                ModelState.AddModelError("", "Favor preencher todos os campos");
            }
            return View(model);
        }

        /// <summary>
        /// Logout page
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Remove("user");
            return View();
        }


        /// <summary>
        /// Update profile
        /// </summary>
        /// <returns></returns>
        public ActionResult Update()
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                Models.User user = (Models.User)Session["user"];
                Models.UserViewModel model = new UserViewModel();
                model.Email = user.Email;
                model.Password = user.Password;
                model.Confirm = user.Password;
                model.Id = user.Id;
                model.UnitName = user.UnitName;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Update(UserViewModel model)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                if (ModelState.IsValid)
                {
                    if (model.Password != string.Empty && model.Email != string.Empty)
                    {
                        Models.User user = new Models.User();
                        Result result = user.updateUser(model.Id, model.Email, model.Password);

                        if (result.Success)
                            result = new Unit().updateUnit(model.UnitName, model.Id);

                        if (result.Success)
                        {
                            Session["user"] = new User().getUser(model.Id);
                            return RedirectToAction("UpdateSuccess");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Os campos email e senha precisam ser preenchidos");
                    }
                }
                return View(model);
            }
        }

        public ActionResult UpdateSuccess()
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
