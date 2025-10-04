using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.DAO
{
    public class AdminDao
    {
        private readonly JobsFinderDBContext db = null;
        public AdminDao()
        {
            db = new JobsFinderDBContext();
        }
        public long Insert(Admin entity)
        {
            if (entity.Status == null)
            {
                entity.Status = false;
            }
            db.Admins.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Admin entity)
        {
            try
            {
                var admin = db.Admins.Find(entity.ID);
                admin.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    admin.Password = entity.Password;
                }
                admin.Address = entity.Address;
                admin.Email = entity.Email;
                admin.Phone = entity.Phone;
                admin.ModifiedBy = entity.ModifiedBy;
                admin.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
            
        }

        public IPagedList<Admin> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Admin> model = db.Admins;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public Admin GetByID(string userName)
        {
            return db.Admins.SingleOrDefault(x => x.UserName == userName);
        }
        public Admin ViewDetail(int id)
        {
            return db.Admins.Find(id);
        }
        public int Login(string username, string password)
        {
            var result = db.Admins.SingleOrDefault(x => x.UserName == username);
            if(result == null)
            {
                return 0;
            } else
            {
                if(result.Status == false)
                {
                    return -1;
                } else
                {
                    if(result.Password == password)
                    {
                        return 1;
                    } else
                    {
                        return -2;
                    }
                }
            }
        }
        public int CountUser()
        {
            return db.Admins.Count();
        }


        public bool ChangeStatus(long id)
        {
            var admin = db.Admins.Find(id);
            admin.Status = !admin.Status;

            db.SaveChanges();

            return (bool)admin.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var admin = db.Admins.Find(id);
                db.Admins.Remove(admin);
                db.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
