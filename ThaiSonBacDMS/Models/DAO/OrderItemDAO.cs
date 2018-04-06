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
            return db.Order_items.Where(x => x.Order_ID.Equals(orderId) && x.Order_part_ID == null).ToList();
        }

        public List<Order_items> getReturnOrderItem(String orderId)
        {
            return db.Order_items.Where(x => x.Order_ID.Equals(orderId) && x.Order_part_ID != null && x.Order_part.Status_ID == 9).ToList();
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

        public Dictionary<string, int> getTopSellingProductInMonth(DateTime beginDate, DateTime endDate)
        {
            var query = from oi in db.Order_items
                        join p in db.Products on oi.Product_ID equals p.Product_ID
                        join ot in db.Order_total on oi.Order_ID equals ot.Order_ID
                        where ot.Date_created >= beginDate && ot.Date_created <= endDate 
                        && oi.Order_part_ID == null
                        select new
                        {
                            orderItemId = oi.ID,
                            productName = p.Product_name,
                            quantity = oi.Quantity
                        };
            Dictionary<string, int> topSellingProductInMonth = new Dictionary<string, int>();
            if(query != null)
            {
                foreach (var item in query)
                {
                    if (topSellingProductInMonth.ContainsKey(item.productName))
                    {
                        topSellingProductInMonth[item.productName] += (int)item.quantity;
                    }
                    else
                    {
                        topSellingProductInMonth.Add(item.productName, (int)item.quantity);
                    }
                }
                topSellingProductInMonth = topSellingProductInMonth.Take(10).OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            } 
            return topSellingProductInMonth;
        }

        public Dictionary<string, int> getTopSellingCategory(DateTime beginDate, DateTime endDate)
        {
            var query = from oi in db.Order_items
                        join p in db.Products on oi.Product_ID equals p.Product_ID
                        join c in db.Categories on p.Category_ID equals c.Category_ID
                        join ot in db.Order_total on oi.Order_ID equals ot.Order_ID
                        where oi.Order_part_ID == null 
                        && ot.Date_created >= beginDate && ot.Date_created <= endDate
                        select new
                        {
                            category = c,
                            quantity = oi.Quantity
                        };
            Dictionary<string, int> topSellingCategory = new Dictionary<string, int>();
            if (query != null)
            {
                foreach (var item in query)
                {
                    if (topSellingCategory.ContainsKey(item.category.Category_ID))
                    {
                        topSellingCategory[item.category.Category_ID] += (int)item.quantity;
                    }
                    else
                    {
                        topSellingCategory.Add(item.category.Category_ID, (int)item.quantity);
                    }
                }
                topSellingCategory = topSellingCategory.Take(5).OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            }
            return topSellingCategory;
        }

        public int getCategoryQuantityByDate(string cateID, DateTime beginDate, DateTime endDate)
        {
            var query = from oi in db.Order_items
                        join p in db.Products on oi.Product_ID equals p.Product_ID
                        join c in db.Categories on p.Category_ID equals c.Category_ID
                        join ot in db.Order_total on oi.Order_ID equals ot.Order_ID
                        where oi.Order_part_ID == null && c.Category_ID == cateID
                        && ot.Date_created >= beginDate && ot.Date_created <= endDate
                        select new
                        {
                            quantity = oi.Quantity
                        };
            var total = 0;
            foreach(var item in query)
            {
                if(item == null)
                {
                    total += 0;
                }else
                {
                    total += (int)item.quantity;
                }
            }
            return total;
        }
        //thuongtx
        public List<Order_items> getLstOrderItems(int productId)
        {
            List<Order_items> lstOrderItem = new List<Order_items>();
            var query = from oi in db.Order_items
                        join od in db.Order_total on oi.Order_ID equals od.Order_ID
                        where oi.Product_ID == productId
                        select oi;
            if (query.Count() != 0)
            {
                foreach(var orderItem in query)
                {
                    lstOrderItem.Add(orderItem);
                }
            }
            
            return lstOrderItem;
        }
    }
}
