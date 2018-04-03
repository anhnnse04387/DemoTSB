using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class POItemModel
    {

        public String product { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string NOTE { get; set; }
        public decimal? per { get; set; }

    }
}