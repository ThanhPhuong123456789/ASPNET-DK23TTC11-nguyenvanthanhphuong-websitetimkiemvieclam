using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JobsFinder_Main.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()

        {
            SetViewBag();
            int companyCount = CountCompany();
            ViewBag.CompanyCount = companyCount;

            int jobCount = CountJob();
            ViewBag.JobCount = jobCount;

            int jobCountNew = CountJobNew();
            ViewBag.JobCountNew = jobCountNew;

            int userCount = UserCount();
            ViewBag.UserCount = userCount;

            return View();
        }

        [ChildActionOnly]
        public ActionResult Carousel()
        {
            SetViewBag();
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult Carousel_Company()
        {
            SetViewBag();
            return PartialView();
        }


        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }

        public void SetViewBag(long? selectedId = null)
        {
            var commonDao = new CommonDao.CityDao();
            var cities = commonDao.GetAllCities().Select(c => new SelectListItem
            {
                Value = c.Value,
                Text = c.Name
            });
            ViewBag.CityList = new SelectList(cities, "Value", "Text", selectedId);


            var careerDao = new CareerDao();
            List<Career> careers = careerDao.ListAll().ToList();
            var jobDao = new JobDao();
            foreach (var career in careers)
            {
                var count = jobDao.ListAll().Count(j => j.CarrerID == career.ID);
                career.JobCount = count;
            }
            ViewBag.AllCareers = careers;

            var jobCategoryDao = new JobCategoryDao();
            var jobCategories = jobCategoryDao.ListAll().ToList();
            foreach (var jobCategory in jobCategories)
            {
                var count = jobDao.ListAll().Count(j => j.CategoryID == jobCategory.ID);
                jobCategory.JobCount = count;
            }
            ViewBag.AllJobCategory = jobCategories;

            var job = jobDao.ListNew().ToList();
            ViewBag.NewJob = job;
        }

        public int CountCompany()
        {
            var dao = new CompanyDao();
            int companyCount  = dao.CountCompanies();
            return companyCount;
        }

        public int CountJob()
        {
            var dao = new JobDao();
            int jobCount = dao.CountJob();
            return jobCount;
        }
        public int CountJobNew()
        {
            var dao = new JobDao();
            int jobCountNew = dao.CountJobNew();
            return jobCountNew;
        }

        public int UserCount()
        {
            var dao = new UserDao();
            int userCount = dao.CountUser();
            return userCount;
        }

    }
}