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
    public class GiaSanPhamController : PhanPhoiBaseController
    {
        // GET: PhanPhoi/GiaSanPham
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var productDAO = new ProductDAO();
                var daoCategory = new CategoryDAO();


                ProductPhanPhoiModel model = new ProductPhanPhoiModel();
                model.map = new Dictionary<string, List<ShowProductModel>>();
                model.lstDisplay = new List<ShowProductModel>();
                model.lstCateSearch = new List<SelectListItem>();
                model.lstCategory = daoCategory.getLstCate();
                model.lstProduct = new List<Product>();

                //add item vao search danh muc
                List<Category> lstCateTemp = daoCategory.getLstCate();
                foreach (Category item in lstCateTemp)
                {
                    model.lstCateSearch.Add(new SelectListItem { Text = item.Category_name, Value = item.Category_ID });
                }

                //first load page

                model.lstCategory = daoCategory.getLstCate();
                model.lstProduct = productDAO.getListProduct();

                if (model.lstProduct.Count != 0)
                {
                    foreach (Product itemProduct in model.lstProduct)
                    {
                        ShowProductModel spm = new ShowProductModel();
                        spm.product = itemProduct;
                        model.lstDisplay.Add(spm);
                    }
                }
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult Index(ProductPhanPhoiModel mo)
        {
            try
            {

                Boolean checkboxValue = false;
                var productDAO = new ProductDAO();
                var daoCategory = new CategoryDAO();

                ProductPhanPhoiModel model = new ProductPhanPhoiModel();
                model.map = new Dictionary<string, List<ShowProductModel>>();
                model.lstCateSearch = new List<SelectListItem>();
                model.lstCategory = daoCategory.getLstCate();
                model.lstProduct = new List<Product>();
                model.priceFrom = mo.priceFrom;
                model.priceTo = mo.priceTo;
                model.lstDisplay = new List<ShowProductModel>();

                Product product = new Product();
                product.Category_ID = mo.categorySearch;
                product.Product_code = mo.pCodeSearch;
                product.Supplier_ID = mo.supplierSearch;
                if (mo.VAT != null)
                {
                    if (mo.VAT.Equals("1"))
                    {
                        checkboxValue = true;
                    }
                }
                //add item vao search danh muc
                List<Category> lstCateTemp = daoCategory.getLstCate();
                foreach (Category item in lstCateTemp)
                {
                    model.lstCateSearch.Add(new SelectListItem { Text = item.Category_name, Value = item.Category_ID });
                }
                if (mo.categorySearch != null || mo.pCodeSearch != null || mo.priceFrom != null || mo.priceTo == null)
                {
                    //search products by product code
                    if (mo.pCodeSearch != null)
                    {
                        model.lstCategory = new List<Category>();
                        var cateId = productDAO.getCateIdByProductName(mo.pCodeSearch);
                        model.lstCategory = daoCategory.getLstCateSearch(cateId);
                    }
                    if (mo.categorySearch != null)
                    {
                        model.lstCategory = new List<Category>();
                        model.lstCategory = daoCategory.getLstCateSearch(mo.categorySearch);
                    }
                    //search by price before VAT
                    if (mo.priceFrom != null || mo.priceTo != null)
                    {
                        model.lstCategory = new List<Category>();
                        List<Product> lstProductTemp = productDAO.getLstSearch(product, mo.priceFrom == null ? 0 : Decimal.Parse(mo.priceFrom), mo.priceTo == null ? 0 : Decimal.Parse(mo.priceTo), checkboxValue);
                        if (lstProductTemp.Count > 0)
                        {
                            foreach (Product itemProduct in lstProductTemp)
                            {
                                Category category = new Category();
                                category = daoCategory.getCategoryById(itemProduct.Category_ID);
                                model.lstCategory.Add(category);
                            }
                            model.lstCategory = model.lstCategory.Distinct().ToList();
                        }
                    }
                    model.lstProduct = productDAO.getLstSearch(product, mo.priceFrom == null ? 0 : Decimal.Parse(mo.priceFrom), mo.priceTo == null ? 0 : Decimal.Parse(mo.priceTo), checkboxValue);
                }
                if (model.lstProduct.Count != 0)
                {
                    foreach (Product p in model.lstProduct)
                    {
                        ShowProductModel spm = new ShowProductModel();
                        spm.product = p;
                        model.lstDisplay.Add(spm);
                    }
                }
                //Nhom san pham theo category
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}