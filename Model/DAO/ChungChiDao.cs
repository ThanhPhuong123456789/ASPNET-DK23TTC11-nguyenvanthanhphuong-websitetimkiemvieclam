using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ChungChiDao
    {
        private readonly JobsFinderDBContext db = null;
        public ChungChiDao()
        {
            db = new JobsFinderDBContext();
        }

        public bool Insert(ChungChi entity)
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
                db.ChungChis.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thực hiện SaveChanges(): " + ex.InnerException.Message);
                return false;
            }
        }

        public bool Update(ChungChi entity)
        {
            try
            {
                var chungChi = db.ChungChis.Find(entity.ID);
                chungChi.TenChungChi = entity.TenChungChi;
                chungChi.ToChuc = entity.ToChuc;
                chungChi.ThangXacThuc = entity.ThangXacThuc;
                chungChi.NamXacThuc = entity.NamXacThuc;
                chungChi.ThangHetHan = entity.ThangHetHan;
                chungChi.NamHetHan = entity.NamHetHan;
                chungChi.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool Delete(ChungChi entity)
        {
            try
            {
                var chungChi = db.ChungChis.Find(entity.ID);
                if(chungChi != null)
                {
                    db.ChungChis.Remove(chungChi);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception) { return false; }
        }

        public List<ChungChi> ListAll(long? id)
        {
            return db.ChungChis.Where(x => x.Status == true && x.UserID == id).ToList();
        }

        public ChungChi  ViewDetail(long id)
        {
            return db.ChungChis.Find(id);
        }
    }
}
