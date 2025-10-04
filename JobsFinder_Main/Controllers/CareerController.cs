using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobsFinder_Main.Controllers
{
    public class CareerController : Controller
    {
        // GET: Career
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ListCareer()
        {
            var model = new CareerDao().GetOption();
            return PartialView(model);
        }
    }
}