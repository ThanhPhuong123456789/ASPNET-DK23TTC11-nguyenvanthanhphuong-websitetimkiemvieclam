using JobsFinder_Main.Common;
using Model.DAO;
using Model.EF;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace JobsFinder_Main.Areas.Admin.Controllers
{
    public class BlogCategoryController : BaseController
    {
        // GET: Admin/BlogCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BlogCategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;

            return View(model);
        }

        // GET: Admin/BlogCategory/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // GET: Admin/BlogCategory/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var blogCategory = new BlogCategoryDao().ViewDetail(id);
            return View(blogCategory);
        }

        // POST: Admin/BlogCategory/Create
        [HttpPost]
        public ActionResult Create(BlogCategory blogCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new BlogCategoryDao();

                    long id = dao.Insert(blogCategory);
                    if (id > 0)
                    {
                        SetAlert("Thêm bản ghi mới thành công", "success");
                        return RedirectToAction("Index", "BlogCategory");
                    }
                    else
                    {
                        SetAlert("Thêm bản ghi mới KHÔNG thành công", "warning");
                        return RedirectToAction("Index", "BlogCategory");
                    }
                }
                return View(blogCategory);
            }
            catch (Exception ex)
            {
                SetAlert("Có lỗi xảy ra: " + ex.Message, "danger");
                return RedirectToAction("Index", "BlogCategory");
            }
        }


        // POST: Admin/BlogCategory/Edit/{id}
        [HttpPost]
        public ActionResult Edit(BlogCategory blogCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new BlogCategoryDao();
                    var result = dao.Update(blogCategory);

                    if (result)
                    {
                        SetAlert("Cập nhật danh mục công việc thành công", "success");
                        return RedirectToAction("Index", "BlogCategory");
                    }
                    else
                    {
                        SetAlert("Cập nhật bản ghi KHÔNG thành công", "warning");
                    }
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                SetAlert("Cập nhật danh mục công việc thất bại: " + ex.Message, "error");
                return RedirectToAction("Index", "BlogCategory");
            }

        }

        // DELETE: Admin/BlogCategory/Delete/{id}
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new BlogCategoryDao().Delete(id);
            return RedirectToAction("Index");
        }

        // POST: Admin/BlogCategory/ChangeStatus/{id}
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new BlogCategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}