using Models.DAO;
using Models.DAO_Model;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanLy.Models;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers
{
    public class SanPhamNgungKinhDoanhController : QuanLyBaseController
    {
        // GET: QuanLy/SanPhamNgungKinhDoanh
        public ActionResult Index()
        {
            try
            {
                var daoProduct = new ProductDAO();
                var daoCategory = new CategoryDAO();
                var daoSupplier = new SupplierDAO();
                var daoSubCate = new Sub_CategoryDAO();

                ProductPhanPhoiModel model = new ProductPhanPhoiModel();

                model.lstSupplier = new List<SelectListItem>();
                model.lstDisplay = new List<ShowProductModel>();
                model.lstProduct = new List<Product>();
                model.lstCategory = new List<Category>();
                model.lstSanPham = new List<SanPham>();

                //first load page
                model.lstCategory = daoCategory.getLstCate();
                model.lstSanPham = daoProduct.sanPhamNgungKinhDoanh();

                model.lstSubCate = new List<SelectListItem>();

                //first load page
                var lstSubCate = daoSubCate.getAllSubCate();
                if (lstSubCate.Count() > 0)
                {
                    foreach (var item in lstSubCate)
                    {
                        model.lstSubCate.Add(new SelectListItem
                        {
                            Text = item.Sub_category_name,
                            Value = item.Sub_category_ID
                        });
                    }

                }

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
            var daoSubCate = new Sub_CategoryDAO();

            ProductPhanPhoiModel model = new ProductPhanPhoiModel();

            Product product = new Product();
            product.Category_ID = mo.categorySearch;
            product.Product_name = mo.pCodeSearch;
            product.Supplier_ID = mo.supplierSearch;
            product.Sub_category_ID = mo.subCateSearch;


            model.lstSupplier = new List<SelectListItem>();
            model.lstDisplay = new List<ShowProductModel>();
            model.lstProduct = new List<Product>();
            model.lstCategory = new List<Category>();
            model.lstSanPham = new List<SanPham>();
            model.lstSubCate = new List<SelectListItem>();

            model.lstCategory = daoCategory.getLstCate();
            model.mapSanPham = new Dictionary<string, List<SanPham>>();

            //khoi tao list cho tim kiem san pham theo Category
            List<Category> lstAll = daoCategory.getLstCate();
            model.lstCateSearch = new List<SelectListItem>();
            foreach (Category item in lstAll)
            {
                model.lstCateSearch.Add(new SelectListItem { Text = @item.Category_name, Value = @item.Category_ID });
            }
            //khoi tao list cho tim kiem nha cung cap
            List<Supplier> lstAllSupplier = daoSupplier.getLstSupplier();
            model.lstSupplier = new List<SelectListItem>();
            foreach (Supplier item in lstAllSupplier)
            {
                model.lstSupplier.Add(new SelectListItem { Text = @item.Supplier_name, Value = @item.Supplier_ID.ToString() });
            }


            model.lstSanPham = daoProduct.getLstSearchSanPhamNgungKD(product);

            var lstSubCate = daoSubCate.getSubCateByCateId(mo.categorySearch);
            if (lstSubCate.Count() > 0)
            {
                foreach (var item in lstSubCate)
                {
                    model.lstSubCate.Add(new SelectListItem { Text = item.key, Value = item.strValue });
                }
            }

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
        [ValidateInput(false)]
        public JsonResult GetSearchValue(string searchValue)
        {
            var daoProduct = new ProductDAO();
            var lstProduct = daoProduct.autocompleteSanPhamNgungKD(searchValue);
            List<Autocomplete> allSearch = lstProduct.Select(x => new Autocomplete()
            {
                key = x.key,
                strValue = x.key,
            }).ToList();
            return new JsonResult { Data = allSearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult generateSubCateList(string cateId)
        {
            var subCateDao = new Sub_CategoryDAO();
            var lstSubCate = subCateDao.getSubCateByCateId(cateId);
            return new JsonResult { Data = lstSubCate, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}