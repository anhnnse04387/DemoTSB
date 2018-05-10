using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class TaoSanPhamModel
    {
        public IList<SelectListItem> supplierList { get; set; }
        public IList<SelectListItem> categoryList { get; set; }
        public IList<SelectListItem> subCategoryList { get; set; }
        public int userID { get; set; }
    }
}