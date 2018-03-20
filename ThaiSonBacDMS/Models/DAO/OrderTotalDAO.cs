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

        public String createOrder(Order_total order)
        {
            db.Order_total.Add(order);
            if (db.SaveChanges() > 0)
            {
                return order.Order_ID;
            }
            return null;
        }

        public int getNextValueID()
        {
            return db.Order_total.ToList().Count + 1;
        }

    }
}
