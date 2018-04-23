using Models.DAO_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.QuanLy.Models
{
    public class ChiTietNoKhachHangModel
    {
        public List<ChiTietNoKhachHang> lstDisplay { get; set; }
        public string customerName { get; set; }
        public string customerId { get; set; }
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public  decimal? lastedDebt { get; set; }
        public decimal? tienHang { get; set; }
        public decimal? thanhToan { get; set; }
        public int? vat { get; set; }
        public string dienGiai { get; set; }
        public decimal? duNo { get; set; }
       
    }
}