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
        public string soDienThoai { get; set; }
        public string diaChi { get; set; }
        public string trangThai { get; set; }
        public string chucVu { get; set; }
        public string ngaySinh { get; set; }
        public string BHYT { get; set; }
        public string email { get; set; }
        public int userId { get; set; }
        public DateTime dob { get; set; }
        public string roleId { get; set; }
        public string officeId { get; set; }
        public string account { get; set; }
        public string accountDateCreated{ get; set; }
    }
}
