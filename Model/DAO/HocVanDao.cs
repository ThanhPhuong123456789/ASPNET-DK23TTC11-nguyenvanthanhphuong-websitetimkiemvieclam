using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class HocVanDao
    {
        private readonly JobsFinderDBContext db = null;
        public HocVanDao()
        {
            db = new JobsFinderDBContext();
        }

        public bool Insert(HocVan entity)
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
                db.HocVans.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực hiện SaveChanges(): " + ex.InnerException.Message);
                return false;
            }
        }

        public bool Update(HocVan entity)
        {
            try
            {
                var hocVan = db.HocVans.Find(entity.ID);
                hocVan.Truong = entity.Truong;
                hocVan.ChuyenNganh = entity.ChuyenNganh;
                hocVan.ThangBatDau = entity.ThangBatDau;
                hocVan.NamBatDau = entity.NamBatDau;
                hocVan.ThangKetThuc = entity.ThangKetThuc;
                hocVan.NamKetThuc = entity.NamKetThuc;
                hocVan.MoTaChiTiet = entity.MoTaChiTiet;
                hocVan.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(HocVan entity)
        {
            try
            {
                var hocVan = db.HocVans.Find(entity.ID);
                if (hocVan != null)
                {
                    db.HocVans.Remove(hocVan);
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


        public List<HocVan> ListAll(long? id)
        {
            return db.HocVans.Where(x => x.Status == true && x.UserID == id).ToList();
        }

        public HocVan ViewDetail(long id)
        {
            return db.HocVans.Find(id);
        }
    }
}
