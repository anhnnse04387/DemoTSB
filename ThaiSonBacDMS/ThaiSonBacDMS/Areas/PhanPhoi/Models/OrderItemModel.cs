using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class OrderItemModel
    {

        public String code;
        public String param;
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public byte? Discount { get; set; }
        public byte? Box { get; set; }
        public decimal? per { get; set; }
        public decimal? priceBeforeDiscount { get; set; }

    }
}