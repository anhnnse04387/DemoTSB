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
        public List<ChiTietNoCungCap> lstDisplay { get; set; }
        public string supplierName{ get; set; }
        public int supplierId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public decimal? lastedDebt { get; set; }
        public decimal? tienHang { get; set; }
        public decimal? thanhToan { get; set; }
        public int? vat { get; set; }
        public string dienGiai { get; set; }
        public decimal? duNo { get; set; }
        public decimal? noCu { get; set; }
        public string ghiChu { get; set; }
        public string ngay { get; set; }
        
    }
}