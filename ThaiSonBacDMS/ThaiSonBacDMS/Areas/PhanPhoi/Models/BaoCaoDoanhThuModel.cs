using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class BaoCaoDoanhThuModel
    {
        public IList<SelectListItem> listShowYear { get; set; }
        public IList<SelectListItem> listCategory { get; set; }
        public string selectedYear { get; set; }
        public string selectedMonth { get; set; }
        public string selectedDay { get; set; }
        public string selectedCategory { get; set; }
        public List<DataLineChart> dataLineChart { get; set; }
        public List<DataPieChart> dataPieChart { get; set; }
    }

    public class DataLineChart
    {
        public string displayTime { get; set; }
        public decimal nhapVon { get; set; }
        public decimal xuatVon { get; set; }
        public decimal banChoKhach { get; set; }
    }
    public class DataPieChart
    {
        public string categoryName { get; set;}
        public int numberSold { get; set; }
        public decimal nhapVon { get; set; }
        public decimal xuatVon { get; set; }
        public decimal banChoKhach { get; set; }
        public decimal loiNhuan { get; set; }
    }
}