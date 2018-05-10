using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class CustomerDAO
    {

        private ThaiSonBacDMSDbContext db = null;

        public CustomerDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public List<Customer> getCustomer()
        {
            return db.Customers.ToList();
        }

        public Customer getCustomerById(int id)
        {
            var query = from e in getCustomer()
                        where e.Customer_ID == id && e.Status == 1
                        select e;
            return query.SingleOrDefault();
        }

        public List<Customer> getCustomerByDateCreated(DateTime dateBegin, DateTime dateEnd)
        {
            return db.Customers.Where(s => s.Date_Created >= dateBegin && s.Date_Created <= dateEnd).ToList();
        }

        public List<Customer> getTop10CustomerDebt()
        {
            return db.Customers.OrderBy(s => s.Current_debt).Where(s => s.Status == 1).Take(10).ToList();
        }

        public int addCustomer(string cus_name, int? media_ID, string address, string deliver_address, string phone,
            string mail, string tax_code)
        {
            try
            {
                Customer cus = new Customer();
                cus.Customer_name = cus_name;
                cus.Media_ID = media_ID;
                cus.Delivery_address = address;
                cus.Export_invoice_address = deliver_address;
                cus.Phone = phone;
                cus.Mail = mail;
                cus.Tax_code = tax_code;
                cus.Rank = "Khách mới";
                cus.Date_Created = DateTime.Now;
                cus.Current_debt = 0;
                cus.Status = 1;
                db.Customers.Add(cus);
                db.SaveChanges();
                return cus.Customer_ID;
            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return -1;
            }
        }

        public int editCustomer(string cus_name, int? media_ID, string address, string deliver_address, string phone,
            string mail, string tax_code, int customerID, string acronym)
        {
            try
            {
                var query = db.Customers.SingleOrDefault(x=>x.Customer_ID == customerID);
                query.Customer_name = cus_name;
                query.Acronym = acronym;
                query.Media_ID = media_ID;
                query.Delivery_address = address;
                query.Export_invoice_address = deliver_address;
                query.Phone = phone;
                query.Mail = mail;
                query.Tax_code = tax_code;
                db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return -1;
            }
        }

        public bool checkExitsMail(string mail)
        {
            return db.Customers.Where(x=>x.Mail == mail).Any();
        }
    }
}
