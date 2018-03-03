using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class OrderTotalModel
    {
        
        public String orderId { get; set; }

        [Required(ErrorMessage = "Xin hãy chọn đơn vị")]
        public long customerId { get; set; }

        [Required(ErrorMessage = "Xin hãy nhập địa chỉ giao hàng")]
        public String deliveryAddress { get; set; }

        public int deliveryQtt { get; set; }

        [Required(ErrorMessage = "Xin hãy nhập địa chỉ xuất hợp đồng")]
        public String invoiceAddress { get; set; }

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