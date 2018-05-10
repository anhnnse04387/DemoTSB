using Models.DAO_Model;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class TonKhoModel
    {
        public string pCodeSearch { get; set; }
        public string categorySearch { get; set; }
        public string fromValue { get; set; }
        public string toValue { get; set; }
        public List<SelectListItem> lstCategorySearch { get; set; }
        public List<Category> lstCategory { get; set; }
        public List<Product> lstProduct { get; set; }
        public Dictionary<string, List<TonKho>> map { get; set; }
        public string nhap { get; set; }
        public string xuat { get; set; }
        public string ton { get; set; }
        public bool updated { get; set; }
        public List<TonKho> lstDisplay { get; set; }
    }
}