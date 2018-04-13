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

    }
}
