using Models.DAO;
using Models.DAO_Model;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers
{
    public class SanPhamDangKinhDoanhController : QuanLyBaseController
    {
        // GET: PhanPhoi/SanPhamDangKinhDoanh
        public ActionResult Index()
        {
            try
            {
                var daoProduct = new ProductDAO();
                var daoCategory = new CategoryDAO();
                var daoSupplier = new SupplierDAO();
                ProductPhanPhoiModel model = new ProductPhanPhoiModel();

                model.lstSupplier = new List<SelectListItem>();
                model.lstDisplay = new List<ShowProductModel>();
                model.lstProduct = new List<Product>();
                model.lstCategory = new List<Category>();

                //first load page
                model.lstCategory = daoCategory.getLstCate();
                model.lstSanPham = daoProduct.sanPhamDangKinhDoanh();

                //list cho tim kiem san pham theo Category
                List<Category> lstAll = daoCategory.getLstCate();
                model.lstCateSearch = new List<SelectListItem>();
                foreach (Category item in lstAll)
                {
                    model.lstCateSearch.Add(new SelectListItem { Text = @item.Category_name, Value = @item.Category_ID });
                }
                //khoi tao list cho tim kiem nha cung cap
                List<Supplier> lstAllSupplier = daoSupplier.getLstSupplier();
                foreach (Supplier item in lstAllSupplier)
                {
                    model.lstSupplier.Add(new SelectListItem { Text = @item.Supplier_name, Value = @item.Supplier_ID.ToString() });
                }
                model.mapSanPham = new Dictionary<string, List<SanPham>>();
                //Loc san pham theo category
                if (model.lstCategory != null)
                {
                    foreach (Category item in model.lstCategory)
                    {
                        List<SanPham> lstProductAdd = new List<SanPham>();

                        foreach (var p in model.lstSanPham)
                        {
                            if (p.cateId.Equals(item.Category_ID))
                            {
                                lstProductAdd.Add(p);
                            }
                        }
                        model.mapSanPham.Add(item.Category_name, lstProductAdd);
                    }
                    model.mapSanPham = model.mapSanPham.Where(x => x.Value.Count() != 0).ToDictionary(x => x.Key, x => x.Value);
                }

                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Index(ProductPhanPhoiModel mo)
        {
            var daoProduct = new ProductDAO();
            var daoCategory = new CategoryDAO();
            var daoSupplier = new SupplierDAO();
            ProductPhanPhoiModel model = new ProductPhanPhoiModel();
            Product product = new Product();

            model.lstSupplier = new List<SelectListItem>();
            model.lstDisplay = new List<ShowProductModel>();
            model.lstProduct = new List<Product>();
            model.lstCategory = new List<Category>();

            product.Category_ID = mo.categorySearch;
            product.Supplier_ID = mo.supplierSearch;
            product.Product_code = mo.pCodeSearch;
            //first load page
            model.lstCategory = daoCategory.getLstCate();
            model.lstSanPham = daoProduct.getLstSearchSanPham(product,mo.fromDate,mo.toDate);

            //list cho tim kiem san pham theo Category
            List<Category> lstAll = daoCategory.getLstCate();
            model.lstCateSearch = new List<SelectListItem>();
            foreach (Category item in lstAll)
            {
                model.lstCateSearch.Add(new SelectListItem { Text = @item.Category_name, Value = @item.Category_ID });
            }
            //khoi tao list cho tim kiem nha cung cap
            List<Supplier> lstAllSupplier = daoSupplier.getLstSupplier();
            foreach (Supplier item in lstAllSupplier)
            {
                model.lstSupplier.Add(new SelectListItem { Text = @item.Supplier_name, Value = @item.Supplier_ID.ToString() });
            }
            model.mapSanPham = new Dictionary<string, List<SanPham>>();
            //Loc san pham theo category
            if (model.lstCategory != null)
            {
                foreach (Category item in model.lstCategory)
                {
                    List<SanPham> lstProductAdd = new List<SanPham>();

                    foreach (var p in model.lstSanPham)
                    {
                        if (p.cateId.Equals(item.Category_ID))
                        {
                            lstProductAdd.Add(p);
                        }
                    }
                    model.mapSanPham.Add(item.Category_name, lstProductAdd);
                }
                model.mapSanPham = model.mapSanPham.Where(x => x.Value.Count() != 0).ToDictionary(x => x.Key, x => x.Value);
            }

            return View(model);
        }
        public JsonResult GetSearchValue(string searchValue)
        {
            var daoProduct = new ProductDAO();
            var lstProduct = daoProduct.getLstProductSearch(searchValue);
            List<ProductPhanPhoiModel> allSearch = lstProduct.Select(x => new ProductPhanPhoiModel()
            {
                pCodeSearch = x.Product_name,
                pNameSearch = x.Product_name,
            }).ToList();
            return new JsonResult { Data = allSearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}