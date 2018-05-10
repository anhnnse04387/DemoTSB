using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class OrderPartDAO
    {

        private ThaiSonBacDMSDbContext db = null;

        public OrderPartDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public String createOrderPart(Order_part orderPart)
        {
            db.Order_part.Add(orderPart);
            if (db.SaveChanges() > 0)
            {
                return orderPart.Order_part_ID;
            }
            return null;
        }

        public void updateOrderPart(Order_part part, bool takeInvoice, bool takeBallot)
        {
            var p = db.Order_part.Where(x => x.Order_part_ID.Equals(part.Order_part_ID)).SingleOrDefault();
            p.Shiper_ID = part.Shiper_ID;
            p.Driver_ID = part.Driver_ID;
            p.DeliverMethod_ID = part.DeliverMethod_ID;
            if (takeInvoice)
            {
                p.Date_take_invoice = DateTime.Now;
            }
            if (takeBallot)
            {
                p.Date_take_ballot = DateTime.Now;
            }
            db.SaveChanges();
        }

        public List<Order_part> getByCreatedDate(DateTime beginDate, DateTime endDate)
        {
            return db.Order_part.Where(x => x.Date_created >= beginDate && x.Date_created <= endDate).ToList();
        }

        public Order_part getByName(string orderPartName)
        {
            return db.Order_part.Where(x => x.Order_part_ID == orderPartName).SingleOrDefault();
        }

        public List<Order_part> getAllOrderPartByStatus(int status_ID)
        {
            return db.Order_part.Where(x => x.Status_ID == status_ID).ToList();
        }

        public List<Order_part> getAllOrderPart()
        {
            return db.Order_part.ToList();
        }

        public Order_part getOrderPart(String id)
        {
            return db.Order_part.Where(x => x.Order_part_ID.Equals(id)).SingleOrDefault();
        }
    }
}
