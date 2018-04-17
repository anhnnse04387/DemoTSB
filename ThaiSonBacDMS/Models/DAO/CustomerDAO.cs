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

        public bool addCustomer(string cus_name, int? media_ID, string address, string deliver_address, string phone,
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
                return true;
            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }
    }
}
