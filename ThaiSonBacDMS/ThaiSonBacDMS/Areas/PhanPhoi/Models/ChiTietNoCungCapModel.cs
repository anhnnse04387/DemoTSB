using Models.DAO_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class ChiTietNoCungCapModel
    {
        public List<ChiTietNoCungCap> lstDisplay { get; set; }
        public string supplierName{ get; set; }
        public int supplierId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public decimal? lastedDebt { get; set; }
        public decimal? tienHang { get; set; }
        public decimal? thanhToan { get; set; }
        public int? vat { get; set; }
        public string dienGiai { get; set; }
        public decimal? duNo { get; set; }
        public decimal? noCu { get; set; }
        public string ghiChu { get; set; }
        public string ngay { get; set; }

    }
}