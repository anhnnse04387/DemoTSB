using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.HangHoa.Models
{
    public class OrderItemModel
    {

        public String code { get; set; }
        public byte? Box { get; set; }
        public int? Quantity { get; set; }

    }
}