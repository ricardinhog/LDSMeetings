using OrganizadorReuniao.Helper;
using OrganizadorReuniao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizadorReuniao.Controllers
{
    public class MemoController : BaseController
    {
        public ActionResult Get(string date)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                DateTime meetingDate = new Common().convertDate(date.Replace("/", "-"), true);
                Memo memo = new Memo();
                memo.loadData(meetingDate, loggedUser.Unit);
                return Json(memo, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Save(int id, string date, int conductedBy, int presidedBy, string recognitions,
            int firstHymn, int firstPrayer, string stake, int stakeFlag, string ward, int wardFlag, int sacramentalHymn,
            int speaker1, int speaker2, int speaker3, int speaker4, int speaker5, string theme1, string theme2,
            string theme3, string theme4, string theme5, int intermediateHymn, int pianist,
            int conductor, string otherSubjects, int lastHymn, int lastPrayer)
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                DateTime meetingDate = new Common().convertDate(date.Replace("/", "-"), true);

                Memo memo = new Memo();
                memo.id = id;
                memo.date = meetingDate;
                memo.conductedBy = conductedBy;
                memo.presidedBy = presidedBy;
                memo.recognitions = recognitions;
                memo.openingHymn = firstHymn;
                memo.firstPrayer = firstPrayer;
                memo.stake = stake;
                memo.stakeFlag = stakeFlag;
                memo.ward = ward;
                memo.wardFlag = wardFlag;
                memo.sacramentalHymn = sacramentalHymn;
                memo.speaker1 = speaker1;
                memo.speaker2 = speaker2;
                memo.speaker3 = speaker3;
                memo.speaker4 = speaker4;
                memo.speaker5 = speaker5;
                memo.speaker1Theme = theme1;
                memo.speaker2Theme = theme2;
                memo.speaker3Theme = theme3;
                memo.speaker4Theme = theme4;
                memo.speaker5Theme = theme5;
                memo.intermediateHymn = intermediateHymn;
                memo.pianistBy = pianist;
                memo.hymnConductedBy = conductor;
                memo.otherSubjects = otherSubjects;
                memo.lastHymn = lastHymn;
                memo.lastPrayer = lastPrayer;

                return Json(memo.updateOrAdd(loggedUser.Unit), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index()
        {
            if (!isAuthenticated())
                return new HttpUnauthorizedResult();
            else
            {
                ViewBag.unit = loggedUser.Unit;
                ViewBag.unitName = loggedUser.UnitName;

                return View();
            }
        }
    }
}
