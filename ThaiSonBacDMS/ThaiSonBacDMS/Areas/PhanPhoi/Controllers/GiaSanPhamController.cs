using Models.DAO;
using Models.DAO_Model;
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
        public ActionResult Index()
        {
            try
            {
                var productDAO = new ProductDAO();
                var daoCategory = new CategoryDAO();


                ProductPhanPhoiModel model = new ProductPhanPhoiModel();
                model.lstGiaSanPham = new List<GiaSanPham>();
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
                model.lstGiaSanPham = productDAO.giaSanPham();

                model.mapGiaSanPham = new Dictionary<string, List<GiaSanPham>>();
                //Loc san pham theo category
                if (model.lstCategory != null)
                {
                    foreach (Category item in model.lstCategory)
                    {
                        List<GiaSanPham> lstProductAdd = new List<GiaSanPham>();

                        foreach (var p in model.lstGiaSanPham)
                        {
                            if (p.pCateId.ToString().Equals(item.Category_ID.ToString()))
                            {
                                lstProductAdd.Add(p);
                            }
                        }
                        model.mapGiaSanPham.Add(item.Category_name, lstProductAdd);
                    }
                    model.mapGiaSanPham = model.mapGiaSanPham.Where(x => x.Value.Count != 0).ToDictionary(x => x.Key, x => x.Value);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Index(ProductPhanPhoiModel mo)
        {
            try
            {

                //bool checkboxValue = false;
                //var productDAO = new ProductDAO();
                //var daoCategory = new CategoryDAO();

                ProductPhanPhoiModel model = new ProductPhanPhoiModel();
                //model.mapGiaSanPham = new Dictionary<string, List<GiaSanPham>>();
                //model.lstCateSearch = new List<SelectListItem>();
                //model.lstCategory = daoCategory.getLstCate();
                //model.lstProduct = new List<Product>();
                //model.priceFrom = mo.priceFrom;
                //model.priceTo = mo.priceTo;
                //model.lstGiaSanPham = new List<GiaSanPham>();

                //Product product = new Product();
                //product.Category_ID = mo.categorySearch;
                //product.Product_code = mo.pCodeSearch;
                //product.Supplier_ID = mo.supplierSearch;
                //if (mo.VAT != null)
                //{
                //    if (mo.VAT.Equals("1"))
                //    {
                //        checkboxValue = true;
                //    }
                //}
                ////add item vao search danh muc
                //List<Category> lstCateTemp = daoCategory.getLstCate();
                //foreach (Category item in lstCateTemp)
                //{
                //    model.lstCateSearch.Add(new SelectListItem { Text = item.Category_name, Value = item.Category_ID });
                //}
                //model.lstGiaSanPham = productDAO.giaSanPham(product, mo.priceFrom == null ? 0 : Decimal.Parse(mo.priceFrom), mo.priceTo == null ? 0 : Decimal.Parse(mo.priceTo), checkboxValue);
                ////Nhom san pham theo category
                //if (model.lstCategory != null)
                //{
                //    foreach (Category item in model.lstCategory)
                //    {
                //        List<GiaSanPham> lstProductAdd = new List<GiaSanPham>();

                //        foreach (var p in model.lstGiaSanPham)
                //        {
                //            if (p.pCateId.Equals(item.Category_ID))
                //            {
                //                lstProductAdd.Add(p);
                //            }
                //        }
                //        model.mapGiaSanPham.Add(item.Category_name, lstProductAdd);
                //    }
                //}
                //model.mapGiaSanPham = model.mapGiaSanPham.Where(x => x.Value.Count != 0).ToDictionary(x => x.Key, x => x.Value);
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}