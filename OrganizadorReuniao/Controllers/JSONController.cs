using OrganizadorReuniao.Helper;
using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class JSONController : BaseController
    {
        public ActionResult SearchMember(string keyword)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                Member member = new Member();
                return Json(member.getMembers(keyword, loggedUser.Unit), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SearchMembers(int typeId, string date)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                Member member = new Member();
                return Json(member.getMembers((Member.memberType)typeId, loggedUser.Unit, new Common().convertDate(date.Replace("/", "-"), true)), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SearchMusic(string keyword)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                return Json(new Hymn().getHymns(keyword), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveData(int member, bool present, int type, string date)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                return Json(new Frequency().setPresent(
                    member, new Common().convertDate(date.Replace("/", "-"), true), (Member.memberType)type, present),
                    JsonRequestBehavior.AllowGet);
            }
        }
    }
}
