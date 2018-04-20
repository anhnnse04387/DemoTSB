using Models.DAO_Model;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class PIDAO
    {

        private ThaiSonBacDMSDbContext db = null;

        public PIDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }
        public void createPI(Purchase_invoice pi)
        {
            db.Purchase_invoice.Add(pi);
            db.SaveChanges();
        }
        public List<Purchase_invoice> getLstPI()
        {
            return db.Purchase_invoice.ToList();
        }

        public Purchase_invoice getPI(int id)
        {
            return db.Purchase_invoice.Where(x => x.Purchase_invoice_ID == id).SingleOrDefault();
        }

        public Dictionary<string, List<DataCongNoCungCap>> getDataLineChart(DateTime beginDate, DateTime endDate, int Supplier_ID, string categoryID)
        {
            Dictionary<string, List<DataCongNoCungCap>> dicData = new Dictionary<string, List<DataCongNoCungCap>>();
            if (categoryID == "-1") categoryID = "";
            var query = from pi_item in db.Purchase_invoice_Items
                        join pi in db.Purchase_invoice on pi_item.Purchase_invoice_ID equals pi.Purchase_invoice_ID
                        join p in db.Products on pi_item.Product_ID equals p.Product_ID
                        where pi.Shipment_date >= beginDate && pi.Shipment_date <= endDate 
                        && pi.Supplier_ID == Supplier_ID && p.Category_ID.Contains(categoryID)
                        select new
                        {
                            shipment_date = pi.Shipment_date,
                            productId = pi_item.Product_ID,
                            categoryID = p.Category_ID,
                            p_quantity = pi_item.Quantity,
                            p_price = pi_item.Price 
                        };
            var handleQuery = query.GroupBy(x=>x.shipment_date);
            foreach(var item in handleQuery)
            {
                var group_category = item.GroupBy(x=>x.categoryID);
                List<DataCongNoCungCap> lstData = new List<DataCongNoCungCap>();
                foreach(var i in group_category)
                {
                    DataCongNoCungCap data = new DataCongNoCungCap();
                    try
                    {
                        data.categoryName = new CategoryDAO().getCategoryById(i.Key).Category_name;
                        data.totalQuantity = (int)i.Sum(x => x.p_quantity);
                        data.totalPrice = (decimal)i.Sum(x => x.p_price * x.p_quantity);
                        lstData.Add(data);
                    }
                    catch(Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e);
                    }
                    
                }
                DateTime key = (DateTime) item.Key;
                dicData.Add(key.ToString("dd/MM/yyyy") , lstData);
            }
            return dicData;
        }

        public List<DataCongNoCungCap> getDataNoCungCap(DateTime beginDate, DateTime endDate,int supplier_id, string product_name, string category_id,
            int? from_number, int? to_number, decimal? from_price, decimal? to_price)
        {
            List<DataCongNoCungCap> lstData = new List<DataCongNoCungCap>();
            var query = from pi_item in db.Purchase_invoice_Items
                        join pi in db.Purchase_invoice on pi_item.Purchase_invoice_ID equals pi.Purchase_invoice_ID
                        join p in db.Products on pi_item.Product_ID equals p.Product_ID
                        join c in db.Categories on p.Category_ID equals c.Category_ID
                        where pi.Shipment_date >= beginDate && pi.Shipment_date <= endDate && pi.Supplier_ID == supplier_id
                        select new
                        {
                            productID = pi_item.Product_ID,
                            productName = p.Product_name,
                            categoryID = p.Category_ID,
                            quantity = pi_item.Quantity,
                            price = pi_item.Quantity*pi_item.Price,
                        };
            var group_query = from q in query
                              group q by q.productID into g
                              select new
                              {
                                  productID = g.Key,
                                  totalQuantity = g.Sum(x => x.quantity),
                                  totalPrice = g.Sum(x=>x.price)
                              };
            var handleQuery = (from q in query
                              join g in group_query on q.productID equals g.productID
                              select new
                              {
                                  productID = q.productID,
                                  productName = q.productName,
                                  categoryID = q.categoryID,
                                  quantity = g.totalQuantity,
                                  price = g.totalPrice,
                              }).Distinct();
            if(!string.IsNullOrEmpty(product_name))
            {
                handleQuery = handleQuery.Where(x=>x.productName.Contains(product_name));
            }
            if(category_id!=null)
            {
                handleQuery = handleQuery.Where(x => x.categoryID == category_id);
            }
            if(from_number!=null)
            {
                handleQuery = handleQuery.Where(x => x.quantity >= from_number);
            }
            if (to_number != null)
            {
                handleQuery = handleQuery.Where(x => x.quantity <= to_number);
            }
            if (from_price != null)
            {
                handleQuery = handleQuery.Where(x => x.price >= from_price);
            }
            if (to_price != null)
            {
                handleQuery = handleQuery.Where(x => x.price <= to_price);
            }
            foreach(var item in handleQuery)
            {
                string cateName = string.Empty;
                string productParam = string.Empty;
                try
                {
                    cateName = new CategoryDAO().getCategoryAllStatus(item.categoryID).Category_name;
                    productParam = new ProductDAO().getProductById(item.productID).Product_parameters;
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
                DataCongNoCungCap data = new DataCongNoCungCap((int) item.productID, 
                    item.productName + " " + productParam
                    , cateName, (int) item.quantity, (decimal)item.price);
                lstData.Add(data);
            }
            return lstData;
        }

    }
}
