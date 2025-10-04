using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class SanPhamDao
    {
        private readonly JobsFinderDBContext db = null;
        public SanPhamDao()
        {
            db = new JobsFinderDBContext();
        }

        public bool Insert(SanPham entity)
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
                db.SanPhams.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực hiện SaveChanges(): " + ex.InnerException.Message);
                return false;
            }
        }

        public bool Update(SanPham entity)
        {
            try
            {
                var sanPham = db.SanPhams.Find(entity.ID);
                sanPham.TenSanPham = entity.TenSanPham;
                sanPham.TheLoai = entity.TheLoai;
                sanPham.ThangHoanThanh = entity.ThangHoanThanh;
                sanPham.NamHoanThanh = entity.NamHoanThanh;
                sanPham.MoTaChiTiet = entity.MoTaChiTiet;
                sanPham.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(SanPham entity)
        {
            try
            {
                var sanPham = db.SanPhams.Find(entity.ID);
                if(sanPham != null)
                {
                    db.SanPhams.Remove(sanPham);
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

        public List<SanPham> ListAll(long? id)
        {
            return db.SanPhams.Where(x => x.Status == true && x.UserID == id).ToList();
        }

        public  SanPham ViewDetail(long id)
        {
            return db.SanPhams.Find(id);
        }
    }
}
