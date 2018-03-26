﻿using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class SanPhamDangKinhDoanhController : Controller
    {
        // GET: PhanPhoi/SanPhamDangKinhDoanh
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

            List<Supplier> lstAllSupplier = daoSupplier.getLstSupplier();
            model.lstSupplier = new List<SelectListItem>();
            foreach (Supplier item in lstAllSupplier)
            {
                model.lstSupplier.Add(new SelectListItem { Text = @item.Supplier_name, Value = @item.Supplier_ID.ToString() });
            }
            if (mo.categorySearch != null || mo.pCodeSearch != null || mo.supplierSearch != null)
            {
                //search products by product code
                if (mo.categorySearch == null && mo.pCodeSearch != null)
                {
                    var cateId = daoProduct.getCateIdByProductCode(mo.pCodeSearch);
                    model.lstCategory = daoCategory.getLstCateSearch(cateId);
                }
                if (mo.supplierSearch != null)
                {
                    List<Product> lstTemp = daoProduct.lstProductBySupplierId(mo.supplierSearch);
                    List<Category> lstCategoryTemp = new List<Category>();
                    foreach (Product item in lstTemp)
                    {
                        Category cate = new Category();
                        cate = daoCategory.getCategoryById(item.Category_ID);
                        lstCategoryTemp.Add(cate);
                    }
                    model.lstCategory = lstCategoryTemp;
                    model.lstCategory = model.lstCategory.Distinct().ToList();
                }
                //search products by category
                else
                {
                    model.lstCategory = daoCategory.getLstCateSearch(mo.categorySearch);
                }
                model.lstProduct = daoProduct.getLstSearch(product);
            }
            //first load page
            else
            {
                model.lstCategory = daoCategory.getLstCate();
                model.lstProduct = daoProduct.getListProduct();
            }

            //list cho tim kiem san pham theo Category
            List<Category> lstAll = daoCategory.getLstCate();
            model.lstCateSearch = new List<SelectListItem>();
            foreach (Category item in lstAll)
            {
                model.lstCateSearch.Add(new SelectListItem { Text = @item.Category_name, Value = @item.Category_ID });
            }
            model.map = new Dictionary<string, List<Product>>();


            //Loc san pham theo category
            if (model.lstCategory != null)
            {
                foreach (Category item in model.lstCategory)
                {
                    List<Product> lstProductAdd = new List<Product>();

                    foreach (Product p in model.lstProduct)
                    {
                        if (p.Category_ID.Equals(item.Category_ID))
                        {
                            lstProductAdd.Add(p);
                        }
                    }
                    model.map.Add(item.Category_name, lstProductAdd);
                }
            }

            return View(model);
        }

        public JsonResult GetSearchValue(string searchValue)
        {
            var daoProduct = new ProductDAO();
            var lstProduct = daoProduct.getLstProductSearch(searchValue);
            List<ProductPhanPhoiModel> allSearch = lstProduct.Select(x => new ProductPhanPhoiModel()
            {
                pCodeSearch = x.Product_code,
                pNameSearch = x.Product_name,


            }).ToList();
            return new JsonResult { Data = allSearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}