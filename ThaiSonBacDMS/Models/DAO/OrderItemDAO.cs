using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class OrderItemDAO
    {

        private ThaiSonBacDMSDbContext db = null;

        public OrderItemDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public void createOrderItem(Order_items orderItem)
        {
            orderItem.ID = db.Order_items.Count() + 1;
            db.Order_items.Add(orderItem);
            db.SaveChanges();
        }

        public List<Order_items> getOrderItem(String orderId)
        {
            return db.Order_items.Where(x => x.Order_ID.Equals(orderId)).Where(x => x.Order_part_ID == null).ToList();
        }

    }
}
