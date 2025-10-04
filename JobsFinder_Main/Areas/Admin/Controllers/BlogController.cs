using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace JobsFinder_Main.Areas.Admin.Controllers
{
    public class BlogController : BaseController
    {
        // GET: Admin/Blog
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BlogDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;

            return View(model);
        }

        // GET: Admin/Blog/Create
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        // POST: Admin/Blog/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Blog model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new BlogDao();

                    long id = dao.Insert(model);
                    if (id > 0)
                    {
                        SetAlert("Thêm bản ghi mới thành công", "success");
                        return RedirectToAction("Index", "Blog");
                    }
                    else
                    {
                        SetAlert("Thêm bản ghi mới không thành công", "warning");
                        return RedirectToAction("Index", "Blog");
                    }
                }
                SetViewBag();
                return View("Index");
            }
            catch (Exception)
            {
                SetAlert("Có lỗi xảy ra, vui lòng thử lại sau!", "error");
                return RedirectToAction("Index", "Blog");
            }

        }

        // GET: Admin/Blog/Edit
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new BlogDao();
            var blog = dao.GetById(id);
            SetViewBag(blog.CategoryID);
            return View();
        }
        // POST: Admin/Blog/Edit
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Blog model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new BlogDao();
                    var result = dao.Update(model);
                    if (result)
                    {
                        SetAlert("Cập nhật danh mục công việc thành công", "success");
                        return RedirectToAction("Index", "Blog");
                    }
                    else
                    {
                        SetAlert("Thêm bản ghi mới không thành công", "warning");
                        ModelState.AddModelError("", "Cập nhật danh mục công việc không thành công");
                    }
                }
                SetViewBag(model.CategoryID);
                return View("Index");
            }
            catch (Exception)
            {
                SetAlert("Có lỗi xảy ra, vui lòng thử lại sau!", "error");
                return RedirectToAction("Index", "Blog");
            }
        }


        // DELETE: Admin/Blog/Delete/{id}
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new BlogDao().Delete(id);
            return RedirectToAction("Index");
        }

        // POST: Admin/Blog/ChangeStatus/{id}
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new BlogDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new BlogCategoryDao();
            ViewBag.BlogCategoryId = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
    }
}