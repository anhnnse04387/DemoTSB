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

            model.lstDisplay = new List<ShowTonKhoModel>();
            model.lstProduct = new List<Product>();
            model.lstProduct = daoProduct.getAllProduct();
            if (model.lstProduct.Count() != 0)
            {
                foreach (Product itemProduct in model.lstProduct)
                {
                    ShowTonKhoModel stm = new ShowTonKhoModel();
                    stm.product = itemProduct;
                    model.lstDisplay.Add(stm);
                }
            }
            //nhom san pham theo category
            model.map = new Dictionary<string, List<ShowTonKhoModel>>();
            //Loc san pham theo category
            if (model.lstCategory != null)
            {
                foreach (Category item in model.lstCategory)
                {
                    List<ShowTonKhoModel> lstProductAdd = new List<ShowTonKhoModel>();

                    foreach (ShowTonKhoModel p in model.lstDisplay)
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
        public ActionResult Index(TonKhoModel mo)
        {
            TonKhoModel model = new TonKhoModel();

            ProductDAO daoProduct = new ProductDAO();
            CategoryDAO daoCate = new CategoryDAO();
            model.lstProduct = new List<Product>();
            //khoi tao list category
            model.lstCategory = new List<Category>();
            List<Category> lstTemp = new List<Category>();
            model.map = new Dictionary<string, List<ShowTonKhoModel>>();
            lstTemp = daoCate.getLstCate();
            model.lstCategorySearch = new List<SelectListItem>();
            if (lstTemp.Count() != 0)
            {
                foreach (Category itemCate in lstTemp)
                {
                    model.lstCategorySearch.Add(new SelectListItem { Text = itemCate.Category_name, Value = itemCate.Category_ID });
                }

            }
            model.lstDisplay = new List<ShowTonKhoModel>();
            if (mo.categorySearch != null || mo.fromValue != null || mo.toValue != null || mo.pCodeSearch != null)
            {

                model.lstProduct = daoProduct.getLstProductSearch(mo.fromValue, mo.toValue, mo.pCodeSearch, mo.categorySearch);
                if (model.lstProduct.Count() != 0)
                {
                    foreach (Product itemProduct in model.lstProduct)
                    {                       
                        Category cate = new Category();
                        cate = daoCate.getCategoryById(itemProduct.Category_ID);
                        model.lstCategory.Add(cate);
                    }
                    model.lstCategory = model.lstCategory.Distinct().ToList();

                }
            }
            else
            {
                model.lstCategory = daoCate.getLstCate();
                model.lstProduct = daoProduct.getAllProduct();
            }

            if (model.lstProduct.Count() != 0)
            {
                foreach (Product itemProduct in model.lstProduct)
                {
                    ShowTonKhoModel stm = new ShowTonKhoModel();
                    stm.product = itemProduct;
                    model.lstDisplay.Add(stm);
                }
            }
           
            //Nhom san pham theo category
            if (model.lstCategory != null)
            {
                foreach (Category item in model.lstCategory)
                {
                    List<ShowTonKhoModel> lstProductAdd = new List<ShowTonKhoModel>();

                    foreach (ShowTonKhoModel p in model.lstDisplay)
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
    }


}