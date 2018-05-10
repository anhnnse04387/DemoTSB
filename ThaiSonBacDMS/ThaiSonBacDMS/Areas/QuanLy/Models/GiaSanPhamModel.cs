using Models.DAO_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class GiaSanPhamModel
    {
        public Dictionary<string, List<GiaSanPham>> map { get; set; }
        public string pCodeSearch { get; set; }
        public string categorySearch { get; set; }
        public string priceFrom { get; set; }
        public string priceTo { get; set; }
        public List<GiaSanPham> lstGiaSanPham { get; set; }
        public List<SelectListItem> lstCateSearch { get; set; }
        public string VAT { get; set; }
    }
}