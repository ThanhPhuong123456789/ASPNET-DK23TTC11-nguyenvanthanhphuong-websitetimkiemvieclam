using BotDetect.Web.Mvc;
using JobsFinder_Main.Common;
using JobsFinder_Main.Models;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using System.Configuration;
using BotDetect;
using ServiceStack;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;


namespace JobsFinder_Main.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Profile/Create
        [HttpPost]
        public ActionResult Create(Profile profile)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                var dao = new ProfileDao();
                var updateProfile = dao.GetByID(session.UserID);
                if (updateProfile != null)
                {
                    // Thực hiện cập nhật
                    if (!string.IsNullOrEmpty(profile.HoVaTen))
                    {
                        updateProfile.HoVaTen = profile.HoVaTen;
                    }
                    if (!string.IsNullOrEmpty(profile.AnhCaNhan))
                    {
                        updateProfile.AnhCaNhan = profile.AnhCaNhan;
                    }
                    if (!string.IsNullOrEmpty(profile.GioiTinh))
                    {
                        updateProfile.GioiTinh = profile.GioiTinh;
                    }
                    if (!string.IsNullOrEmpty(profile.NgaySinh.ToString()))
                    {
                        updateProfile.NgaySinh = profile.NgaySinh;
                    }
                    if (!string.IsNullOrEmpty(profile.ThangSinh.ToString()))
                    {
                        updateProfile.ThangSinh = profile.ThangSinh;
                    }
                    if (!string.IsNullOrEmpty(profile.NamSinh.ToString()))
                    {
                        updateProfile.NamSinh = profile.NamSinh;
                    }
                    if (!string.IsNullOrEmpty(profile.DiaChiHienTai))
                    {
                        updateProfile.DiaChiHienTai = profile.DiaChiHienTai;
                    }
                    if (!string.IsNullOrEmpty(profile.Email))
                    {
                        updateProfile.Email = profile.Email;
                    }
                    if (!string.IsNullOrEmpty(profile.SoDienThoai))
                    {
                        updateProfile.SoDienThoai = profile.SoDienThoai;
                    }
                    if (!string.IsNullOrEmpty(profile.GioiThieu))
                    {
                        updateProfile.GioiThieu = profile.GioiThieu;
                    }

                    var result = dao.Update(updateProfile);
                    if (result)
                    {
                        TempData["Message"] = "Cập nhật thành công!";
                        TempData["MessageType"] = "success";
                        TempData["Type"] = "Thành công";
                        return RedirectToAction("Index", "Profile");
                    } else
                    {
                        TempData["Message"] = "Cập nhật không thành công!";
                        TempData["MessageType"] = "error";
                        TempData["Type"] = "Thất bại";
                        return RedirectToAction("Index", "Profile");
                    }
                }
                else
                {
                    // Thêm mới
                    profile.UserID = session.UserID;
                    profile.HoVaTen = session.Name;
                    profile.Email = session.Email;
                    profile.SoDienThoai = session.Phone;
                    profile.AnhCaNhan = session.Avatar;

                    var result = dao.Insert(profile);
                    if (result > 0)
                    {
                        TempData["Message"] = "Cập nhật thành công!";
                        TempData["MessageType"] = "success";
                        TempData["Type"] = "Thành công";
                        return RedirectToAction("Index", "Profile");
                    } else
                    {
                        TempData["Message"] = "Cập nhật không thành công!";
                        TempData["MessageType"] = "error";
                        TempData["Type"] = "Thất bại";
                        return RedirectToAction("Index", "Profile");
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult Detail(long id)
        {
            var profile = new ProfileDao().ViewDetail(id);
            return View(profile);
        }

        public ActionResult Confirm(long id)
        {
            var recument = new RecumentDao().ViewDetail(id);
            return View(recument);
        }

        [HttpPost]
        public ActionResult Confirm(Recument entity)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                var dao = new RecumentDao();
                var recument = new Recument
                {
                    Status = entity.Status,
                };

                var result = dao.Confirm(recument);
                if (result == true)
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
                return RedirectToAction("Login", "User");
            }
        }
    }
}