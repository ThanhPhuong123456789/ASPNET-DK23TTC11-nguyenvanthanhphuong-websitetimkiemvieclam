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
    public class HoatDongController : Controller
    {
        // GET: HoatDong
        public ActionResult Index()
        {
            return View();
        }

        // GET: HoatDong/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: HoatDong/Create
        [HttpPost]
        public ActionResult Create(HoatDongModel model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if(session != null)
            {
                var dao = new HoatDongDao();

                var hoatDong = new HoatDong
                {
                    TenHoatDong = model.TenHoatDong,
                    ViTriThamGia = model.ViTriThamGia,
                    ThangThamGia = model.ThangThamGia,
                    NamThamGia = model.NamThamGia,
                    ThangKetThuc = model.ThangKetThuc,
                    NamKetThuc = model.NamKetThuc,
                    MoTaChiTiet = model.MoTaChiTiet,
                    Img = model.Img,
                    LienKet = model.LienKet,
                    UserID = session.UserID,
                    CreatedBy = session.UserName
                };

                var result = dao.Insert(hoatDong);
                if(result == true)
                {
                    model = new HoatDongModel();
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
            var hoatDong = new HoatDongDao().ViewDetail(id);
            return PartialView(hoatDong);
        }

        [HttpPost]
        public ActionResult Update(HoatDong model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                var dao = new HoatDongDao();
                var entity = dao.ViewDetail(model.ID);
                if (entity != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (!string.IsNullOrEmpty(model.TenHoatDong))
                        {
                            entity.TenHoatDong = model.TenHoatDong;
                        }
                        if (!string.IsNullOrEmpty(model.ViTriThamGia))
                        {
                            entity.ViTriThamGia = model.ViTriThamGia;
                        }
                        if (!string.IsNullOrEmpty(model.ThangThamGia.ToString()))
                        {
                            entity.ThangThamGia = model.ThangThamGia;
                        }
                        if (!string.IsNullOrEmpty(model.NamThamGia.ToString()))
                        {
                            entity.NamThamGia = model.NamThamGia;
                        }
                        if (!string.IsNullOrEmpty(model.ThangKetThuc.ToString()))
                        {
                            entity.ThangKetThuc = model.ThangKetThuc;
                        }
                        if (!string.IsNullOrEmpty(model.NamKetThuc.ToString()))
                        {
                            entity.NamKetThuc = model.NamKetThuc;
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
            var hoatDong = new HoatDongDao().ViewDetail(id);
            return PartialView(hoatDong);
        }

        [HttpPost]
        public ActionResult DeleteConfirm(HoatDong model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                var dao = new HoatDongDao();
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