using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class DanhSachConNoCungCapModel
    {
        public string error { set; get; }
        public List<Supplier> lstSupp { set; get; } 
    }
}