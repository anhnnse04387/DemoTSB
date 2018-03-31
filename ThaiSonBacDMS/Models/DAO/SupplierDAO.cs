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
<<<<<<< HEAD

        private ThaiSonBacDMSDbContext db = null;

=======
        private ThaiSonBacDMSDbContext db = null;
>>>>>>> 9be8fbbbcdae3c41b4c3adb9b5908fb4ffaec433
        public SupplierDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }
<<<<<<< HEAD

=======
>>>>>>> 9be8fbbbcdae3c41b4c3adb9b5908fb4ffaec433
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
