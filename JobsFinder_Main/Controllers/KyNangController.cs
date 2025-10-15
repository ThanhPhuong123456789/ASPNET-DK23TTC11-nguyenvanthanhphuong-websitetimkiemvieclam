using JobsFinder_Main.Common;
using JobsFinder_Main.Models;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobsFinder_Main.Controllers
{
    public class KyNangController : Controller
    {
        // GET: KyNang
        public ActionResult Index()
        {
            return View();
        }

        // GET: KyNang/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: KyNang/Create
        [HttpPost]
        public ActionResult Create(KyNangModel model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if(session != null)
            {
                var dao = new KyNangDao();

                var kyNang = new KyNang
                {
                    TenKyNang = model.TenKyNang,
                    DanhGia = model.DanhGia,
                    MoTaChiTiet = model.MoTaChiTiet,
                    UserID = session.UserID,
                    CreatedBy = session.UserName
                };

                var result = dao.Insert(kyNang);
                if(result == true)
                {
                    model = new KyNangModel();
                    TempData["Message"] = "Cập nhật thành công!";
                    TempData["MessageType"] = "success";
                    TempData["Type"] = "Thành công";
                    return RedirectToAction("Index", "Profile", model);
                }
                else
                {
                    TempData["Message"] = "Cập nhật không thành công!";
                    TempData["MessageType"] = "error";
                    TempData["Type"] = "Thất bại";
                    return RedirectToAction("Index", "Profile");
                }
            } else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public ActionResult Update(long id)
        {
            var kyNang = new KyNangDao().ViewDetail(id);
            return PartialView(kyNang);
        }

        [HttpPost]
        public ActionResult Update(KyNang model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                var dao = new KyNangDao();
                var entity = dao.ViewDetail(model.ID);
                if (entity != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (!string.IsNullOrEmpty(model.TenKyNang))
                        {
                            entity.TenKyNang = model.TenKyNang;
                        }
                        if (!string.IsNullOrEmpty(model.DanhGia.ToString()))
                        {
                            entity.DanhGia = model.DanhGia;
                        }
                        if (!string.IsNullOrEmpty(model.MoTaChiTiet))
                        {
                            entity.MoTaChiTiet = model.MoTaChiTiet;
                        }
                        entity.ModifiedBy = session.UserName;
                        entity.ModifiedDate = DateTime.Now;

                        var result = dao.Update(entity);
                        if (result)
                        {
                            TempData["Message"] = "Cập nhật thành công!";
                            TempData["MessageType"] = "success";
                            TempData["Type"] = "Thành công";
                            return RedirectToAction("Index", "Profile");
                        }
                        else
                        {
                            TempData["Message"] = "Cập nhật không thành công!";
                            TempData["MessageType"] = "error";
                            TempData["Type"] = "Thất bại";
                            return RedirectToAction("Index", "Profile");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Có lỗi xảy ra! Vui lòng thử lại!";
                        TempData["MessageType"] = "warning";
                        TempData["Type"] = "Thất bại";
                        return RedirectToAction("Index", "Profile");
                    }
                }
                else
                {
                    TempData["Message"] = "Có lỗi xảy ra! Vui lòng thử lại!";
                    TempData["MessageType"] = "warning";
                    TempData["Type"] = "Thất bại";
                    return RedirectToAction("Index", "Profile");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(long id)
        {
            var kyNang = new KyNangDao().ViewDetail(id);
            return PartialView(kyNang);
        }

        [HttpPost]
        public ActionResult DeleteConfirm(KyNang model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                var dao = new KyNangDao();
                var entity = dao.ViewDetail(model.ID);
                if (entity != null)
                {
                    var result = dao.Delete(entity);
                    if (result)
                    {
                        TempData["Message"] = "Cập nhật thành công!";
                        TempData["MessageType"] = "success";
                        TempData["Type"] = "Thành công";
                        return RedirectToAction("Index", "Profile");
                    }
                    else
                    {
                        TempData["Message"] = "Cập nhật không thành công!";
                        TempData["MessageType"] = "error";
                        TempData["Type"] = "Thất bại";
                        return RedirectToAction("Index", "Profile");
                    }
                }
                else
                {
                    TempData["Message"] = "Có lỗi xảy ra! Vui lòng thử lại!";
                    TempData["MessageType"] = "warning";
                    TempData["Type"] = "Thất bại";
                    return RedirectToAction("Index", "Profile");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}