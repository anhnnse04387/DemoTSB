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
    public class SanPhamNgungKinhDoanhController : Controller
    {
        // GET: PhanPhoi/SanPhamNgungKinhDoanh
        public ActionResult Index()
        {
            try
            {
                ProductDAO daoProduct = new ProductDAO();
                CategoryDAO daoCate = new CategoryDAO();
                SupplierDAO daoSupplier = new SupplierDAO();
                ProductPhanPhoiModel model = new ProductPhanPhoiModel();

                model.lstSupplier = new List<SelectListItem>();
                model.lstDisplay = new List<ShowProductModel>();
                model.lstProduct = new List<Product>();
                model.lstCategory = new List<Category>();

                //first load page
                model.lstCategory = daoCate.getLstCate();
                model.lstProduct = daoProduct.sanPhamNgungKinhDoanh();
                if (model.lstProduct.Count != 0)
                {
                    foreach (Product itemProduct in model.lstProduct)
                    {
                        ShowProductModel spm = new ShowProductModel();
                        spm.product = itemProduct;
                        model.lstDisplay.Add(spm);
                    }
                }

                //list cho tim kiem san pham theo Category
                List<Category> lstAll = daoCate.getLstCate();
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
                model.map = new Dictionary<string, List<ShowProductModel>>();
                //Loc san pham theo category
                if (model.lstCategory != null)
                {
                    foreach (Category item in model.lstCategory)
                    {
                        List<ShowProductModel> lstProductAdd = new List<ShowProductModel>();

                        foreach (ShowProductModel p in model.lstDisplay)
                        {
                            if (p.product.Category_ID.Equals(item.Category_ID))
                            {
                                lstProductAdd.Add(p);
                            }
                        }
                        model.map.Add(item.Category_name, lstProductAdd);
                    }
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
            product.Category_ID = mo.categorySearch;
            product.Product_code = mo.pCodeSearch;
            product.Supplier_ID = mo.supplierSearch;


            model.lstSupplier = new List<SelectListItem>();
            model.lstDisplay = new List<ShowProductModel>();
            model.lstProduct = new List<Product>();
            model.lstCategory = new List<Category>();


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


            if (!string.IsNullOrEmpty(mo.categorySearch) || !string.IsNullOrEmpty(mo.pCodeSearch) || !string.IsNullOrEmpty(mo.supplierSearch) || !string.IsNullOrEmpty(mo.fromDate) || !string.IsNullOrEmpty(mo.toDate))
            {
                model.lstProduct = daoProduct.sanPhamNgungKinhDoanh(product, mo.fromDate, mo.toDate);

                if (model.lstProduct.Count != 0)
                {
                    foreach (Product item in model.lstProduct)
                    {
                        Category cate = new Category();
                        cate = daoCategory.getCategoryById(item.Category_ID);
                        model.lstCategory.Add(cate);
                    }
                }
                model.lstCategory = model.lstCategory.Distinct().ToList();

            }
            else
            {
                model.lstCategory = daoCategory.getLstCate();
                model.lstProduct = daoProduct.sanPhamNgungKinhDoanh();
            }
            
            if (model.lstProduct.Count != 0)
            {
                foreach (Product productItem in model.lstProduct)
                {
                    ShowProductModel spm = new ShowProductModel();
                    spm.product = productItem;
                    model.lstDisplay.Add(spm);
                }
            }

            model.map = new Dictionary<string, List<ShowProductModel>>();

            //Loc san pham theo category
            if (model.lstCategory != null)
            {
                foreach (Category item in model.lstCategory)
                {
                    List<ShowProductModel> lstProductAdd = new List<ShowProductModel>();

                    foreach (ShowProductModel p in model.lstDisplay)
                    {
                        if (p.product.Category_ID.Equals(item.Category_ID))
                        {
                            lstProductAdd.Add(p);
                        }
                    }
                    model.map.Add(item.Category_name, lstProductAdd);
                }
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult GetSearchValue(string searchValue)
        {
            var daoProduct = new ProductDAO();
            var lstProduct = daoProduct.searchNgungKinhDoanh(searchValue);
            List<ProductPhanPhoiModel> allSearch = lstProduct.Select(x => new ProductPhanPhoiModel()
            {
                pCodeSearch = x.Product_code,
                pNameSearch = x.Product_name,


            }).ToList();
            return new JsonResult { Data = allSearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}