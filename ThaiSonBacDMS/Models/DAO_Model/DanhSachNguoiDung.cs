using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO_Model
{
   public class DanhSachNguoiDung
    {
        public string tenNguoiDung { get; set; }
        public string anhDaiDien { get; set; }
        public string gioiTinh { get; set; }
        public DateTime ngayTao { get; set; }
        public string phanHe { get; set; }
        public string trangThai { get; set; }
    }
}
