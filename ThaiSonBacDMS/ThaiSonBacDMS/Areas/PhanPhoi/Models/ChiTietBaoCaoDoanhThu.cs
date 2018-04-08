using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO_Model;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class ChiTietBaoCaoDoanhThu
    {
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
        public string doanhThuFrom { set; get; }
        public string doanhThuTo { set; get; }
        public Dictionary<string, List<DataChiTietDoanhThu>> data { set; get; }

    }
}