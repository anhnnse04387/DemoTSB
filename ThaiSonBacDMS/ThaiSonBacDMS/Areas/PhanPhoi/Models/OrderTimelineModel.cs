using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class OrderTimelineModel
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string orderName { get; set; }
        public List<OrderPartInfo> ListOpInfo { get; set; }
    }

    public class OrderPartInfo
    {
        public int indexOf { get; set; }
        public string orderName { get; set; }
        public DateTime dateCreated { get; set; }
        public string customerName { get; set; }
        public string userName { get; set; }
        public string status { get; set; }
        public DateTime? dateCompleted { get; set; }
        public int quantity { get; set; }
    }
}