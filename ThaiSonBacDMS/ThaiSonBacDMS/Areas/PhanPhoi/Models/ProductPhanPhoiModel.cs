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
        public string pCodeSearch { get; set; }
        public string pNameSearch { get; set; }
        public string productIdSearch { get; set; }
        public string supplierSearch { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string categorySearch { get; set; }
        public string priceFrom { get; set; }
        public string priceTo { get; set; }
        public Product product { get; set; }
        public List<Product> lstProduct { get; set; }
        public List<Category> lstCategory { get; set; }
        public List<Category> lstCategorySearch { get; set; }
        public List<SelectListItem> lstSupplier { get; set; }
        public Dictionary<string, List<Product>> map { get; set; }
        public List<SelectListItem> lstCateSearch { get; set; }
        public string suppliers { get; set; }
        public string VAT { get; set; }
        public string test { get; set; }
    }
}