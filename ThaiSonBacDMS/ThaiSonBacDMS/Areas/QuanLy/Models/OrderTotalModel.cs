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
        public String invoiceNumber { get; set; }
        public String dayCreated { get; set; }
        public Dictionary<int, StatusDetailModel> dict { get; set; }
        public String lineStatus { get; set; }
        public String orderId { get; set; }
        public int customerId { get; set; }
        public String customerName { get; set; }
        public String deliveryAddress { get; set; }
        public int deliveryQtt { get; set; }
        public String invoiceAddress { get; set; }
        public String status { get; set; }
        public int? rate { get; set; }
        public String taxCode { get; set; }
        public decimal subTotal { get; set; }
        public decimal? total { get; set; }
        public decimal? vat { get; set; }
        public int? qttTotal { get; set; }
        public float? boxTotal { get; set; }
        public byte? discount { get; set; }
        public decimal? discountMoney { get; set; }
        public decimal? afterDiscountMoney { get; set; }
        public List<Order_part> part { get; set; }
        public List<Order_items> items { get; set; }
        public List<OrderItemModel> readItems { get; set; }
        public List<OrderItemModel> leftItems { get; set; }
        public IList<SelectListItem> lstCustomer { get; set; }
        public List<OrderPartModel> readPart { get; set; }
        public decimal? leftSubTotal { get; set; }
        public decimal? leftTotal { get; set; }
        public int? leftQttTotal { get; set; }
        public float? leftBoxTotal { get; set; }
        public decimal? leftDiscountMoney { get; set; }
        public decimal? leftAfterDiscountMoney { get; set; }
        public decimal? leftVatMoney { get; set; }
    }
}