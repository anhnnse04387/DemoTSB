using Models.DAO;
using Models.DAO_Model;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class ProductPhanPhoiModel
    {
        public List<SelectListItem> lstSubCate { get; set; }
        public string subCateSearch { get; set; }
        public string pCodeSearch { get; set; }
        public string pNameSearch { get; set; }
        public string productIdSearch { get; set; }
        public string supplierSearch { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string categorySearch { get; set; }
        public string priceFrom { get; set; }
        public string priceTo { get; set; }
        public Product product { get; set; }
        public List<Product> lstProduct { get; set; }
        public List<Category> lstCategory { get; set; }
        public List<Category> lstCategorySearch { get; set; }
        public List<SelectListItem> lstSupplier { get; set; }
        public Dictionary<string, List<ShowProductModel>> map { get; set; }
        public List<SelectListItem> lstCateSearch { get; set; }
        public string suppliers { get; set; }
        public string VAT { get; set; }
        public List<ShowProductModel> lstDisplay { get; set; }
        public List<SanPham> lstSanPham { get; set; }
        public Dictionary<string, List<SanPham>> mapSanPham { get; set; }
        public List<GiaSanPham> lstGiaSanPham { get; set; }
        public Dictionary<string,List<GiaSanPham>> mapGiaSanPham { get; set; }
      
    }
    public class ShowProductModel
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
        public decimal Price_after_VAT_VND
        {
            get
            {
                return (decimal)(product.Price_before_VAT_VND + (product.Price_before_VAT_VND * (product.VAT / 100)));
            }
        }
        public decimal Price_after_VAT_USD
        {
            get
            {
                return (decimal)(product.Price_before_VAT_USD + (product.Price_before_VAT_USD * (product.VAT / 100)));
            }
        }
    }
}