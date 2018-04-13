using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class ListItemModel
    {

        public String orderId { get; set; }
        public String customer { get; set; }
        public DateTime date { get; set; }
        public decimal? total { get; set; }
        public String status { get; set; }
        public String note { get; set; }
        public int delivery { get; set; }
        public String spanClass { get; set; }

    }
}