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

        public Order_total getOrder(String orderId)
        {
            return db.Order_total.Where(x => x.Order_ID.Equals(orderId)).SingleOrDefault();
        }

        public void checkOut(String orderId, String userId)
        {
            (from o in db.Order_total where o.Order_ID.Equals(orderId) select o).ToList().ForEach(x => x.Status_ID = 7);
            (from op in db.Order_part where op.Order_ID.Equals(orderId) select op).ToList().ForEach(x => x.Status_ID = 7);
            var status = (from os in db.Order_detail_status where os.Order_ID.Equals(orderId) select os).ToList();
            foreach (Order_detail_status s in status)
            {
                s.Status_ID = 7;
                s.User_ID = userId;
                s.Date_change = DateTime.Now;
            }
            db.SaveChanges();
        }

        public void cancelOrder(String orderId, String reason, String userId)
        {
            var order = (from o in db.Order_total where o.Order_ID.Equals(orderId) select o).SingleOrDefault();
            order.Status_ID = 8;
            order.Note = reason;
            var part = (from op in db.Order_part where op.Order_ID.Equals(orderId) select op).ToList();
            foreach(Order_part p in part)
            {
                p.Status_ID = 8;
                p.Note = reason;
            }
            var status = (from os in db.Order_detail_status where os.Order_ID.Equals(orderId) select os).ToList();
            foreach(Order_detail_status s in status)
            {
                s.Status_ID = 8;
                s.User_ID = userId;
                s.Date_change = DateTime.Now;
            }
            db.SaveChanges();
        }

        public List<Order_total> getOrderByDateCreated(DateTime date)
        {
            return db.Order_total.Where(s => s.Date_created >= date).ToList();
        }
    }
}
