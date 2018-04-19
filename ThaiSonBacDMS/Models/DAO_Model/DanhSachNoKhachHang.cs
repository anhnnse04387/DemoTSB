using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO_Model
{
    public class DanhSachNoKhachHang
    {
        public int? customerId { get; set; }
        public string tenKhachHang { get; set; }
        public decimal? noCu { get; set; }
        public int? soLuong { get; set; }
        public decimal? tienHang { get; set; }
        public decimal? vat { get; set; }
        public decimal? tongCong { get; set; }
        public decimal? thanhToan { get; set; }
        public decimal? conNo { get; set; }
        public string dienGiai { get; set; }
        public int id { get; set; }
    }
}
