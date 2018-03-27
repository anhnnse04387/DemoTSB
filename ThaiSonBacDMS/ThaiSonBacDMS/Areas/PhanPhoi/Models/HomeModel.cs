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
        public List<NewsetOrder> newestOrderList { get; set; }
        public Dictionary<int, decimal> dataLineChartCurrentMonth { get; set; }
        public Dictionary<int, int> dataLineChartOrderCurrentMonth { get; set; }
        public Dictionary<int, decimal> dataLineChartPreviousMonth { get; set; }
        public Dictionary<int, int> dataLineChartOrderPreviousMonth { get; set; }
        public bool valueFlag { get; set; }
        public double diffrentValueMonth {get;set;}
        public bool orderFlag { get; set; }
        public double diffrentOrderMonth { get; set; }
    }
    public class NewsetOrder
    {
        public string orderID { get; set; }
        public string customerName { get; set; }
        public int numberProduct { get; set; }
        public int numberBox { get; set; }
        public decimal totalPrice { get; set; }
        public string status { get; set; }
    }
}