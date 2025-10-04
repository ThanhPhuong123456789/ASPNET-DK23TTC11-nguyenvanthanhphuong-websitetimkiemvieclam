using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class HoatDongDao
    {
        private readonly JobsFinderDBContext db = null;
        public HoatDongDao()
        {
            db = new JobsFinderDBContext();
        }

        public bool Insert(HoatDong entity)
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
                db.HoatDongs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực hiện SaveChanges(): " + ex.InnerException.Message);
                return false;
            }
        }

        public bool Update(HoatDong entity)
        {
            try
            {
                var hoatDong = db.HoatDongs.Find(entity.ID);
                hoatDong.TenHoatDong = entity.TenHoatDong;
                hoatDong.ViTriThamGia = entity.ViTriThamGia;
                hoatDong.ThangThamGia = entity.ThangThamGia;
                hoatDong.NamThamGia = entity.NamThamGia;
                hoatDong.ThangKetThuc = entity.ThangKetThuc;
                hoatDong.NamKetThuc = entity.NamKetThuc;
                hoatDong.MoTaChiTiet = entity.MoTaChiTiet;
                hoatDong.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(HoatDong entity)
        {
            try
            {
                var hoatDong = db.HoatDongs.Find(entity.ID);
                if(hoatDong != null)
                {
                    db.HoatDongs.Remove(hoatDong);
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

        public List<HoatDong> ListAll(long? id)
        {
            return db.HoatDongs.Where(x => x.Status == true && x.UserID == id).ToList();
        }

        public HoatDong ViewDetail(long id)
        {
            return db.HoatDongs.Find(id);
        }
    }
}
