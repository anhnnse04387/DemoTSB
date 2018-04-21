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

        public List<Supplier> getLstSupplier()
        {
            return db.Suppliers.Where(x => x.Status == 1).ToList();
        }
        public string getSupplierName(int supplierId)
        {
            return db.Suppliers.SingleOrDefault(x => x.Supplier_ID == supplierId && x.Status == 1).Supplier_name;
        }

        public bool addSupplier(string supp_name, int? mediaID,string supp_address, string supp_phone,
            string supp_mail, string supp_taxCode)
        {
            try
            {
                //create new supplier
                Supplier supp = new Supplier();
                supp.Supplier_name = supp_name;
                supp.Media_ID = mediaID;
                supp.Supplier_address = supp_address;
                supp.Phone = supp_phone;
                supp.Mail = supp_mail;
                supp.Tax_code = supp_taxCode;
                supp.Current_debt = 0;
                supp.Status = 1;
                supp.Date_Created = DateTime.Now;
                //insert to db
                db.Suppliers.Add(supp);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }

        public bool editSupplier(string supp_name, int? mediaID, string supp_address, string supp_phone,
            string supp_mail, string supp_taxCode, int supplierID)
        {
            try
            {
                //find supplier
                Supplier supp = db.Suppliers.SingleOrDefault(x => x.Supplier_ID == supplierID);
                supp.Supplier_name = supp_name;
                supp.Media_ID = mediaID;
                supp.Supplier_address = supp_address;
                supp.Phone = supp_phone;
                supp.Mail = supp_mail;
                supp.Tax_code = supp_taxCode;
                //update to db
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }
        
    }
}
