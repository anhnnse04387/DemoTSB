using Models.DAO_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class DanhSachNguoiDungHoatDongModel
    {
        public List<DanhSachNguoiDung> lstDisplay { get; set; }
        public List<SelectListItem> lstRole { get; set; }
        public string roleIdSearch { get; set; }
        public string nameSearch { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
    }
}