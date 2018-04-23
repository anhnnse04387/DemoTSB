using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class OrderDetailStatusDAO
    {

        private ThaiSonBacDMSDbContext db = null;

        public OrderDetailStatusDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }

        public int createOrderStatus(Order_detail_status orderStatus)
        {
            orderStatus.ID = db.Order_detail_status.Count() + 1;
            db.Order_detail_status.Add(orderStatus);
            if (db.SaveChanges() > 0)
            {
                return orderStatus.ID;
            }
            return 0;
        }

        public List<Order_detail_status> getStatus(String orderPartId)
        {
            return db.Order_detail_status.Where(x => x.Order_part_ID.Equals(orderPartId)).ToList();
        }

    }
}
