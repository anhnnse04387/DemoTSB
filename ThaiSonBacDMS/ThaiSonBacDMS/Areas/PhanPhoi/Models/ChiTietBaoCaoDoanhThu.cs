using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class ChiTietBaoCaoDoanhThu
    {
        public IList<SelectListItem> listShowYear { get; set; }

        public string selectedYear { set; get; }
        public string selectedMonth { set; get; }
        public string selectedDay { set; get; }
        public string productName { set; get; }
        public string categoryName { set; get; }
        public int numberSoldFrom { set; get; }
        public int numberSoldTo { set; get; }
        public decimal priceFrom { set; get; }
        public decimal priceTo { set; get; }
        public decimal doanhThuFrom { set; get; }
        public decimal doanhThuTo { set; get; }

    }
}