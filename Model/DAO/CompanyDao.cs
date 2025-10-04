using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.DAO
{
    public class CompanyDao
    {
        private readonly JobsFinderDBContext db = null;
        public CompanyDao()
        {
            db = new JobsFinderDBContext();
        }
        public long Insert(Company entity)
        {
            if (entity.Status == null)
            {
                entity.Status = false;
            }
            if(entity.CreatedDate == null)
            {
                entity.CreatedDate = DateTime.Now;
            }
            if (entity.MetaTitle == null)
            {
                string name = entity.Name;
                string slug = Regex.Replace(name, @"[^a-zA-Z0-9]", "").ToLower();
                slug = slug.Replace(" ", "-");
                entity.MetaTitle = slug;
            }
            db.Companies.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Company entity)
        {
            try
            {
                var company = db.Companies.Find(entity.ID);
                company.Name = entity.Name;;
                company.LinkPage = entity.LinkPage;
                company.Description = entity.Description;;
                company.Avatar = entity.Avatar;
                company.Employees = entity.Employees;
                company.Location = entity.Location;
                company.ModifiedDate = DateTime.Now;
                company.Status = entity.Status;

                db.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public IPagedList<Company> ListAllPaging(string searchString, string  searchName, string searchLocation, int page, int pageSize)
        {
            IQueryable<Company> model = db.Companies;
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
                model = model.Where(x => x.Location.Contains(searchLocation));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public List<Company> ListAll()
        {
            return db.Companies.Where(x => x.Status == true).ToList();
        }
        public List<Company> ListInUser(string UserName)
        {
            if (UserName != null)
            {
                return db.Companies.Where(x => x.Status == true && x.CreatedBy == UserName).ToList();
            }
            else
            {
                return new List<Company>();
            }
        }
        public Company GetByID(string companyName)
        {
            return db.Companies.SingleOrDefault(x => x.Name == companyName);
        }
        public string GetName(int? CompanyID)
        {
            var company = db.Companies.FirstOrDefault(x => x.ID == CompanyID);
            if (company != null)
            {
                return company.Name;
            }
            else
            {
                return "JobsFinder";
            }
        }
        public string GetMetaTitle(int? CompanyID)
        {
            var company = db.Companies.FirstOrDefault(x => x.ID == CompanyID);
            if (company != null)
            {
                return company.MetaTitle;
            }
            else
            {
                return "jobsfinder";
            }
        }
        public string GetAvatar(int? CompanyID)
        {
            var company = db.Companies.FirstOrDefault(x => x.ID == CompanyID);
            if(company != null)
            {
                return company.Avatar;
            }
            else
            {
                return "./Assets/Client/JobsFinder/img/Logo.png";
            }
        }

        public string GetLinkPage(int? CompanyID)
        {
            var company = db.Companies.FirstOrDefault(x  => x.ID == CompanyID);
            if(company != null)
            {
                return company.LinkPage;
            } else
            {
                return " ";
            }
        }
        public string GetDetail(int? CompanyID)
        {
            var company = db.Companies.FirstOrDefault(x => x.ID == CompanyID);
            if (company != null)
            {
                return company.Description;
            }
            else
            {
                return " ";
            }
        }

        public Company ViewDetail(int id)
        {
            return db.Companies.Find(id);
        }

        public int CountCompanies()
        {
            return db.Companies.Count();
        }

        public bool ChangeStatus(long id)
        {
            var company = db.Companies.Find(id);
            company.Status = !company.Status;

            db.SaveChanges();

            return (bool)company.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var job = db.Companies.Find(id);
                db.Companies.Remove(job);
                db.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
