using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.QuanTri.Models
{
    public class TaoNguoiDungModel
    {
        public string avatar { get; set; }
        public string name { get; set; }
        public string roleId { get; set; }
        public string officeId { get; set; }
        public string email { get; set; }
        public string dob { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public string insuranceNo { get; set; }
        public List<SelectListItem> lstRole { get; set; }
        public List<SelectListItem> lstOffice { get; set; }
    }
}