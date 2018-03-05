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

    }
}
