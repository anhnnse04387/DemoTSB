using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class POModel
    {

        public int id { get; set; }
        public string PO_no { get; set; }
        public String supplier { get; set; }
        public int? Supplier_ID { get; set; }
        public String address { get; set; }
        public String tel { get; set; }
        public String email { get; set; }
        public DateTime? Date_request_ex_work { get; set; }
        public string Payment { get; set; }
        public DateTime? Date_create { get; set; }
        public IList<SelectListItem> lstSupplier { get; set; }
        public List<PO_Items> items { get; set; }
        public decimal? Total_price { get; set; }
        public int? totalQtt { get; set; }
        public List<POItemModel> readItems { get; set; }

    }
}