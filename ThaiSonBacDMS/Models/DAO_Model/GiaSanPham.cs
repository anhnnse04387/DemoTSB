using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO_Model
{
    public class GiaSanPham
    {
        public string pName { get; set; }
        public string pParam { get; set; }
        public string pId { get; set; }
        public string pCateId { get; set; }
        public decimal? cif { get; set; }
        public decimal? price_before_vat_usd { get; set; }
        public decimal? price_before_vat_vnd { get; set; }
        public int? vat { get; set; }
        public decimal? price_after_vat_usd { get; set; }
        public decimal? price_after_vat_vnd { get; set; }

    }
}
