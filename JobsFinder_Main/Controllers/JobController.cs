using JobsFinder_Main.Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace JobsFinder_Main.Controllers
{
    public class JobController : Controller
    {
        // GET: Job
        [HttpGet]
        public ActionResult Index(string searchString, string searchName, string searchLocation, string fillterCareer, string fillterCategory, string fillterGender, string fillterEXP, int page = 1, int pageSize = 10)
        {
            SetViewBag();
            var dao = new JobDao();
            var model = dao.ListAllPaging( searchString, searchName, searchLocation, fillterCareer, fillterCategory, fillterGender, fillterEXP, page, pageSize);
            ViewBag.SearchName = searchString;
            ViewBag.SearchName = searchName;
            ViewBag.SearchLocation = searchLocation;
            ViewBag.FillterCareer = fillterCareer;
            ViewBag.FillterCategory = fillterCategory;
            ViewBag.FillterGender = fillterGender;
            ViewBag.FillterEXP = fillterEXP;

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Carousel()
        {
            SetViewBag();
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Tab_Fillter()
        {
            return PartialView();
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

        public ActionResult Detail(long id)
        {
            var job = new JobDao().ViewDetail(id);
            var recument = new Recument();

            job.recument = recument;
            return View(job);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CompanyDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Apply(Job entity)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                var dao = new JobDao();
                var recumentDao = new RecumentDao();
                var confirm = recumentDao.CheckApply(session.UserID, entity.recument.JobID);

                if (confirm == false)
                {
                    TempData["Message"] = "Ứng tuyển không thành công!";
                    TempData["MessageType"] = "error";
                    TempData["Type"] = "Thất bại";
                    return RedirectToAction("Index", "Job", entity.ID);
                }
                else
                {
                    var job = new Job
                    {
                        recument = new Recument
                        {
                            JobID = entity.recument.JobID,
                            UserID = session.UserID,
                            LetterInfo = entity.recument.LetterInfo,
                            CreateDate = DateTime.Now,
                            Status = 0,
                            Name = session.Name,
                            Phone = session.Phone,
                            Email = session.Email,
                            Address = session.Address,
                            ProfileID = entity.recument.ProfileID,
                        }
                    };

                    var result = dao.ApplyJob(job);
                    if (result == true)
                    {
                        TempData["Message"] = "Ứng tuyển thành công!";
                        TempData["MessageType"] = "success";
                        TempData["Type"] = "Thành công";
                        return RedirectToAction("Index", "Job", entity.ID);
                    }
                    else
                    {
                        TempData["Message"] = "Có lỗi xảy ra! Vui lòng thử lại!";
                        TempData["MessageType"] = "warning";
                        TempData["Type"] = "Thất bại";
                        return RedirectToAction("Index", "Job", entity.ID);
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}