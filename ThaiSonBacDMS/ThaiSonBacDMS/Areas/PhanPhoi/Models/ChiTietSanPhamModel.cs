using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class ChiTietSanPhamModel
    {
        public Product product { get; set; }
        public ShowDetailProductModel itemDisplay = new ShowDetailProductModel();
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
}