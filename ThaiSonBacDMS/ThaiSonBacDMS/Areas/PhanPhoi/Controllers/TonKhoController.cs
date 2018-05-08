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
    public class TonKhoController : Controller
    {
        // GET: PhanPhoi/TonKho
        public ActionResult Index()
        {

            TonKhoModel model = new TonKhoModel();

            ProductDAO daoProduct = new ProductDAO();
            CategoryDAO daoCate = new CategoryDAO();

            //khoi tao list category
            model.lstCategory = new List<Category>();
            model.lstCategory = daoCate.getLstCate();
            model.lstCategorySearch = new List<SelectListItem>();
            if (model.lstCategory.Count() != 0)
            {
                foreach (Category itemCate in model.lstCategory)
                {
                    model.lstCategorySearch.Add(new SelectListItem { Text = itemCate.Category_name, Value = itemCate.Category_ID });
                }
            }
            model.lstDisplay = new List<TonKho>();
            model.lstDisplay = daoProduct.getAllProduct();
            model.map = new Dictionary<string, List<TonKho>>();
            foreach(Category itemCate in model.lstCategory)
            {
                List<TonKho> lst = new List<TonKho>();
                foreach (TonKho itemTonKho in model.lstDisplay)
                {
                    if (itemTonKho.categoryId.Equals(itemCate.Category_ID))
                    {
                        lst.Add(itemTonKho);
                    }
                }
                model.map.Add(itemCate.Category_name, lst);
            }
            model.map = model.map.Where(x => x.Value.Count != 0).ToDictionary(x => x.Key, x => x.Value);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(TonKhoModel mo)
        {
            TonKhoModel model = new TonKhoModel();

            ProductDAO daoProduct = new ProductDAO();
            CategoryDAO daoCate = new CategoryDAO();
            model.lstProduct = new List<Product>();
            //khoi tao list category
            model.lstCategory = new List<Category>();
            List<Category> lstTemp = new List<Category>();
            model.map = new Dictionary<string, List<TonKho>>();
            lstTemp = daoCate.getLstCate();
            model.lstCategorySearch = new List<SelectListItem>();
            if (lstTemp.Count() != 0)
            {
                foreach (Category itemCate in lstTemp)
                {
                    model.lstCategorySearch.Add(new SelectListItem { Text = itemCate.Category_name, Value = itemCate.Category_ID });
                }

            }
            model.lstDisplay = new List<TonKho>();
            model.lstDisplay = daoProduct.getLstProductSearch(mo.fromValue,mo.toValue,mo.pCodeSearch,mo.categorySearch);

            //Nhom san pham theo category
            foreach (Category itemCate in lstTemp)
            {
                List<TonKho> lst = new List<TonKho>();
                foreach (TonKho itemTonKho in model.lstDisplay)
                {
                    if (itemTonKho.categoryId.Equals(itemCate.Category_ID))
                    {
                        lst.Add(itemTonKho);
                    }
                }
                model.map.Add(itemCate.Category_name, lst);
            }
            model.map = model.map.Where(x => x.Value.Count != 0).ToDictionary(x => x.Key, x => x.Value);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateData(int productId, string nhap, string xuat)
        {
            try
            {
                TonKhoModel model = new TonKhoModel();

                ProductDAO daoProduct = new ProductDAO();
                CategoryDAO daoCate = new CategoryDAO();
                DetailStockInDAO dsiDAO = new DetailStockInDAO();
                DetailStockOutDAO dsoDAO = new DetailStockOutDAO();

                //update data
                int rowUpdatedIn = dsiDAO.updateQuantities(productId, nhap);
                int rowUpdatedOut = dsoDAO.updateQuantites(productId, xuat);
                if (rowUpdatedIn > 0 || rowUpdatedOut > 0)
                {

                    model.updated = true;
                }
                else
                {
                    model.updated = false;
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
               
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }

        }
        public JsonResult GetSearchValue(string searchValue)
        {
            var daoProduct = new ProductDAO();
            var lstProduct = daoProduct.getLstProductTonKho(searchValue);
            List<ProductPhanPhoiModel> allSearch = lstProduct.Select(x => new ProductPhanPhoiModel()
            {
                pCodeSearch = x.productName,
                pNameSearch = x.productName,
            }).ToList();
            return new JsonResult { Data = allSearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }


}