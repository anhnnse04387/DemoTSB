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

    }
}
