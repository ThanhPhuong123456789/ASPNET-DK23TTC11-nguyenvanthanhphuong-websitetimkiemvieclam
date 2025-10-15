using JobsFinder_Main.Common;
using JobsFinder_Main.Models;
using Model.DAO;
using Model.EF;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace JobsFinder_Main.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        [HttpGet]
        public ActionResult Index(string searchString, string searchName, string searchLocation, int page = 1, int pageSize = 12)
        {
            SetViewBag();
            var dao = new CompanyDao();
            var model = dao.ListAllPaging(searchString, searchName, searchLocation, page, pageSize);
            ViewBag.SearchName = searchString;
            ViewBag.SearchLocation = searchLocation;

            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var company = new CompanyDao().ViewDetail(id);
            return View(company);
        }


        [ChildActionOnly]
        public ActionResult Carousel_Company()
        {
            SetViewBag();
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Testimonial()
        {
            var model = new CompanyDao().ListAll();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult JobInCompany(Company company)
        {
            var companyID = company.ID;
            var model = new JobDao().GetJobInCompany(companyID);
            return PartialView(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CompanyDao().Delete(id);
            return RedirectToAction("Index");
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
        }
    }
}