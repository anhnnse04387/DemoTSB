using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class RoleDetailDAO
    {
        public ThaiSonBacDMSDbContext db = null;

        public RoleDetailDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }
        public List<Role_detail> lstAllRole()
        {
            return db.Role_detail.ToList();
        }
    }
}
