using Models.DAO_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class ChiTietNguoiDungModel
    {

        public DanhSachNguoiDung userInfor = new DanhSachNguoiDung();
        public List<SelectListItem> lstRole { get; set; }
        public List<SelectListItem> lstOffice { get; set; }
        public string roleId { get; set; }
        public string officeId { get; set; }
        public List<string> lstAllEmail { get; set; }
    }
}