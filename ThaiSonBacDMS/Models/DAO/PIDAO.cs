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

        public Purchase_invoice getPI(int id)
        {
            return db.Purchase_invoice.Where(x => x.Purchase_invoice_ID == id).SingleOrDefault();
        }

    }
}
