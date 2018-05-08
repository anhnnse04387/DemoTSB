using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO_Model
{
    public class TonKho
    {
        public string productName { get; set; }
        public string productParameter { get; set; }
        public int? tongXuat { get; set; }
        public int? tongNhap { get; set; }
        public int?  tongTon { get; set; }
        public int? productId { get; set; }
        public string categoryId { get; set; }
    }
}