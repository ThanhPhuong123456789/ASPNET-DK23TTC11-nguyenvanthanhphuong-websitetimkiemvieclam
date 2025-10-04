using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class KhoaHocDao
    {
        private readonly JobsFinderDBContext db = null;
        public KhoaHocDao()
        {
            db = new JobsFinderDBContext();
        }

        public bool Insert(KhoaHoc entity)
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
                db.KhoaHocs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực hiện SaveChanges(): " + ex.InnerException.Message);
                return false;
            }
        }

        public bool Update(KhoaHoc entity)
        {
            try
            {
                var khoaHoc = db.KhoaHocs.Find(entity.ID);
                khoaHoc.TenKhoaHoc = entity.TenKhoaHoc;
                khoaHoc.ToChuc = entity.ToChuc;
                khoaHoc.ThangBatDau = entity.ThangBatDau;
                khoaHoc.NamBatDau = entity.NamBatDau;
                khoaHoc.ThangKetThuc = entity.ThangKetThuc;
                khoaHoc.ThangKetThuc = entity.ThangKetThuc;
                khoaHoc.MoTaChiTiet = entity.MoTaChiTiet;
                khoaHoc.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(KhoaHoc entity)
        {
            try
            {
                var khoaHoc = db.KhoaHocs.Find(entity.ID);
                if(khoaHoc != null)
                {
                    db.KhoaHocs.Remove(khoaHoc);
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

        public List<KhoaHoc> ListAll(long? id)
        {
            return db.KhoaHocs.Where(x => x.Status == true && x.UserID == id).ToList();
        }

        public KhoaHoc ViewDetail(long id)
        {
            return db.KhoaHocs.Find(id);
        }
    }
}
