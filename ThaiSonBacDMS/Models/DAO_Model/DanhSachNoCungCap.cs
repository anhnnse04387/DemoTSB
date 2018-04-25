using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO_Model
{
    public class DanhSachNoCungCap
    {
        public string tenNhaCungCap { get; set; }
        public decimal noDauKy { get; set; }
        public decimal nhapTrongKy { get; set; }
        public decimal thanhToan { get; set; }
        public decimal conNo { get; set; } 
        public int supplierId { get; set; }
        public string dienGiai { get; set; }
        public string ghiChu { get; set; }
    }
}
