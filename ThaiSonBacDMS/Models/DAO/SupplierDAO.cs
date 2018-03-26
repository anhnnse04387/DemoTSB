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
        public List<Supplier> getLstSupplier()
        {
            return db.Suppliers.Where(x => x.Status == 1).ToList();
        }
        public string getSupplierName(int supplierId)
        {
            return db.Suppliers.SingleOrDefault(x => x.Supplier_ID == supplierId && x.Status == 1).Supplier_name;
        }
        
    }
}
