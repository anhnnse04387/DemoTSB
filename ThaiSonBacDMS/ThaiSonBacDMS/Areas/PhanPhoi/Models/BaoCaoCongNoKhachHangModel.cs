using Models.DAO_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class BaoCaoCongNoKhachHangModel
    {
        public IList<SelectListItem> listShowYear { get; set; }
        public string selectedYear { set; get; }
        public string selectedMonth { set; get; }
        public string selectedDay { set; get; }
        public IList<SelectListItem> listCategory { get; set; }
        public string selectedCategory { get; set; }
        public Dictionary<string, List<DataCongNoKhachHang>> dataCongNo { set; get; }
        public decimal totalPrice { get; set; }
        public List<CongNoKhachHang> listCongNo { get; set; }
    }
    public class CongNoKhachHang
    {
        public CongNoKhachHang(string cus_name, string cus_address, decimal cur_debt, int cus_id)
        {
            this.cus_name = cus_name;
            this.cus_address = cus_address;
            this.cur_debt = cur_debt;
            this.cur_id = cur_id;
        }
        public string cus_name { get; set; }
        public string cus_address { get; set; }
        public decimal cur_debt { get; set; }
        public int cur_id { get; set; }
    }
}