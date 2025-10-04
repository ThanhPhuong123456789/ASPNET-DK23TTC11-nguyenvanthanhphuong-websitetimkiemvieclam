using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class KyNangDao
    {
        private readonly JobsFinderDBContext db = null;
        public KyNangDao()
        {
            db = new JobsFinderDBContext();
        }

        public bool Insert(KyNang entity)
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
                db.KyNangs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực hiện SaveChanges(): " + ex.InnerException.Message);
                return false;
            }
        }

        public bool Update(KyNang entity)
        {
            try
            {
                var kyNang = db.KyNangs.Find(entity.ID);
                kyNang.TenKyNang = entity.TenKyNang;
                kyNang.DanhGia = entity.DanhGia;
                kyNang.MoTaChiTiet = entity.MoTaChiTiet;
                kyNang.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool Delete(KyNang entity)
        {
            try
            {
                var kyNang = db.KyNangs.Find(entity.ID);
                if(kyNang != null)
                {
                    db.KyNangs.Remove(kyNang);
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

        public List<KyNang> ListAll(long? id)
        {
            return db.KyNangs.Where(x => x.Status == true && x.UserID == id).ToList();
        }

        public KyNang ViewDetail(long id)
        {
            return db.KyNangs.Find(id);
        }
    }
}
