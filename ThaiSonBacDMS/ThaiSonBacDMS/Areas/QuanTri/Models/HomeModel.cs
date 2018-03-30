using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.QuanTri.Models
{
    public class HomeModel
    {
        public int roleCount { get; set; }
        public int accountCount { get; set; }
        public int pageCount { get; set; }
        public int categoryCount { get; set; }
        public int productCount { get; set; }
        public int fileCount { get; set; }
    }
}