using OrganizadorReuniao.Helper;
using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class BatchCommandController : BaseController
    {
        //
        // GET: /BatchCommand/

        public ActionResult Index()
        {
            if (!isAuthenticated() || !loggedUser.isAdmin)
                return new HttpUnauthorizedResult();
            else
            {
                return View();
            }
        }

        public ActionResult Success()
        {
            if (!isAuthenticated() || !loggedUser.isAdmin)
                return new HttpUnauthorizedResult();
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (!isAuthenticated() || !loggedUser.isAdmin)
                return new HttpUnauthorizedResult();
            else
            {
                if (file != null && file.ContentLength > 0 && file.ContentType == "text/plain")
                {
                    var fileName = new Common().generatePassword(20) + DateTime.Now.Ticks;
                    var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    file.SaveAs(path);

                    Result result = new Result(false);

                    using (var reader = new StreamReader(path))
                    {
                        result = new BatchCommand().execute(reader, loggedUser.Unit);
                    }

                    System.IO.File.Delete(path);

                    if (result.Success)
                    {
                        return RedirectToAction("Success");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Erro ao ler arquivo. Por favor verifique o arquivo novamente");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Erro ao ler arquivo. Por favor verifique o arquivo novamente");
                }
                return View();
            }
        }
    }
}
