using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO_Model
{
   public class ChiTietNoCungCap
    {
        public string tenNhaCungCap { get; set; }
        public string ngay { get; set; }
        public string loSo { get; set; }
        public string soHopDong { get; set; }
        public int soLuong { get; set; }
        public string dienGiai { get; set; }
        public decimal? tienHang { get; set; }
        public byte? VAT { get; set; }
        public decimal? tongCong { get; set; }
        public decimal? thanhToan { get; set; }
        public decimal? duNo { get; set; }
        public string ghiChu { get; set; }
        public string soPo { get; set; }
    }
}
