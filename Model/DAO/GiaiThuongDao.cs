using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class GiaiThuongDao
    {
        private readonly JobsFinderDBContext db = null;
        public GiaiThuongDao()
        {
            db = new JobsFinderDBContext();
        }

        public bool Insert(GiaiThuong entity)
        {
            if (entity.Status == null)
            {
                entity.Status = true;
            }
            if (entity.CreatedDate == null)
            {
                entity.CreatedDate = DateTime.Now;
            }
            try
            {
                db.GiaiThuongs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực hiện SaveChanges(): " + ex.InnerException.Message);
                return false;
            }
        }

        public bool Update(GiaiThuong entity)
        {
            try
            {
                var giaiThuong = db.GiaiThuongs.Find(entity.ID);
                giaiThuong.TenGiaiThuong = entity.TenGiaiThuong;
                giaiThuong.ToChuc = entity.ToChuc;
                giaiThuong.ThangNhan = entity.ThangNhan;
                giaiThuong.NamNhan = entity.NamNhan;
                giaiThuong.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(GiaiThuong entity)
        {
            try
            {
                var giaiThuong = db.GiaiThuongs.Find(entity.ID);
                if(giaiThuong != null)
                {
                    db.GiaiThuongs.Remove(giaiThuong);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<GiaiThuong> ListAll(long? id)
        {
            return db.GiaiThuongs.Where(x => x.Status == true && x.UserID == id).ToList();
        }

        public  GiaiThuong ViewDetail(long id)
        {
            return db.GiaiThuongs.Find(id);
        }
    }
}
