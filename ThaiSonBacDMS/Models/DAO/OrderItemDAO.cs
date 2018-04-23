using Models.DAO_Model;
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
            var lst = new List<Order_items>();
            foreach(Order_part o in db.Order_part.Where(x=>x.Order_ID.Equals(orderId) && x.Status_ID == 9).ToList())
            {
                foreach(Order_items i in db.Order_items.Where(x=>x.Order_part_ID.Equals(o.Order_part_ID)))
                {
                    lst.Add(i);
                }
            }
            return lst;
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
                if (item.Quantity == null)
                {
                    numberProduct += 0;
                }
                else
                {
                    numberProduct += (int)item.Quantity;
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
            if (query != null)
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
            foreach (var item in query)
            {
                if (item == null)
                {
                    total += 0;
                }
                else
                {
                    total += (int)item.quantity;
                }
            }
            return total;
        }

        public Dictionary<DateTime, List<decimal>> getChartData(DateTime beginDate, DateTime endDate, string category_ID)
        {
            var query = from oi in db.Order_items
                        join p in db.Products on oi.Product_ID equals p.Product_ID
                        join c in db.Categories on p.Category_ID equals c.Category_ID
                        join ot in db.Order_total on oi.Order_ID equals ot.Order_ID
                        where ot.Date_created >= beginDate && ot.Date_created <= endDate
                        && oi.Order_part_ID == null
                        select new
                        {
                            productId = p.Product_ID,
                            categoryName = c.Category_name,
                            categoryID = c.Category_ID,
                            dateCreated = ot.Date_created,
                            nhapVon = p.CIF_VND * oi.Quantity,
                            xuatVon = (p.CIF_VND + p.CIF_VND * (p.VAT / 100)) * oi.Quantity,
                            banChoKhach = oi.Quantity * (p.Price_before_VAT_USD + p.Price_before_VAT_USD * (p.VAT / 100))
                        };
            if (category_ID != "-1")
            {
                query = query.Where(x => x.categoryID == category_ID);
            }
            var groupBy = from q in query
                          group q by q.dateCreated into g
                          select g;
            Dictionary<DateTime, List<decimal>> returnValue = new Dictionary<DateTime, List<decimal>>();
            foreach (var item in groupBy)
            {
                List<decimal> totalData = new List<decimal>();
                decimal totalNhapVon = 0;
                decimal totalxuatVon = 0;
                decimal totalbanChoKhach = 0;
                foreach (var i in item)
                {
                    totalNhapVon += (decimal)i.nhapVon;
                    totalxuatVon += (decimal)i.xuatVon;
                    totalbanChoKhach += (decimal)i.banChoKhach;
                }
                totalData.Add(totalNhapVon);
                totalData.Add(totalxuatVon);
                totalData.Add(totalbanChoKhach);
                returnValue.Add(item.Key, totalData);
            }
            return returnValue;
        }

        public Dictionary<string, List<decimal>> getPieChartData(DateTime beginDate, DateTime endDate)
        {
            Dictionary<string, List<decimal>> returnDic = new Dictionary<string, List<decimal>>();
            var listCate = new CategoryDAO().getLstCate();
            foreach (var item in listCate)
            {
                List<decimal> tempList = new List<decimal>();
                tempList.Add(0);
                tempList.Add(0);
                tempList.Add(0);
                tempList.Add(0);
                returnDic.Add(item.Category_name, tempList);
            }
            var query = from oi in db.Order_items
                        join p in db.Products on oi.Product_ID equals p.Product_ID
                        join c in db.Categories on p.Category_ID equals c.Category_ID
                        join ot in db.Order_total on oi.Order_ID equals ot.Order_ID
                        where ot.Date_created >= beginDate && ot.Date_created <= endDate
                        && oi.Order_part_ID == null
                        select new
                        {
                            categoryName = c.Category_name,
                            categoryID = c.Category_ID,
                            quantity = oi.Quantity,
                            nhapVon = p.CIF_VND * oi.Quantity,
                            xuatVon = (p.CIF_VND + p.CIF_VND * (p.VAT / 100)) * oi.Quantity,
                            banChoKhach = oi.Quantity * (p.Price_before_VAT_USD + p.Price_before_VAT_USD * (p.VAT / 100))
                        };
            foreach (var item in query)
            {
                if (returnDic.ContainsKey(item.categoryName))
                {
                    returnDic[item.categoryName][0] += (decimal)item.quantity;
                    returnDic[item.categoryName][1] += (decimal)item.nhapVon;
                    returnDic[item.categoryName][2] += (decimal)item.xuatVon;
                    returnDic[item.categoryName][3] += (decimal)item.banChoKhach;
                }
            }
            return returnDic;

        }

        public List<DataChiTietDoanhThu> getDataDoanhThu(DateTime beginDate, DateTime endDate, string categoryID,
            string productCode, int? numberFrom, int? numberTo, decimal? priceFrom,
            decimal? priceTo, decimal? doanhThuFrom, decimal? doanhThuTo)
        {
            List<DataChiTietDoanhThu> dataList = new List<DataChiTietDoanhThu>();
            var query = from oi in db.Order_items
                        join ot in db.Order_total on oi.Order_ID equals ot.Order_ID
                        where ot.Date_created >= beginDate && ot.Date_created <= endDate
                        && oi.Order_part_ID == null
                        group oi by oi.Product_ID into sumQuantity
                        select new
                        {
                            productID = sumQuantity.Key,
                            totalQuantity = sumQuantity.Sum(x => x.Quantity),
                        };
            var handleQuery = (from oi in db.Order_items
                               join p in db.Products on oi.Product_ID equals p.Product_ID
                               join ot in db.Order_total on oi.Order_ID equals ot.Order_ID
                               join c in db.Categories on p.Category_ID equals c.Category_ID
                               join q in query on oi.Product_ID equals q.productID
                               where ot.Date_created >= beginDate && ot.Date_created <= endDate
                               && oi.Order_part_ID == null
                               select new
                               {
                                   productCode = p.Product_code,
                                   productID = p.Product_ID,
                                   categoryID = c.Category_ID,
                                   price = p.Price_before_VAT_VND + p.Price_before_VAT_VND * (p.VAT / 100),
                                   totalQuantity = q.totalQuantity,
                                   doanhThu = q.totalQuantity * (p.Price_before_VAT_VND + p.Price_before_VAT_VND * (p.VAT / 100))
                               }).Distinct();
            if (!string.IsNullOrEmpty(productCode))
            {
                handleQuery = handleQuery.Where(x => x.productCode.Contains(productCode));
            }
            if (!string.IsNullOrEmpty(categoryID))
            {
                handleQuery = handleQuery.Where(x => x.categoryID == categoryID);
            }
            if (numberFrom != null)
            {
                handleQuery = handleQuery.Where(x => x.totalQuantity >= numberFrom);
            }
            if (numberTo != null)
            {
                handleQuery = handleQuery.Where(x => x.totalQuantity <= numberTo);
            }
            if (priceFrom != null)
            {
                handleQuery = handleQuery.Where(x => x.price >= priceFrom);
            }
            if (priceTo != null)
            {
                handleQuery = handleQuery.Where(x => x.price <= priceTo);
            }
            if (doanhThuFrom != null)
            {
                handleQuery = handleQuery.Where(x => x.doanhThu >= doanhThuFrom);
            }
            if (doanhThuTo != null)
            {
                handleQuery = handleQuery.Where(x => x.doanhThu <= doanhThuTo);
            }
            foreach (var item in handleQuery)
            {
                try
                {
                    DataChiTietDoanhThu data = new DataChiTietDoanhThu();
                    data.productName = new ProductDAO().getProductById(item.productID).Product_name
                        + "     " + new ProductDAO().getProductById(item.productID).Product_parameters;
                    data.categoryName = new CategoryDAO().getCategoryById(item.categoryID).Category_name;
                    data.price = (decimal)item.price;
                    data.totalQuantity = (int)item.totalQuantity;
                    data.doanhThu = (decimal)item.doanhThu;
                    dataList.Add(data);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }


            }
            return dataList;
        }


        public Dictionary<string, List<DataCongNoKhachHang>> getDataCongNoKhachHang(DateTime beginDate, DateTime endDate, string categoryID)
        {
            Dictionary<string, List<DataCongNoKhachHang>> listData = new Dictionary<string, List<DataCongNoKhachHang>>();
            if (categoryID == "-1") categoryID = "";
            var tested_query = from oi in db.Order_items
                               join ot in db.Order_total on oi.Order_ID equals ot.Order_ID
                               where ot.Date_created >= beginDate && ot.Date_created <= endDate
                               && oi.Order_part_ID == null
                               group oi by ot.Customer_ID into group_customer
                               from group_category in
                               (from oi in group_customer
                                join p in db.Products on oi.Product_ID equals p.Product_ID
                                where p.Category_ID.Contains(categoryID)
                                group oi by p.Category_ID)
                               group group_category by group_customer.Key;

            foreach (var item in tested_query)
            {
                try
                {
                    var listCategory = new List<DataCongNoKhachHang>();
                    foreach (var i in item)
                    {
                        DataCongNoKhachHang data = new DataCongNoKhachHang();
                        data.categoryName = new CategoryDAO().getCategoryById(i.Key).Category_name;
                        data.totalQuantity = (int)i.Sum(x => x.Quantity);
                        data.totalPrice = i.Sum(x => x.Discount == null ? (decimal)x.Price
                        : (decimal)x.Price - (decimal)x.Price * ((decimal)x.Discount / 100));
                        listCategory.Add(data);
                    }
                    var cusName = new CustomerDAO().getCustomerById(item.Key).Customer_name;
                    listData.Add(cusName, listCategory);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            return listData;
        }

        public Dictionary<string, int?> getProductCustomerBought(int customerID)
        {
            var query = (from oi in db.Order_items
                        join ot in db.Order_total on oi.Order_ID equals ot.Order_ID
                        join p in db.Products on oi.Product_ID equals p.Product_ID
                        where oi.Order_part_ID == null && ot.Customer_ID == customerID
                        group oi by p.Product_name into g
                        select new
                        {
                            productName = g.Key,
                            totalQuantity = g.Sum(x => x.Quantity)
                        }).ToDictionary(x=>x.productName, x=>x.totalQuantity);
            return query;
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
                foreach (var orderItem in query)
                {
                    lstOrderItem.Add(orderItem);
                }
            }

            return lstOrderItem;
        }
    }
}
