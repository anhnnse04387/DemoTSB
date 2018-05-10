using Models.DAO_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class DanhSachCongNoCungCapModel
    {
        public List<DanhSachNoCungCap> lstDisplay { get; set; }
        public string supplierId { get; set; }
        public string conNoTu { get; set; }
        public string conNoDen { get; set; }
        public List<Autocomplete> lstAutoComplete { get; set; }
    }
}