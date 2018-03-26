using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        // GET: PhanPhoi/ChiTietSanPham
        public ActionResult Index(int product_Id)
        {

            var daoProduct = new ProductDAO();
            ChiTietSanPhamModel model = new ChiTietSanPhamModel();
            model.product = daoProduct.getProductByProductId(product_Id);
            return View(model);
        }
    }
}