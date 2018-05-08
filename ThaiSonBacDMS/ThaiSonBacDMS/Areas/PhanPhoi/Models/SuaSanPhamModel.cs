using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class SuaSanPhamModel
    {
        public Product itemProduct { get; set; }
        public List<SelectListItem> lstDanhMuc { get; set; }
        public List<SelectListItem> lstNhaCungCap { get; set; }
        public List<SelectListItem> lstDanhMucCon { get; set; }
    }
}