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
    public class DuAnController : Controller
    {
        // GET: DuAn
        public ActionResult Index()
        {
            return View();
        }

        // GET: DuAn/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: DuAn/Create
        [HttpPost]
        public ActionResult Create(DuAnModel model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if(session != null)
            {
                var dao = new DuAnDao();

                var duAn = new DuAn
                {
                    TenDuAn = model.TenDuAn,
                    TenKhachHang = model.TenKhachHang,
                    SoThanhVien = model.SoThanhVien,
                    ViTri = model.ViTri,
                    NhiemVu = model.NhiemVu,
                    CongNgheSuDung = model.CongNgheSuDung,
                    ThangBatDau = model.ThangBatDau,
                    NamBatDau = model.NamBatDau,
                    ThangKetThuc =  model.ThangKetThuc,
                    NamKetThuc = model.NamKetThuc,
                    MoTaChiTiet = model.MoTaChiTiet,
                    Img = model.Img,
                    LienKet = model.LienKet,
                    UserID = session.UserID,
                    CreatedBy = session.UserName
                };

                var result = dao.Insert(duAn);
                if(result == true)
                {
                    model = new DuAnModel();
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
                    RedirectToAction("Index", "Profile");
                }
            } else
            {
                TempData["Message"] = "Có lỗi xảy ra! Vui lòng thử lại!";
                TempData["MessageType"] = "warning";
                TempData["Type"] = "Thất bại";
                return RedirectToAction("Index", "Profile");
            }
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult Update(long id)
        {
            var duAn = new DuAnDao().ViewDetail(id);
            return PartialView(duAn);
        }

        [HttpPost]
        public ActionResult Update(DuAn model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                var dao = new DuAnDao();
                var entity = dao.ViewDetail(model.ID);
                if (entity != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (!string.IsNullOrEmpty(model.TenDuAn))
                        {
                            entity.TenDuAn = model.TenDuAn;
                        }
                        if (!string.IsNullOrEmpty(model.TenKhachHang))
                        {
                            entity.TenKhachHang = model.TenKhachHang;
                        }
                        if (!string.IsNullOrEmpty(model.ViTri))
                        {
                            entity.ViTri = model.ViTri;
                        }
                        if (!string.IsNullOrEmpty(model.NhiemVu))
                        {
                            entity.NhiemVu = model.NhiemVu;
                        }
                        if (!string.IsNullOrEmpty(model.CongNgheSuDung))
                        {
                            entity.CongNgheSuDung = model.CongNgheSuDung;
                        }
                        if (!string.IsNullOrEmpty(model.SoThanhVien.ToString()))
                        {
                            entity.SoThanhVien = model.SoThanhVien;
                        }
                        if (!string.IsNullOrEmpty(model.ThangBatDau.ToString()))
                        {
                            entity.ThangBatDau = model.ThangBatDau;
                        }
                        if (!string.IsNullOrEmpty(model.NamBatDau.ToString()))
                        {
                            entity.NamBatDau = model.NamBatDau;
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
            var duAn = new DuAnDao().ViewDetail(id);
            return PartialView(duAn);
        }

        [HttpPost]
        public ActionResult DeleteConfirm(DuAn model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                var dao = new DuAnDao();
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