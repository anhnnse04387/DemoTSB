using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;

namespace Models.DAO
{
    public class OrderTotalDAO
    {

        private ThaiSonBacDMSDbContext db = null;

        public OrderTotalDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public void createOrder(Order_total order)
        {
            db.Order_total.Add(order);
            db.SaveChanges();
        }

    }
}
