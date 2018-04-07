using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.HangHoa.Models
{
    public class OrderTotalModel
    {

        public String orderId { get; set; }
        public String customerName { get; set; }
        public String deliveryAddress { get; set; }
        public int deliveryQtt { get; set; }
        public String invoiceAddress { get; set; }
        public String status { get; set; }
        public int? rate { get; set; }
        public int statusId { get; set; }
        public String taxCode { get; set; }
        public List<OrderItemModel> readItems { get; set; }

    }
}