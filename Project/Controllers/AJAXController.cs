using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zadatak1.Controllers
{
    public class AJAXController : Controller
    {
        public ActionResult GetKupci()
        {
            return Json(Repo.GetTop10Kupaca(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetKupac(int id)
        {
            return Json(Repo.GetKupac(id), JsonRequestBehavior.AllowGet);
        }
    }
}