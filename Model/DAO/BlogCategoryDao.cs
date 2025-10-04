using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class BlogCategoryDao
    {
        readonly JobsFinderDBContext db = null;
        public BlogCategoryDao()
        {
            db = new JobsFinderDBContext();
        }
        public long Insert(BlogCategory entity)
        {
            if (entity.Status == null)
            {
                entity.Status = false;
            }
            if(entity.CreatedDate  == null)
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
            db.BlogCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(BlogCategory entity)
        {
            try
            {
                var blogCategory = db.BlogCategories.Find(entity.ID);
                blogCategory.Name = entity.Name;
                blogCategory.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public IPagedList<BlogCategory> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<BlogCategory> model = db.BlogCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public List<BlogCategory> ListAll()
        {
            return db.BlogCategories.Where(x => x.Status == true).ToList();
        }
        public BlogCategory GetByID(string blogCategoryName)
        {
            return db.BlogCategories.SingleOrDefault(x => x.Name == blogCategoryName);
        }
        public BlogCategory ViewDetail(int id)
        {
            return db.BlogCategories.Find(id);
        }

        public bool ChangeStatus(long id)
        {
            var blogCategory = db.BlogCategories.Find(id);
            blogCategory.Status = !blogCategory.Status;

            db.SaveChanges();

            return (bool)blogCategory.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var blogCategory = db.BlogCategories.Find(id);
                db.BlogCategories.Remove(blogCategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
