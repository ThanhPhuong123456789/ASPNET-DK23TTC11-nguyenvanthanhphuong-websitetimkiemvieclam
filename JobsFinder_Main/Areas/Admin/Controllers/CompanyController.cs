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
    public class CompanyController : BaseController
    {
        // GET: Admin/Company
        public ActionResult Index(string searchString, string searchName, string searchLocation ,int page = 1, int pageSize = 10)
        {
            var dao = new CompanyDao();
            var model = dao.ListAllPaging(searchString, searchName, searchLocation , page, pageSize);
            ViewBag.SearchString = searchString;

            return View(model);
        }

        // GET: Admin/Company/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // GET: Admin/Company/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var company = new CompanyDao().ViewDetail(id);
            return View(company);
        }

        // POST: Admin/Company/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Company company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new CompanyDao();
                    long id = dao.Insert(company);
                    if (id > 0)
                    {
                        SetAlert("Thêm bản ghi mới thành công", "success");
                        return RedirectToAction("Index", "Company");
                    }
                    else
                    {
                        SetAlert("Thêm bản ghi mới KHÔNG thành công", "warning");
                        return RedirectToAction("Index", "Company");
                    }
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                SetAlert("Lỗi khi thêm mới company: " + ex.Message, "error");
                return RedirectToAction("Index", "Company");
            }
        }


        // POST: Admin/Company/Edit/{id}
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Company model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new CompanyDao();

                    var result = dao.Update(model);
                    if (result)
                    {
                        SetAlert("Thêm bản ghi mới thành công", "success");
                        return RedirectToAction("Index", "Company");
                    }
                    else
                    {
                        SetAlert("Thêm bản ghi mới KHÔNG thành công", "warning");
                        return RedirectToAction("Index", "Company");
                    }
                }
                return View("Index");
            }
            catch (Exception)
            {
                SetAlert("Có lỗi xảy ra khi cập nhật company", "error");
                return RedirectToAction("Index", "Company");
            }
        }

        // DELETE: Admin/Company/Delete/{id}
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CompanyDao().Delete(id);
            return RedirectToAction("Index");
        }

        // POST: Admin/Company/ChangeStatus/{id}
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CompanyDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}