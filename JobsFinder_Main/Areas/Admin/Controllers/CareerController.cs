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
    public class CareerController : BaseController
    {
        // GET: Admin/Career
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CareerDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;

            return View(model);
        }

        // GET: Admin/Career/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // GET: Admin/Career/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var career = new CareerDao().ViewDetail(id);
            return View(career);
        }

        // POST: Admin/Career/Create
        [HttpPost]
        public ActionResult Create(Career career)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new CareerDao();

                    long id = dao.Insert(career);
                    if (id > 0)
                    {
                        SetAlert("Thêm bản ghi mới thành công", "success");
                        return RedirectToAction("Index", "Career");
                    }
                    else
                    {
                        SetAlert("Thêm bản ghi mới KHÔNG thành công", "warning");
                        return RedirectToAction("Index", "Career");
                    }
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                SetAlert("Lỗi khi thêm mới bản ghi: " + ex.Message, "error");
                return RedirectToAction("Index", "Career");
            }
        }


        // GET: Admin/Career/Edit/{id}
        [HttpPost]
        public ActionResult Edit(Career career)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new CareerDao();
                    var result = dao.Update(career);

                    if (result)
                    {
                        SetAlert("Cập nhật bản ghi thành công", "success");
                        return RedirectToAction("Index", "Career");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật bản ghi không thành công");
                    }
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                SetAlert("Lỗi khi cập nhật bản ghi: " + ex.Message, "error");
                return RedirectToAction("Index", "Career");
            }
        }


        // GET: Admin/Career/Delete/{id}
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CareerDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}