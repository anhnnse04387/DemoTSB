using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class StockItemModel
    {

        public int? Product_ID { get; set; }

        public String productName { get; set; }

        public int? Quantities { get; set; }

        public string Note { get; set; }

        public int? orderQtt { get; set; }

    }
}