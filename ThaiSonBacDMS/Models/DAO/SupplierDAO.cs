using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class SupplierDAO
    {

        private ThaiSonBacDMSDbContext db = null;

        public SupplierDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public List<Supplier> getSupplier()
        {
            return db.Suppliers.ToList();
        }

        public Supplier getSupplierById(int? id)
        {
            var query = from s in getSupplier()
                        where s.Supplier_ID == id && s.Status == 1
                        select s;
            return query.SingleOrDefault();
        }

    }
}
