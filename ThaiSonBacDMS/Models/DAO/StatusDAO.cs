using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class StatusDAO
    {

        private ThaiSonBacDMSDbContext db = null;
        public StatusDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }
        public String getStatus(byte? id)
        {
            return db.Status.Where(x => x.Status_ID == id).SingleOrDefault().Status_name;
        }

    }
}
