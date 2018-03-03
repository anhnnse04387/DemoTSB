using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class OrderPartModel
    {

        public int partId { get; set; }

        public String orderPartId { get; set; }

        [Required(ErrorMessage = "Xin hãy nhập ngày xuất hàng")]
        public DateTime deliveryDay { get; set; }

        public List<OrderItemModel> lstItem { get; set; }

    }
}