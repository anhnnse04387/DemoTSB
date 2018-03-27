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
            db.Order_items.Add(orderItem);
            db.SaveChanges();
        }

        public List<Order_items> getOrderItem(String orderId)
        {
            return db.Order_items.Where(x => x.Order_ID.Equals(orderId)).Where(x => x.Order_part_ID == null).ToList();
        }

        public int countNumberProductSoldMonth(DateTime dateBegin, DateTime dateEnd)
        {
            int numberProduct = 0;
            var query = from oi in db.Order_items
                        join ot in db.Order_total on oi.Order_ID equals ot.Order_ID
                        where ot.Date_created >= dateBegin && ot.Date_created <= dateEnd
                        select new
                        {
                            quantityProduct = oi.Quantity
                        };
            foreach (var item in query)
            {
                if (item.quantityProduct == null)
                {
                    numberProduct += 0;
                }
                else
                {
                    numberProduct += (int)item.quantityProduct;
                }
            }
            return numberProduct;
        }

        public int countNumberProduct(string order_id)
        {
            int numberProduct = 0;
            List<Order_items> lstOrderItem = db.Order_items.
                Where(x => x.Order_ID == order_id && x.Order_part_ID == null).ToList();
            foreach (var item in lstOrderItem)
            {
                if(item.Quantity == null)
                {
                    numberProduct += 0;
                }else
                {
                    numberProduct += (int) item.Quantity;
                }
            }
            return numberProduct;
        }

        public int countNumberBox(string order_id)
        {
            int numberBox = 0;
            List<Order_items> lstOrderItem = db.Order_items.
                Where(x => x.Order_ID == order_id && x.Order_part_ID == null).ToList();
            foreach (var item in lstOrderItem)
            {
                if (item.Box == null)
                {
                    numberBox += 0;
                }
                else
                {
                    numberBox += (int)item.Box;
                }
            }
            return numberBox;
        }
    }
}
