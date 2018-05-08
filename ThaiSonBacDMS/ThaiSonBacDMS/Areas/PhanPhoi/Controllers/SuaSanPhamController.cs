using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class SuaSanPhamController : Controller
    {
        // GET: PhanPhoi/SuaSanPham
        public ActionResult Index(string product_Id)
        {
            ProductDAO dao = new ProductDAO();
            SupplierDAO supplierDao = new SupplierDAO();
            CategoryDAO daoCate = new CategoryDAO();
            SuaSanPhamModel model = new SuaSanPhamModel();

            model.lstNhaCungCap = new List<SelectListItem>();
            model.lstDanhMuc = new List<SelectListItem>();
            model.itemProduct = new Product();
            model.itemProduct = dao.getDetailProduct(product_Id);
            var lstSupplier = supplierDao.getLstSupplier();
            var lstCate = daoCate.getLstCate();
            var lstSubCate = daoCate.getSubCategory(model.itemProduct.Category_ID);

            if(lstSupplier.Count() != 0)
            {
                foreach(var item in lstSupplier)
                {
                    model.lstNhaCungCap = lstSupplier.Select(x => new SelectListItem
                    {
                        Text = item.Supplier_name,
                        Value = item.Supplier_ID.ToString()
                    }).ToList();
                   
                }
            } 

            if(lstCate.Count() > 0)
            {
                foreach (var item in lstCate)
                {
                    model.lstDanhMuc = lstCate.Select(x => new SelectListItem
                    {
                        Text = item.Category_name,
                        Value = item.Category_ID.ToString()
                    }).ToList();
                }
            }

          if(lstSubCate.Count() > 0)
            {
                foreach(var item in lstSubCate)
                {
                    model.lstDanhMucCon = lstSubCate.Select(x => new SelectListItem
                    {
                        Text = item.Sub_category_name,
                        Value = item.Sub_category_ID
                    }).ToList();
                }
            }

            return View(model);
        }
    }
}