using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class DanhSachKhachHangModel
    {
        public string error { set; get; }
        public List<Customer> lstCus { set; get; }
    }
}