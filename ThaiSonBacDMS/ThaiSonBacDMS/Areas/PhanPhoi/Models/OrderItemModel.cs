using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class OrderItemModel
    {

        public long productId { get; set; }
        public int quantity { get; set; }
        public int box { get; set; }
        public int discount { get; set; }
        public long price { get; set; }
        public String productName { get; set; }
        public String productParam { get; set; }
        public float preMoney { get; set; }
        public float afterMoney { get; set; }

    }
}