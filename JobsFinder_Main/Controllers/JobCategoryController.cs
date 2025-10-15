using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobsFinder_Main.Controllers
{
    public class JobCategoryController : Controller
    {
        // GET: JobCategory
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ListJobCategry()
        {
            var model = new JobCategoryDao().GetOption();
            return PartialView(model);
        }
    }
}