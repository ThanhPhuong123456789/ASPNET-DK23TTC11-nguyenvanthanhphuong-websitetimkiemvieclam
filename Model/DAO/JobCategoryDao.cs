using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.DAO
{
    public class JobCategoryDao
    {
        private readonly JobsFinderDBContext db = null;
        public JobCategoryDao()
        {
            db = new JobsFinderDBContext();
        }
        public long Insert(JobCategory entity)
        {
            if (entity.Status == null)
            {
                entity.Status = false;
            }
            if (entity.CreatedDate == null)
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
            db.JobCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(JobCategory entity)
        {
            try
            {
                var jobCategory = db.JobCategories.Find(entity.ID);
                jobCategory.Name = entity.Name;
                jobCategory.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
            
        }

        public IPagedList<JobCategory> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<JobCategory> model = db.JobCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public List<JobCategory> ListAll()
        {
            return db.JobCategories.Where(x => x.Status == true).ToList();
        }

        public List<JobCategory> GetOption()
        {
            return db.JobCategories.Take(8).ToList();
        }
        public JobCategory GetByID(string jobCategoryName)
        {
            return db.JobCategories.SingleOrDefault(x => x.Name == jobCategoryName);
        }
        public JobCategory ViewDetail(int id)
        {
            return db.JobCategories.Find(id);
        }

        public bool ChangeStatus(long id)
        {
            var jobCategory = db.JobCategories.Find(id);
            jobCategory.Status = !jobCategory.Status;

            db.SaveChanges();

            return (bool)jobCategory.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var jobCategory = db.JobCategories.Find(id);
                db.JobCategories.Remove(jobCategory);
                db.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }
        public string GetName(long? categoryID)
        {
            var category = db.JobCategories.FirstOrDefault(x => x.ID == categoryID);
            if (category != null)
            {
                return category.Name;
            }
            else
            {
                return "Không chứa danh mục";
            }
        }
    }
}
