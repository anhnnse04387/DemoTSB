using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class OfficeDAO
    {
        private ThaiSonBacDMSDbContext db = null;
        public OfficeDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }
        public List<Office> getListOffice()
        {
            return db.Offices.ToList();
        }
    }
   
}
