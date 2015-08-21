using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class JSONController : Controller
    {
        public ActionResult SearchMember(string keyword)
        {
            Models.User user = (Models.User)Session["user"];

            Member member = new Member();
            return Json(member.getMembers(keyword, user.Units[0].Id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchMembers(int typeId)
        {
            Models.User user = (Models.User)Session["user"];
            Member member = new Member();
            return Json(member.getMembers((Member.memberType)typeId, 1), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchMusic(string keyword)
        {
            return Json(new Hymn().getHymns(keyword), JsonRequestBehavior.AllowGet);
        }
    }
}
