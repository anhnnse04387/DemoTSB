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

        [Required(ErrorMessage = "Xin hãy nhập lô số")]
        public String orderId { get; set; }

        [Required(ErrorMessage = "Xin hãy chọn đơn vị")]
        public String customerId { get; set; }

        [Required(ErrorMessage = "Xin hãy nhập địa chỉ giao hàng")]
        public String deliveryAddress { get; set; }

        public int deliveryQtt { get; set; }

        [Required(ErrorMessage = "Xin hãy nhập địa chỉ xuất hợp đồng")]
        public String invoiceAddress { get; set; }

        [Required(ErrorMessage = "Xin hãy chọn mức giá áp dụng")]
        public int rate { get; set; }

        [Required(ErrorMessage = "Xin hãy nhập mã số thuế")]
        public String taxCode { get; set; }

        public long subTotal { get; set; }

        public long vat { get; set; }

        public long discount { get; set; }

        public long total { get; set; }

        public List<OrderPartModel> lstPart { get; set; }

        public List<OrderItemModel> lstItem { get; set; }

    }
}