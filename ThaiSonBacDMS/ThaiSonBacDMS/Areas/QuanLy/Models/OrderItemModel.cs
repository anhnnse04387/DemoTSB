using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class OrderItemModel
    {
        public decimal? priceBeforeVat { get; set; }
        public int? productId { get; set; }
        public int? qttInven { get; set; }
        public int? qttBox { get; set; }
        public String code { get; set; }
        public String param { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public byte? Discount { get; set; }
        public float? Box { get; set; }
        public decimal? per { get; set; }
        public decimal? priceBeforeDiscount { get; set; }

    }
}