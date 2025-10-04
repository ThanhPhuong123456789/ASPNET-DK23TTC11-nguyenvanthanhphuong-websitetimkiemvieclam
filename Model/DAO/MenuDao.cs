using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class MenuDao
    {
        private readonly JobsFinderDBContext db = null;
        public MenuDao()
        {
            db = new JobsFinderDBContext();
        }

        public List<Menu> ListByGroupId(int groupId)
        {
            return db.Menus.Where(x => x.TypeID == groupId && x.Status  == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
