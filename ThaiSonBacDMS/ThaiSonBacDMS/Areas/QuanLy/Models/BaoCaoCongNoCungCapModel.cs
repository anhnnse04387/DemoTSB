using Models.DAO_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class BaoCaoCongNoCungCapModel
    {
        public IList<SelectListItem> listShowYear { get; set; }
        public string selectedYear { set; get; }
        public string selectedMonth { set; get; }
        public string selectedDay { set; get; }
        public IList<SelectListItem> listCategory { get; set; }
        public string selectedCategory { get; set; }
        public Dictionary<string, List<DataCongNoCungCap>> supp_HanQuoc { set; get; }
        public decimal sumHanQuoc { set; get; }
        public Dictionary<string, List<DataCongNoCungCap>> supp_LS { set; get; }
        public decimal sumLS { set; get; }
        public Dictionary<string, List<DataCongNoCungCap>> supp_TSN { set; get; }
        public decimal sumTNS { set; get; }
    }
}