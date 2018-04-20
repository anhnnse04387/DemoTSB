using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class PIModel
    {

        public int? qttAll { get; set; }
        public Purchase_invoice pi { get; set; }
        public IList<SelectListItem> lstPO { get; set; }
        public List<Purchase_invoice_Items> items { get; set; }
        public List<PIItemModel> readItems { get; set; }

    }
}