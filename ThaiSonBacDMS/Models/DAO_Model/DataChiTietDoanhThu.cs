using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO_Model
{
    public class DataChiTietDoanhThu
    {
        public string productName { set; get; }
        public string categoryName { set; get; }
        public int totalQuantity { set; get; }
        public decimal price { set; get; }
        public decimal doanhThu { set; get; }
    }
}
