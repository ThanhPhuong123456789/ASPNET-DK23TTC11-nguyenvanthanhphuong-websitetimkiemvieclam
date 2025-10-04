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
    public class CareerDao
    {
        readonly JobsFinderDBContext db = null;
        public CareerDao()
        {
            db = new JobsFinderDBContext();
        }
        public long Insert(Career entity)
        {
            if(entity.Status == null)
            {
                entity.Status = false;
            }
            if(entity.CreatedDate == null)
            {
                entity.CreatedDate = DateTime.Now;
            }
            db.Careers.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Career entity)
        {
            try
            {
                var career = db.Careers.Find(entity.ID);
                career.Name = entity.Name;
                career.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
            
        }

        public IPagedList<Career> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Career> model = db.Careers;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
        public List<Career> ListAll()
        {
            return db.Careers.ToList();
        }
        public List<Career> GetOption()
        {
            return db.Careers.Take(12).ToList();
        }
        public Career GetByID(string careerName)
        {
            return db.Careers.SingleOrDefault(x => x.Name == careerName);
        }
        public Career ViewDetail(int id)
        {
            return db.Careers.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var career = db.Careers.Find(id);
                db.Careers.Remove(career);
                db.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
