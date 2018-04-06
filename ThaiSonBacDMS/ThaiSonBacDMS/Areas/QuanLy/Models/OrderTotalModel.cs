using Models.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class OrderTotalModel
    {
        public String orderId { get; set; }
        public int customerId { get; set; }
        public String customerName { get; set; }
        public String deliveryAddress { get; set; }
        public int deliveryQtt { get; set; }
        public String invoiceAddress { get; set; }
        public String status { get; set; }
        public int? rate { get; set; }
        public int statusId { get; set; }
        public String taxCode { get; set; }
        public decimal subTotal { get; set; }
        public decimal? total { get; set; }
        public decimal vat { get; set; }
        public int? qttTotal { get; set; }
        public int? boxTotal { get; set; }
        public decimal? discount { get; set; }
        public decimal? discountMoney { get; set; }
        public decimal? afterDiscountMoney { get; set; }
        public decimal? vatMoney { get; set; }
        public IList<SelectListItem> lstStatus { get; set; }
        public List<Order_part> part { get; set; }
        public List<Order_items> items { get; set; }
        public List<OrderItemModel> readItems { get; set; }
        public IList<SelectListItem> lstCustomer { get; set; }
        public List<OrderPartModel> readPart { get; set; }
    }
}