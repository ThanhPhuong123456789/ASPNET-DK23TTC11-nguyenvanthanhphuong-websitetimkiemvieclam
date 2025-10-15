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
    public class UserController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url)
                {
                    Query = null,
                    Fragment = null,
                    Path = Url.Action("FacebookCallBack")
                };
                return uriBuilder.Uri;
            }
        }

        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User
                    {
                        UserName = model.UserName,
                        Password = Encryptor.MD5Hash(model.Password),
                        Name = model.Name,
                        Phone = model.Phone,
                        Email = model.Email,
                        Avatar = "./Assets/Client/JobsFinder/img/Logo.png",
                        CreatedDate = DateTime.Now,
                        Status = true
                    };
                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        model = new RegisterModel();
                        TempData["Message"] = "Đăng ký thành công!";
                        TempData["MessageType"] = "success";
                        TempData["Type"] = "Thành công";
                        return RedirectToAction("Login", "User", model);
                    }
                    else
                    {
                        TempData["Message"] = "Đăng ký không thành công!";
                        TempData["MessageType"] = "error";
                        TempData["Type"] = "Thất bại";
                        return RedirectToAction("Register", "User");
                    }
                }
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin
                    {
                        UserName = user.UserName,
                        UserID = user.ID,
                        Name = user.Name,
                        Phone = user.Phone,
                        Address = user.Address,
                        Avatar = user.Avatar,
                        Email = user.Email
                    };
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

        public ActionResult FacebookCallBack(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecretFbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code
            });

            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                dynamic me = fb.Get("me?fieldss=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string firstName = me.first_name;
                string middleName = me.middle_name;
                string lastName = me.last_name;

                var user = new User
                {
                    Email = email,
                    UserName = email,
                    Status = true,
                    Name = firstName + " " + middleName + " " + lastName,
                    CreatedDate = DateTime.Now
                };
                var resultInsert = new UserDao().InsertForFacebook(user);
                if (resultInsert > 0)
                {
                    var userSession = new UserLogin
                    {
                        UserName = user.UserName,
                        UserID = user.ID
                    };
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                }
            }
            return Redirect("/");
        }



        public ActionResult LoginFaceBook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecretFbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                respone_type = "code",
                scope = "email",
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(User user)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var dao = new UserDao();
                var userToUpdate = dao.GetByID(session.UserName);
                if (userToUpdate != null)
                {
                    if (!string.IsNullOrEmpty(user.Name))
                    {
                        userToUpdate.Name = user.Name;
                    }
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        userToUpdate.Password = Encryptor.MD5Hash(user.Password);
                    }
                    if (!string.IsNullOrEmpty(user.Phone.ToString()))
                    {
                        userToUpdate.Phone = user.Phone;
                    }
                    if (!string.IsNullOrEmpty(user.Address))
                    {
                        userToUpdate.Address = user.Address;
                    }
                    if (!string.IsNullOrEmpty(user.Avatar))
                    {
                        userToUpdate.Avatar = user.Avatar;
                    }
                    userToUpdate.ModifiedDate = DateTime.Now;

                    var result = dao.Update(userToUpdate);
                    if (result)
                    {
                        session.Name = userToUpdate.Name;
                        session.Phone = userToUpdate.Phone;
                        session.Address = userToUpdate.Address;
                        session.Avatar = userToUpdate.Avatar;
                        TempData["Message"] = "Cập nhật thành công!";
                        TempData["MessageType"] = "success";
                        TempData["Type"] = "Thành công";
                        return RedirectToAction("Manager", "User");
                    }
                    else
                    {
                        TempData["Message"] = "Cập nhật không thành công!";
                        TempData["MessageType"] = "error";
                        TempData["Type"] = "Thất bại";
                        return RedirectToAction("Manager", "User", user);
                    }
                }
                else
                {
                    TempData["Message"] = "Có lỗi xảy ra! Vui lòng thử lại!";
                    TempData["MessageType"] = "warning";
                    TempData["Type"] = "Thất bại";
                    return RedirectToAction("Manager", "User", user);
                }
            }
        }

        [HttpGet]
        public ActionResult Manager(User user)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var dao = new UserDao();
                var userDetail = dao.GetByID(session.UserName);
                if (userDetail != null)
                {
                    user.Name = session.Name;
                    user.Phone = session.Phone;
                    user.Email = session.Email;
                    user.Address = session.Address;
                    user.Avatar = session.Avatar;
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpGet]
        public ActionResult CompanyManager(User user)
        {
            var session = (UserLogin)Session[(CommonConstants.USER_SESSION)];
            if(session == null)
            {
                return RedirectToAction("Login", "User");
            } else
            {
                var dao = new UserDao();
                var userDetail =dao.GetByID(session.UserName);
                if(userDetail != null)
                {
                    user.UserName = session.UserName;
                    var companyDao = new CompanyDao();
                    var company = new CompanyModel();
                    var model = companyDao.ListInUser(user.UserName);

                    foreach(var item in model)
                    {
                        company.Name  = item.Name;
                        company.Avatar  = item.Avatar;
                        company.Background  = item.Background;
                        company.LinkPage  = item.LinkPage;
                        company.Employees  = item.Employees;
                        company.Location  = item.Location;
                        company.Description  = item.Description;
                    }

                    return View(company);
                } else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpGet]
        public ActionResult JobManager(User user)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var dao = new UserDao();
                var userDetail = dao.GetByID(session.UserName);
                if (userDetail != null)
                {
                    user.UserName = session.UserName;
                    var jobDao = new JobDao();
                    var job = new JobModel();
                    var model = jobDao.ListInUser(user.UserName);

                    foreach (var item in model)
                    {
                        job.ID = item.ID;
                        job.Name = item.Name;
                        job.MetaTitle = item.MetaTitle;
                        job.Description = item.Description;
                        job.RequestCandidate = item.RequestCandidate;
                        job.Interest = item.Interest;
                        job.Image = jobDao.GetAvatarFromCompany(item.CompanyID, item.UserID);
                        job.Salary = item.Salary;
                        job.SalaryMin = item.SalaryMin;
                        job.SalaryMax = item.SalaryMax;
                        job.Quantity = item.Quantity;
                        job.CategoryID = item.CategoryID;
                        job.Details = item.Details;
                        job.Deadline = item.Deadline;
                        job.Rank = item.Rank; ;
                        job.Gender = item.Gender;
                        job.Experience = item.Experience;
                        job.WorkLocation = item.WorkLocation;
                        job.CompanyID = item.CompanyID;
                        job.CarrerID = item.CarrerID;
                        job.UserID = item.UserID;
                        job.code = item.Code;
                    }
                    return View(job);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult CreateCompany()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateCompany(CompanyModel model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var company = new Company();
                var dao = new CompanyDao();
                company.Name = model.Name;
                company.LinkPage = model.LinkPage;
                company.Description = model.Description;
                company.Avatar = model.Avatar;
                company.Background = model.Background;
                company.Employees = model.Employees;
                company.Location = model.Location;
                string name = model.Name;
                string slug = Regex.Replace(name, @"[^a-zA-Z0-9]", "-").ToLower();
                company.MetaTitle = slug;
                company.CreatedDate = DateTime.Now;
                company.Status = true;
                company.CreatedBy = session.UserName;

                var result = dao.Insert(company);
                if (result > 0)
                {
                    model = new CompanyModel();
                    TempData["Message"] = "Cập nhật thành công!";
                    TempData["MessageType"] = "success";
                    TempData["Type"] = "Thành công";
                    return RedirectToAction("Manager", "User", model);
                }
                else
                {
                    TempData["Message"] = "Cập nhật không thành công!";
                    TempData["MessageType"] = "error";
                    TempData["Type"] = "Thất bại";
                    return RedirectToAction("Manager", "User");
                }
            }
        }
        

        [HttpDelete]
        public ActionResult DeleteCompany(int ID)
        {

            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if(session == null)
            {
                return RedirectToAction("Login", "User");
            } else
            {
                var dao = new CompanyDao();
                var result = dao.Delete(ID);

                if (result)
                {
                    TempData["Message"] = "Cập nhật thành công!";
                    TempData["MessageType"] = "success";
                    TempData["Type"] = "Thành công";
                    return RedirectToAction("Manager", "User");
                } else
                {
                    TempData["Message"] = "Cập nhật không thành công!";
                    TempData["MessageType"] = "error";
                    TempData["Type"] = "Thất bại";
                    return RedirectToAction("Manager", "User");
                }
            }
        }

        [HttpDelete]
        public ActionResult DeleteJob(int id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return RedirectToAction("Login", "User");
            }

            var dao = new JobDao();
            var result = dao.Delete(id);

            if (result)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }


        [HttpGet]
        public ActionResult EditCompany(int ID)
        {
            var company = new CompanyDao().ViewDetail(ID);
            return View(company);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCompany(Company model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var dao = new CompanyDao();
                var company = dao.ViewDetail(model.ID);
                if (company != null)
                {
                    if (!string.IsNullOrEmpty(model.Name))
                    {
                        company.Name = model.Name;
                    }
                    if (model.Employees != null)
                    {
                        company.Employees = model.Employees;
                    }
                    if (!string.IsNullOrEmpty(model.Location))
                    {
                        company.Location = model.Location;
                    }
                    if (!string.IsNullOrEmpty(model.Avatar))
                    {
                        company.Avatar = model.Avatar;
                    }
                    if (!string.IsNullOrEmpty(model.Background))
                    {
                        company.Background = model.Background;
                    }
                    if (!string.IsNullOrEmpty(model.Description))
                    {
                        company.Description = model.Description;
                    }
                    company.ModifiedDate = DateTime.Now;
                    company.ModifiedBy = session.UserName;
                    var result = dao.Update(company);

                    if (result)
                    {
                        TempData["Message"] = "Cập nhật thành công!";
                        TempData["MessageType"] = "success";
                        TempData["Type"] = "Thành công";
                        return RedirectToAction("CompanyManager", "User", new { company.ID });
                    } else
                    {
                        TempData["Message"] = "Cập nhật không thành công!";
                        TempData["MessageType"] = "error";
                        TempData["Type"] = "Thất bại";
                        return RedirectToAction("CompanyManager", "User", new { company.ID });
                    }
                }
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult CreateJob()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateJob(JobModel model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if(session == null)
            {
                return RedirectToAction("Login", "User");
            } else
            {
                var job = new Job();
                var dao = new JobDao();

                job.Name = model.Name;
                string name = model.Name;
                string slug = Regex.Replace(name, @"[^a-zA-Z0-9]", "-").ToLower();
                job.MetaTitle = slug;
                job.Description = model.Description;
                job.RequestCandidate = model.RequestCandidate;
                job.Interest = model.Interest;
                job.Image = model.Image;
                job.Salary = model.Salary;
                job.SalaryMin = model.SalaryMin;
                job.SalaryMax = model.SalaryMax;
                job.Quantity = model.Quantity;
                Random random = new Random();

                string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string randomLetters = new string(Enumerable.Repeat(letters, 3).Select(s => s[random.Next(s.Length)]).ToArray());
                string numbers = "0123456789";
                string randomNumbers = new string(Enumerable.Repeat(numbers, 7).Select(s => s[random.Next(s.Length)]).ToArray());
                string code = randomLetters + randomNumbers;
                job.Code = code;

                job.CategoryID = model.CategoryID;
                job.Details = model.Details;
                job.Deadline = model.Deadline;
                job.Rank = model.Rank;
                job.Gender = model.Gender;
                job.Experience = model.Experience;
                job.WorkLocation = model.WorkLocation;
                job.CompanyID = model.CompanyID;
                job.CarrerID = model.CarrerID;
                job.UserID = model.UserID;
                job.CreatedBy = session.UserName;
                job.Status = true;

                var result = dao.Insert(job);

                if (result > 0)
                {
                    model = new JobModel();

                    TempData["Message"] = "Cập nhật thành công!";
                    TempData["MessageType"] = "success";
                    TempData["Type"] = "Thành công";
                    return RedirectToAction("Manager", "User", model);
                }
                else
                {
                    TempData["Message"] = "Cập nhật không thành công!";
                    TempData["MessageType"] = "error";
                    TempData["Type"] = "Thất bại";
                    return RedirectToAction("Manager", "User");
                }
            }
        }

        [HttpGet]
        public ActionResult ListJobs()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var user = new User();
            if (session == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var dao = new UserDao();
                var userDetail = dao.GetByID(session.UserName);
                if (userDetail != null)
                {
                    user.Name = session.Name;
                    user.Phone = session.Phone;
                    user.Email = session.Email;
                    user.Address = session.Address;
                    user.Avatar = session.Avatar;

                    var jobDao = new JobDao();
                    var list = jobDao.ListInUser(user.UserName);
                    return View(list);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpGet]
        public ActionResult ListCompany()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var user = new User();
            if (session == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var dao = new UserDao();
                var userDetail = dao.GetByID(session.UserName);
                if (userDetail != null)
                {
                    user.Name = session.Name;
                    user.Phone = session.Phone;
                    user.Email = session.Email;
                    user.Address = session.Address;
                    user.Avatar = session.Avatar;

                    var companyDao = new CompanyDao();
                    var list = companyDao.ListInUser(user.UserName);
                    return View(list);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpGet]
        public ActionResult EditJob(int ID)
        {
            var job = new JobDao().ViewDetail(ID);
            return View(job);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditJob(Job model)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var dao = new JobDao();
                var job = dao.ViewDetail(model.ID);
                if (job != null)
                {
                    if (!string.IsNullOrEmpty(model.Name))
                    {
                        job.Name = model.Name;
                    }
                    if (!string.IsNullOrEmpty(model.Description))
                    {
                        job.Description = model.Description;
                    }
                    if (!string.IsNullOrEmpty(model.RequestCandidate))
                    {
                        job.RequestCandidate = model.RequestCandidate;
                    }
                    if (!string.IsNullOrEmpty(model.Interest))
                    {
                        job.Interest = model.Interest;
                    }
                    if (!string.IsNullOrEmpty(model.Details))
                    {
                        job.Details = model.Details;
                    }
                    job.Salary = model.Salary;
                    job.SalaryMin = model.SalaryMin;
                    job.SalaryMax = model.SalaryMax;

                    job.Quantity = model.Quantity;
                    job.CategoryID  = model.CategoryID;
                    job.CarrerID = model.CarrerID;
                    job.Deadline = model.Deadline;
                    job.Experience = model.Experience;
                    job.Gender  = model.Gender;
                    job.WorkLocation = model.WorkLocation;
                    var result = dao.Update(job);

                    if (result)
                    {
                        TempData["Message"] = "Cập nhật thành công!";
                        TempData["MessageType"] = "success";
                        TempData["Type"] = "Thành công";
                        return RedirectToAction("ListJobs", "User");
                    } else
                    {
                        TempData["Message"] = "Cập nhật không thành công!";
                        TempData["MessageType"] = "error";
                        TempData["Type"] = "Thất bại";
                        return RedirectToAction("ListJobs", "User");
                    }
                }
            }
            return View(model);
        }

        public void SetViewBag(long? selectedId = null)
        {
            var JobCategorydao = new JobCategoryDao();
            ViewBag.JobCategoryId = new SelectList(JobCategorydao.ListAll(), "ID", "Name", selectedId);

            var careerDao = new CareerDao();
            ViewBag.JobCareerId = new SelectList(careerDao.ListAll(), "ID", "Name", selectedId);

            var companyDao = new CompanyDao();
            ViewBag.CompanyId = new SelectList(companyDao.ListAll(), "ID", "Name", selectedId);
        }
    }
}