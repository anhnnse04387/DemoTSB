using Models.DAO_Model;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class ChiTietNoCungCapModel
    {
        public List<SelectListItem> lstCategorySearch { get; set; }
        public IList<SelectListItem> listShowYear { get; set; }
        public string selectedYear { set; get; }
        public string selectedMonth { set; get; }
        public string selectedDay { set; get; }
        public string productCode { set; get; }
        public string categoryName { set; get; }
        public string numberSoldFrom { set; get; }
        public string numberSoldTo { set; get; }
        public string priceFrom { set; get; }
        public string priceTo { set; get; }
        public Supplier supp { set; get; }
        public Dictionary<string, List<DataCongNoCungCap>> data { set; get; }
        public string errorString { set; get; }
    }
}