using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class PIDAO
    {

        private ThaiSonBacDMSDbContext db = null;

        public PIDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public List<Purchase_invoice> getLstPI()
        {
            return db.Purchase_invoice.ToList();
        }

    }
}
