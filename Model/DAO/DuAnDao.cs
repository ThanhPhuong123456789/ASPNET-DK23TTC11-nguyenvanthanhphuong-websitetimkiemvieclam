using Model.EF;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class DuAnDao
    {
        private readonly JobsFinderDBContext db = null;
        public DuAnDao()
        {
            db = new JobsFinderDBContext();
        }

        public bool Insert(DuAn entity)
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
                db.DuAns.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực hiện SaveChanges(): " + ex.InnerException.Message);
                return false;
            }
        }

        public bool Update(DuAn entity)
        {
            try
            {
                var duAn = db.DuAns.Find(entity.ID);
                duAn.TenDuAn = entity.TenDuAn;
                duAn.TenKhachHang = entity.TenKhachHang;
                duAn.SoThanhVien = entity.SoThanhVien;
                duAn.ViTri = entity.ViTri;
                duAn.NhiemVu = entity.NhiemVu;
                duAn.CongNgheSuDung = entity.CongNgheSuDung;
                duAn.ThangBatDau = entity.ThangBatDau;
                duAn.NamBatDau = entity.NamBatDau;
                duAn.ThangKetThuc = entity.ThangKetThuc;
                duAn.NamKetThuc = entity.NamKetThuc;
                duAn.MoTaChiTiet = entity.MoTaChiTiet;
                duAn.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(DuAn entity)
        {
            try
            {
                var duAn = db.DuAns.Find(entity.ID);
                if (duAn != null)
                {
                    db.DuAns.Remove(duAn);
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

        public List<DuAn> ListAll(long? id)
        {
            return db.DuAns.Where(x => x.Status == true && x.UserID == id).ToList();
        }

        public DuAn ViewDetail(long id)
        {
            return db.DuAns.Find(id);
        }
    }
}
