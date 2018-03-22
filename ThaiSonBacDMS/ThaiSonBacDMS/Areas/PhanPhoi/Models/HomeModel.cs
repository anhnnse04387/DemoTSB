using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class HomeModel
    {
        public int orderInMonth { get; set; }
        public decimal valueInMonth { get; set; }
        public int prodInMonth { get; set; }
        public int numberCustomer { get; set; }
    }
}