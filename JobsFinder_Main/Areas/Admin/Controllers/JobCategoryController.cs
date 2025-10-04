using JobsFinder_Main.Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace JobsFinder_Main.Areas.Admin.Controllers
{
    public class JobCategoryController : BaseController
    {
        // GET: Admin/JobCategory/Index
        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new JobCategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;

            return View(model);
        }

        // GET: Admin/JobCategory/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // GET: Admin/JobCategory/Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var jobCategory = new JobCategoryDao().ViewDetail(id);
            return View(jobCategory);
        }

        // POST: Admin/JobCategory/Create
        [HttpPost]
        public ActionResult Create(JobCategory jobCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new JobCategoryDao();

                    long id = dao.Insert(jobCategory);
                    if (id > 0)
                    {
                        SetAlert("Thêm bản ghi mới thành công", "success");
                        return RedirectToAction("Index", "JobCategory");
                    }
                    else
                    {
                        SetAlert("Thêm bản ghi mới KHÔNG thành công", "warning");
                        return RedirectToAction("Index", "JobCategory");
                    }   
                }
                return View(jobCategory);
            }
            catch (Exception ex)
            {
                SetAlert("Có lỗi xảy ra: " + ex.Message, "danger");
                return RedirectToAction("Index", "JobCategory");
            }
        }


        // POST: Admin/JobCategory/Edit/{id}
        [HttpPost]
        public ActionResult Edit(JobCategory jobCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new JobCategoryDao();

                    var result = dao.Update(jobCategory);
                    if (result)
                    {
                        SetAlert("Cập nhật danh mục công việc thành công", "success");
                        return RedirectToAction("Index", "JobCategory");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật danh mục công việc không thành công");
                    }
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                SetAlert("Cập nhật danh mục công việc thất bại: " + ex.Message, "error");
                return RedirectToAction("Index", "JobCategory");
            }
        }


        // DELETE: Admin/JobCategory/Delete/{id}
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new JobCategoryDao().Delete(id);
            return RedirectToAction("Index");
        }

        // DELETE: Admin/JobCategory/ChangeStatus/{id}
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new JobCategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}