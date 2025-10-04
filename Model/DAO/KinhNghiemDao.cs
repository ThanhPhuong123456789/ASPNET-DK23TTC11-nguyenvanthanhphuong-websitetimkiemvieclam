using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class KinhNghiemDao
    {
        private readonly JobsFinderDBContext db = null;
        public KinhNghiemDao()
        {
            db = new JobsFinderDBContext();
        }

        public bool Insert(KinhNghiem entity)
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
                db.KinhNghiems.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực hiện SaveChanges(): " + ex.InnerException.Message);
                return false;
            }
        }

        public bool Update(KinhNghiem entity)
        {
            try
            {
                var kinhNghiem = db.KinhNghiems.Find(entity.ID);
                kinhNghiem.CongTy = entity.CongTy;
                kinhNghiem.ChucVu = entity.ChucVu;
                kinhNghiem.ThangBatDau = entity.ThangBatDau;
                kinhNghiem.NamBatDau = entity.NamBatDau;
                kinhNghiem.ThangKetThuc = entity.ThangKetThuc;
                kinhNghiem.NamKetThuc = entity.NamKetThuc;
                kinhNghiem.MoTaChiTiet = entity.MoTaChiTiet;
                kinhNghiem.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(KinhNghiem entity)
        {
            try
            {
                var kinhNghiem = db.KinhNghiems.Find(entity.ID);
                if(kinhNghiem != null)
                {
                    db.KinhNghiems.Remove(kinhNghiem);
                    db.SaveChanges();
                    return true;
                } else
                {
                    return false;
                }
            }
            catch (Exception)
            { 
                return false;
            }
        }

        public List<KinhNghiem> ListAll(long? id)
        {
            return db.KinhNghiems.Where(x => x.Status == true && x.UserID == id).ToList();
        }

        public KinhNghiem ViewDetail(long id)
        {
            return db.KinhNghiems.Find(id);
        }
    }
}
