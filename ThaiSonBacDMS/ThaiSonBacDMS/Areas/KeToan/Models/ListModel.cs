using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.KeToan.Models
{
    public class ListModel
    {

        public String orderId { get; set; }
        public String customer { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public decimal fromTotal { get; set; }
        public decimal toTotal { get; set; }
        public byte statusId { get; set; }
        public int orderType { get; set; }
        public IList<SelectListItem> lstStatus { get; set; }
        public List<ListItemModel> items { get; set; }

    }
}