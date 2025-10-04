using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
using ServiceStack;
using ServiceStack.Script;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Data.Entity.Migrations;

namespace Model.DAO
{
    public class JobDao
    {
        private readonly JobsFinderDBContext db = null;
        public JobDao()
        {
            db = new JobsFinderDBContext();
        }
        public long Insert(Job entity)
        {
            if (entity.Status == null)
            {
                entity.Status = false;
            }
            if (entity.CreatedDate == null)
            {
                entity.CreatedDate = DateTime.Now;
            }
            if (entity.Gender == null)
            {
                entity.Gender = "Tất cả";
            }
            if(entity.MetaTitle == null)
            {
                string name = entity.Name;
                string slug = Regex.Replace(name, @"[^a-zA-Z0-9]", "").ToLower();
                slug = slug.Replace(" ", "-");
                entity.MetaTitle = slug;
            }
            if(entity.Code == null)
            {
                Random random = new Random();

                string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string randomLetters = new string(Enumerable.Repeat(letters, 3).Select(s => s[random.Next(s.Length)]).ToArray());
                string numbers = "0123456789";
                string randomNumbers = new string(Enumerable.Repeat(numbers, 7).Select(s => s[random.Next(s.Length)]).ToArray());
                string code = randomLetters + randomNumbers;

                entity.Code = code;
            }
            if(entity.Experience == null)
            {
                entity.Experience = "0";
            }
            db.Jobs.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool ApplyJob(Job entity)
        {
            db.Jobs.AddOrUpdate(entity);
            db.SaveChanges();
            return true;
        }

        public List<Job> ListInUser(string UserName)
        {
            return db.Jobs.Where(x => x.CreatedBy == UserName).ToList();
        }

        public List<Job> LisInCompany(int? companyID)
        {
            return db.Jobs.Where(x  => x.CompanyID == companyID).ToList();
        }

        public bool Update(Job entity)
        {
            try
            {
                var job = db.Jobs.Find(entity.ID);
                job.Name = entity.Name;
                job.Description = entity.Description;
                job.RequestCandidate = entity.RequestCandidate;
                job.Interest = entity.Interest;
                job.Salary = entity.Salary;
                job.SalaryMin = entity.SalaryMin;
                job.SalaryMax = entity.SalaryMax;
                job.Quantity = entity.Quantity;
                job.CategoryID =  entity.CategoryID;
                job.CarrerID =  entity.CarrerID;
                job.Details = entity.Details;
                job.Deadline = entity.Deadline;
                job.Rank = entity.Rank;
                job.Gender = entity.Gender;
                job.Experience = entity.Experience;
                job.WorkLocation = entity.WorkLocation;
                job.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
            
        }

        public IPagedList<Job> ListAllPaging(string searchString, string searchName, string searchLocation, string fillterCareer, string fillterCategory, string fillterGender, string fillterEXP, int page, int pageSize)
        {
            IQueryable<Job> model = db.Jobs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(searchName))
            {
                model = model.Where(x => x.Name.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(searchLocation))
            {
                model = model.Where(x => x.WorkLocation.Contains(searchLocation));
            }
            if (!string.IsNullOrEmpty(fillterCareer))
            {
                model = model.Where(x => x.CarrerID.ToString().Contains(fillterCareer));
            }
            if (!string.IsNullOrEmpty(fillterCategory))
            {
                model = model.Where(x => x.CategoryID.ToString().Contains(fillterCategory));
            }
            if (!string.IsNullOrEmpty(fillterGender))
            {
                model = model.Where(x => x.Gender.Contains(fillterGender));
            }
            if (!string.IsNullOrEmpty(fillterEXP))
            {
                model = model.Where(x  => x.Experience.Contains(fillterEXP));
            }
            return model.Where(x => x.Status == true)
            .OrderByDescending(x => x.CreatedDate)
            .ToPagedList(page, pageSize);
        }
        public List<Job> ListAll()
        {
            return db.Jobs.Where(x => x.Status == true).ToList();
        }
        public List<Job> ListNew()
        {
            return db.Jobs.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).Take(10).ToList();
        }

        public List<Job> ListSuggest()
        {
            return db.Jobs.Where(x => x.Status == true ).ToList();
        }

        public Job GetByID(string jobName)
        {
            return db.Jobs.SingleOrDefault(x => x.Name == jobName);
        }
        public long? GetCategoryID(long? id)
        {
            var job =  db.Jobs.FirstOrDefault(x => x.CategoryID == id);
            if(job != null)
            {
                return job.CategoryID;
            }
            return null;
        }
        public int? GetCompanyID(int? id)
        {
            var job = db.Jobs.FirstOrDefault(x => x.CompanyID == id);
            if (job != null)
            {
                return job.CompanyID;
            }
            return null;
        }

        public List<Job> GetJobInCompany(int? id)
        {
            var companyId =  GetCompanyID(id);
            return db.Jobs.Where(x  => x.Status == true &&  x.CompanyID == companyId).ToList();
        }

        public long? GetUserID(long? id)
        {
            var job = db.Jobs.FirstOrDefault(x => x.UserID == id);
            if (job != null)
            {
                return job.UserID;
            }
            return null;
        }

        public Job ViewDetail(long id)
        {
            return db.Jobs.Find(id);
        }
        public int CountJob()
        {
            return db.Jobs.Count();
        }
        public int CountJobNew()
        {
            DateTime currentDate = DateTime.Now;
            DateTime yesterday = currentDate.AddDays(-1);
            return db.Jobs.Count(x => x.CreatedDate > yesterday && x.CreatedDate <= currentDate);
        }

        public bool ChangeStatus(long id)
        {
            var job = db.Jobs.Find(id);
            job.Status = !job.Status;

            db.SaveChanges();

            return (bool)job.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var job = db.Jobs.Find(id);
                db.Jobs.Remove(job);
                db.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }
        public string FormatSalary(int? id)
        {
            var job = db.Jobs.Find(id);
            if(job.Salary == true)
            {
                decimal? salaryMin = job.SalaryMin;
                decimal? salaryMax = job.SalaryMax;

                string formattedSalaryMin = (salaryMin / 1000000m).ToString() + " triệu";
                string formattedSalaryMax = (salaryMax / 1000000m).ToString() + " triệu";
                return formattedSalaryMin + " - " + formattedSalaryMax;
            } else
            {
                return "Thương lượng";
            }
        }
        public string FormatTime(int? id)
        {
            var job = db.Jobs.Find(id);
            DateTime? createdDate = job.CreatedDate;
            TimeSpan timeSinceCreation;

            if (createdDate != null)
            {
                timeSinceCreation = DateTime.Now - createdDate.Value;
            }
            else
            {
                timeSinceCreation = TimeSpan.Zero;
            }

            string timeAgo;

            if (timeSinceCreation.TotalHours >= 24)
            {
                int daysAgo = (int)timeSinceCreation.TotalDays;
                timeAgo = $"{daysAgo} ngày trước";
            }
            else if (timeSinceCreation.TotalHours >= 1)
            {
                int hoursAgo = (int)timeSinceCreation.TotalHours;
                timeAgo = $"{hoursAgo} giờ trước";
            }
            else
            {
                int minutesAgo = (int)timeSinceCreation.TotalMinutes;
                timeAgo = $"{minutesAgo} phút trước";
            }
            return timeAgo;
        }

        public string CountTime(int? id)
        {
            var job = db.Jobs.Find(id);
            DateTime? deadline = job.Deadline;
            TimeSpan remainingTime;

            if (deadline != null)
            {
                remainingTime = deadline.Value - DateTime.Now;
            }
            else
            {
                remainingTime = TimeSpan.Zero;
            }

            int remainingDays = (int)remainingTime.TotalDays;

            if (remainingDays <= 0)
            {
                job.Status = true;
                db.SaveChanges();
                return "hahaha";
            }

            return $"Còn {remainingDays} ngày để ứng tuyển";
        }


        public string SubTitle(int? comID, long? uID)
        {
            var companyDao = new CompanyDao();
            var userDao = new UserDao();
            int? companyID =  GetCompanyID(comID);
            long? userID =  GetUserID(uID);
            string name;
            if (companyID != null)
            {
                name = companyDao.GetName(companyID);
            }
            else if (userID != null)
            {
                name = userDao.GetName(userID);
            }
            else
            {
                name = "JobsFinder";
            }
            return name;
        }
        public string GetCategory(long? cateID)
        {
            var category = new JobCategoryDao();
            long? categoryID = GetCategoryID(cateID);
            string name;
            if (categoryID != null)
            {
                name = category.GetName(categoryID);
            } else
            {
                name = "Không có danh mục";
            }
            return name;
        }
        public string MetaTitle(int? comID)
        {
            var companyDao = new CompanyDao();
            int? companyID =  GetCompanyID(comID);
            string name;
            if (companyID != null)
            {
                name = companyDao.GetMetaTitle(companyID);
            }
            else
            {
                name = "JobsFinder";
            }
            return name;
        }
        public string GetAvatarFromCompany(int? comID, long? uID)
        {
            var companyDao = new CompanyDao();
            var userDao = new UserDao();
            int? companyID =  GetCompanyID(comID);
            long? userID =  GetUserID(uID);
            string name;
            if (companyID != null)
            {
                name = companyDao.GetAvatar(companyID);
            }
            else if (userID != null)
            {
                name = userDao.GetAvatar(userID);
            }
            else
            {
                name = "./Assets/Client/JobsFinder/img/Logo.png";
            }
            return name;
        }
    }
}
