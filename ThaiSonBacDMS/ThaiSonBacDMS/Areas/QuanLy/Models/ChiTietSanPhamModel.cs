using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class ChiTietSanPhamModel
    {

        public ShowDetailProductModel itemDisplay = new ShowDetailProductModel();
        public ShowOrderItemModel orderItem { get; set; }
        public List<ShowOrderItemModel> lstDisplay { get; set; }
        public List<string> lstLocation { get; set; }
    }
    public class ShowDetailProductModel
    {
        public Product product { get; set; }
        public string supplierName
        {
            get
            {
                SupplierDAO db = new SupplierDAO();
                string supplierName = null;
                string[] supplierId = product.Supplier_ID.Split(',');
                if (supplierId != null)
                {
                    foreach (string item in supplierId)
                    {
                        var supplierNameTemp = db.getSupplierName(Convert.ToInt32(item));
                        supplierName += ", " + supplierNameTemp;
                    }
                }
                supplierName = supplierName.Remove(0, 2);
                return supplierName;
            }
        }
    }
    public class ShowOrderItemModel
    {
        public Order_items item { get; set; }
        public string date
        {
            get
            {
                OrderTotalDAO dao = new OrderTotalDAO();
                return dao.getDate(item.Order_ID)?.ToString("dd-MM-yyyy");
            }
        }
    }
}