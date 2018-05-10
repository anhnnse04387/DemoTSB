using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class ChiTietKhachHangModel
    {
        public Dictionary<string, int?> displayDonutChart { get; set; }
        public string avatar_str { get; set; }
        public Customer customer { get; set; }
        public List<Order_total> lstTotal { get; set; }
        public Dictionary<string, decimal> dataLineChart { get; set; }
        public int numberOrder { get; set; }
        public decimal priceOrder { get; set; }
        public decimal currentDebt { get; set; }
    }
}