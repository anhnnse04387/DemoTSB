using Models.DAO_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class DanhSachCongNoKhachHangModel
    {
        public List<DanhSachNoKhachHang> lstDisplay { get; set; }
        public List<Autocomplete> lstAutoComplete { get; set; }
        public string customerName { get; set; }
        public string noTu { get; set; }
        public string noDen { get; set; }

    }
}