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
                var productDao = new ProductDAO();
                var cateDao = new CategoryDAO();

                GiaSanPhamModel model = new GiaSanPhamModel();
                model.lstCateSearch = new List<SelectListItem>();
                model.lstGiaSanPham = new List<GiaSanPham>();
                model.map = new Dictionary<string, List<GiaSanPham>>();

                var lstCate = cateDao.getLstCate();
                if (lstCate.Count() > 0)
                {
                    foreach (var item in lstCate)
                    {
                        model.lstCateSearch.Add(new SelectListItem
                        {
                            Text = item.Category_name,
                            Value = item.Category_ID
                        });
                    }
                }
                model.lstGiaSanPham = productDao.giaSanPham();
                foreach(var itemCate in lstCate)
                {
                    List<GiaSanPham> lst = new List<GiaSanPham>();
                    foreach(var item in model.lstGiaSanPham)
                    {
                        if (itemCate.Category_ID.Equals(item.pCateId))
                        {
                            lst.Add(item);
                        }
                    }
                    model.map.Add(itemCate.Category_name, lst);
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
        public ActionResult Index(GiaSanPhamModel mo)
        {
            try
            {

                bool checkboxValue = false;
                var productDAO = new ProductDAO();
                var daoCategory = new CategoryDAO();

                GiaSanPhamModel model = new GiaSanPhamModel();
                model.map = new Dictionary<string, List<GiaSanPham>>();
                model.lstCateSearch = new List<SelectListItem>();
                model.priceFrom = mo.priceFrom;
                model.priceTo = mo.priceTo;
                model.lstGiaSanPham = new List<GiaSanPham>();

                Product product = new Product();
                product.Category_ID = mo.categorySearch;
                product.Product_code = mo.pCodeSearch;
           
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
                model.lstGiaSanPham = productDAO.giaSanPham(product, mo.priceFrom == null ? 0 : Decimal.Parse(mo.priceFrom), mo.priceTo == null ? 0 : Decimal.Parse(mo.priceTo), checkboxValue);
                //Nhom san pham theo category
                if (lstCateTemp != null)
                {
                    foreach (Category item in lstCateTemp)
                    {
                        List<GiaSanPham> lstProductAdd = new List<GiaSanPham>();

                        foreach (var p in model.lstGiaSanPham)
                        {
                            if (p.pCateId.Equals(item.Category_ID))
                            {
                                lstProductAdd.Add(p);
                            }
                        }
                        model.map.Add(item.Category_name, lstProductAdd);
                    }
                }
                model.map= model.map.Where(x => x.Value.Count != 0).ToDictionary(x => x.Key, x => x.Value);
                return View(model);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
                return RedirectToAction("Index");
            }
        }
        public JsonResult GetSearchValue(string searchValue)
        {
            var daoProduct = new ProductDAO();
            var lstProduct = daoProduct.autocompleteSanPhamKinhDoanh(searchValue);
            List<Autocomplete> allSearch = lstProduct.Select(x => new Autocomplete()
            {
                key = x.key,
                strValue = x.key,
            }).ToList();
            return new JsonResult { Data = allSearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}