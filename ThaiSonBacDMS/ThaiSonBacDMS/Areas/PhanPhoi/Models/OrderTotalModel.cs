using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class OrderTotalModel
    {
        public String orderId { get; set; }      
        public String customerId { get; set; }
        public String deliveryAddress { get; set; }
        public int deliveryQtt { get; set; }
        public String invoiceAddress { get; set; }
        public int rate { get; set; }
        public String taxCode { get; set; }
        public long subTotal { get; set; }
        public long vat { get; set; }
        public long discount { get; set; }
        public long total { get; set; }
        public List<OrderPartModel> lstPart { get; set; }
        public List<OrderItemModel> lstItem { get; set; }
        public IList<SelectListItem> lstCustomer { get; set; }

    }
}