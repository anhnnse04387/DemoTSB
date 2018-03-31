using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Models
{
    public class TonKhoModel
    {
        public string pCodeSearch { get; set; }
        public string categorySearch { get; set; }
        public string fromValue { get; set; }
        public string toValue { get; set; }
        public List<SelectListItem> lstCategorySearch { get; set; }
        public List<Category> lstCategory { get; set; }
        public List<Product> lstProduct { get; set; }
        public Dictionary<string,List<ShowTonKhoModel>> map {get;set;}
        public string nhap { get; set; }
        public string xuat { get; set; }
        public string ton { get; set; }
        public bool updated { get; set; }
        public List<ShowTonKhoModel> lstDisplay { get; set; }
        public updateData update { get; set; }
    }
    public class ShowTonKhoModel
    {
        public Product product { get; set; }
        public int TongNhap
        {
            get
            {
                DetailStockInDAO dsiDAO = new DetailStockInDAO();
                return dsiDAO.getStockInQuantities(product.Product_ID);
            }
        }
        public int TongXuat
        {
            get
            {
                DetailStockOutDAO dsoDAO = new DetailStockOutDAO();
                return dsoDAO.getStockOutQuantities(product.Product_ID);
            }
        }
        public int TongTon
        {
            get
            {
                return TongNhap - TongXuat;
            }
        }
    }

    public class updateData
    {
        public string productId { get; set; }
        public int nhap { get; set; }
        public int xuat { get; set; }
    }
}