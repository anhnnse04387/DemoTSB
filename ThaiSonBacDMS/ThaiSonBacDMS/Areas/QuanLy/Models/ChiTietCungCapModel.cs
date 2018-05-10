using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class ChiTietCungCapModel
    {
        public string avatar_str { get; set; }
        public Supplier supllier { get; set; }
        public List<Purchase_invoice> lstTotal { get; set; }
        public List<Product> lstProduct { get; set; }
        public Dictionary<string, decimal> dataLineChart { get; set; }
        public int numberOrder { get; set; }
        public decimal priceOrder { get; set; }
        public decimal currentDebt { get; set; }
    }
    public class displayProduct
    {
        public Product product { get; set; }
        public string category_name { get; set; }
    }
}
