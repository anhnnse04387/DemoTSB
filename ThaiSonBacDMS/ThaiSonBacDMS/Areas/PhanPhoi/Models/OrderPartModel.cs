using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class OrderPartModel
    {
       
        public string Order_part_ID { get; set; }
        public decimal? subTotal { get; set; }
        public decimal? total { get; set; }
        public decimal? vat { get; set; }
        public int? qttTotal { get; set; }
        public int? boxTotal { get; set; }
        public decimal? discount { get; set; }
        public decimal? discountMoney { get; set; }
        public decimal? afterDiscountMoney { get; set; }
        public decimal? vatMoney { get; set; }
        public DateTime? Date_reveice_invoice { get; set; }
        public String dateShow { get; set; }
        public List<OrderItemModel> items { get; set; }

    }
}