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

    }
}
