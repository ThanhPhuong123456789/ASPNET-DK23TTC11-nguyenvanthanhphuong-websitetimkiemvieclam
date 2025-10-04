using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class BlogDao
    {
        readonly JobsFinderDBContext db = null;
        public BlogDao()
        {
            db = new JobsFinderDBContext();
        }

        public long Insert(Blog entity)
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
            db.Blogs.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Blog entity)
        {
            try
            {
                var blog = db.Blogs.Find(entity.ID);
                blog.Name = entity.Name;
                blog.Image = entity.Image;
                blog.CategoryID =  entity.CategoryID;
                blog.ModifiedDate = DateTime.Now;
                blog.Status = entity.Status;
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public IPagedList<Blog> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Blog> model = db.Blogs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public Blog GetById(long id)
        {
            return db.Blogs.Find(id);
        }
        public Blog ViewDetail(int id)
        {
            return db.Blogs.Find(id);
        }
        public bool Delete(int  id)
        {
            try
            {
                var blog = db.Blogs.Find(id);
                db.Blogs.Remove(blog);
                db.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }
        public bool ChangeStatus(int id)
        {
            var blog = db.Blogs.Find(id);
            blog.Status = !blog.Status;

            db.SaveChanges();

            return (bool)blog.Status;
        }
    }
}
