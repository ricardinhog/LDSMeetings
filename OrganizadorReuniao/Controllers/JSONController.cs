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
            Models.User user = (Models.User)Session["user"];

            Member member = new Member();
            return Json(member.getMembers(keyword, user.Units[0].Id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchMembers(int typeId, string date)
        {
            Models.User user = (Models.User)Session["user"];
            Member member = new Member();
            return Json(member.getMembers((Member.memberType)typeId, 1, new Common().convertDate(date.Replace("/","-"), true)), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchMusic(string keyword)
        {
            return Json(new Hymn().getHymns(keyword), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveData(int member, bool present, int type, string date)
        {
            return Json(new Frequency().setPresent(
                member, new Common().convertDate(date.Replace("/", "-"), true), (Member.memberType)type, present), 
                JsonRequestBehavior.AllowGet);
        }
    }
}
