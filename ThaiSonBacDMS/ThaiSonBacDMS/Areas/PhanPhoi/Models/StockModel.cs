using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class StockModel
    {

        public bool status { get; set; }
        public String no { get; set; }
        public String customer { get; set; }
        public String address { get; set; }
        public String tel { get; set; }
        public int qttTotal { get; set; }
        public IList<SelectListItem> lstLo { get; set; }
        public IList<SelectListItem> lstPi { get; set; }
        public DateTime? dateRequested { get; set; }
        public DateTime? dateImported { get; set; }
        public List<StockItemModel> lstItem { get; set; }

    }
}