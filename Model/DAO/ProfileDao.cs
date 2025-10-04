using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProfileDao
    {
        private readonly JobsFinderDBContext db = null;

        public ProfileDao()
        {
            db = new JobsFinderDBContext();
        }

        public Profile GetByID(long? userID)
        {
            return db.Profiles.SingleOrDefault(x => x.UserID == userID);
        }

        public long Insert(Profile entity)
        {
            db.Profiles.Add(entity);
            db.SaveChanges();
            return entity.UserID;
        }

        public bool Update(Profile entity)
        {
            var profile = db.Profiles.FirstOrDefault(p => p.UserID == entity.UserID);
            if (profile != null)
            {
                profile.HoVaTen = entity.HoVaTen;
                profile.AnhCaNhan = entity.AnhCaNhan;
                profile.GioiTinh = entity.GioiTinh;
                profile.NgaySinh = entity.NgaySinh;
                profile.ThangSinh = entity.ThangSinh;
                profile.NamSinh = entity.NamSinh;
                profile.DiaChiHienTai = entity.DiaChiHienTai;
                profile.Email = entity.Email;
                profile.SoDienThoai = entity.SoDienThoai;
                profile.GioiThieu = entity.GioiThieu;

                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Profile> ListAll(long id)
        {
            return db.Profiles.Where(x => x.UserID == id ).ToList();
        }

        public string GetAvatar(long? UserID)
        {
            var user = db.Profiles.FirstOrDefault(x => x.UserID == UserID);
            if (user != null)
            {
                if (UserID != null && user.AnhCaNhan != null)
                {
                    return user.AnhCaNhan;
                }
                else
                {
                    return "./Assets/Client/JobsFinder/img/Logo.png";
                }
            }
            else
            {
                return null;
            }

        }

        public Profile ViewDetail(long? id)
        {
            return db.Profiles.Find(id);
        }
    }
}
