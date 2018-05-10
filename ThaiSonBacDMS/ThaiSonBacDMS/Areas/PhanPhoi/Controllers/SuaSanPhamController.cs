using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using ThaiSonBacDMS.Common;

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
            MediaDAO daoMedia = new MediaDAO();
            SuaSanPhamModel model = new SuaSanPhamModel();


            model.lstNhaCungCap = new List<SelectListItem>();
            model.lstDanhMuc = new List<SelectListItem>();
            model.lstDanhMucCon = new List<SelectListItem>();
            model.locationImage = new List<string>();
            model.itemProduct = new Product();
            model.lstStatus = new List<SelectListItem>();
            model.lstProductCode = new List<string>();
            model.lstProductName = new List<string>();
            model.lstProductPram = new List<string>();

            model.itemProduct = dao.getDetailProduct(product_Id);
            model.pId = product_Id;
            model.lstProductCode = dao.checkExistedCode(product_Id);
            model.lstProductName = dao.checkExistedName(product_Id);
            model.lstProductPram = dao.checkExistedParam(product_Id);


            var lstSupplier = supplierDao.getLstSupplier();
            var lstCate = daoCate.getLstCate();
            var lstSubCate = daoCate.getSubCategory(model.itemProduct.Category_ID);


            var lstMedia = daoMedia.getMediaId(Convert.ToInt32(product_Id));
            model.locationImage = new List<string>();
            if (lstMedia.Count() > 0)
            {
                foreach (var item in lstMedia)
                {
                    string location = daoMedia.getProductMedia(Convert.ToInt32(item));
                    model.locationImage.Add(location);
                }
            }
            model.lstStatus.Add(new SelectListItem
            {
                Text = "Đang Kinh Doanh",
                Value = "1"
            });
            model.lstStatus.Add(new SelectListItem
            {
                Text = "Ngừng Kinh Doanh",
                Value = "0"
            });
            if (lstSupplier.Count() != 0)
            {
                foreach (var item in lstSupplier)
                {
                    model.lstNhaCungCap.Add(new SelectListItem
                    {
                        Text = item.Supplier_name,
                        Value = item.Supplier_ID.ToString()
                    });

                }
            }

            if (lstCate.Count() > 0)
            {
                foreach (var item in lstCate)
                {
                    model.lstDanhMuc.Add(new SelectListItem
                    {
                        Text = item.Category_name,
                        Value = item.Category_ID.ToString()
                    });
                }
            }

            if (lstSubCate.Count() > 0)
            {
                foreach (var item in lstSubCate)
                {
                    model.lstDanhMucCon.Add(new SelectListItem
                    {
                        Text = item.Sub_category_name,
                        Value = item.Sub_category_ID
                    });
                }
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            try
            {
                ProductDAO dao = new ProductDAO();

                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var pCode = Request.Form.GetValues("pCode")[0];
                var pName = Request.Form.GetValues("pName")[0];
                var pParam = Request.Form.GetValues("pParam")[0];
                var pSupplier = Request.Form.GetValues("pSupplier")[0];
                var pCategory = Request.Form.GetValues("pCategory")[0];
                var pSubCategory = Request.Form.GetValues("pSubCategory")[0];
                var pNumberCarton = Request.Form.GetValues("pNumberCarton")[0];
                var pBeforeVatVND = Request.Form.GetValues("pBeforeVatVND")[0];
                var pBeforeVatUSD = Request.Form.GetValues("pBeforeVatUSD")[0];
                var cifVND = Request.Form.GetValues("cifVND")[0];
                var cifUSD = Request.Form.GetValues("cifUSD")[0];
                var vat = Request.Form.GetValues("vat")[0];
                var pDescription = Request.Form.GetValues("pDescription")[0];
                var pDetail = Request.Form.GetValues("pDetail")[0];
                var pStatus = Request.Form.GetValues("pStatus")[0];
                var pId = Request.Form.GetValues("pId")[0];

                Product product = new Product();

                product.Product_ID = Convert.ToInt32(pId);
                product.Product_code = pCode;
                product.Product_name = pName;
                product.Product_parameters = pParam;
                product.Supplier_ID = pSupplier;
                product.Category_ID = pCategory;
                product.Sub_category_ID = pSubCategory;
                product.Quantity_in_carton = Convert.ToInt32(pNumberCarton);
                product.Overview = pDescription;
                product.Specification = pDetail;
                product.CIF_USD = Convert.ToDecimal(cifUSD);
                product.CIF_VND = Convert.ToDecimal(cifVND);
                product.Price_before_VAT_USD = Convert.ToDecimal(pBeforeVatUSD);
                product.Price_before_VAT_VND = Convert.ToDecimal(pBeforeVatVND);
                product.VAT = Convert.ToInt32(vat);
                product.Status = Convert.ToInt32(pStatus);

                // Checking no of files injected in Request object  
                if (Request.Files.Count > 0)
                {
                   

                    List<int> listMedia = new List<int>();
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        // Get the complete folder path and store the file inside it. 
                        string newFname = fname;
                        fname = Path.Combine(Server.MapPath("~/Assets/dist/img/Resource"), fname);
                        file.SaveAs(fname);
                        int lastIDMedia = new MediaDAO().insertMedia(newFname, "/Assets/dist/img/Resource/" + newFname, session.user_info.User_ID);
                        listMedia.Add(lastIDMedia);
                    }
                    dao.deleteRecord(pId);
                    dao.updateProduct(product);
                    foreach (var item in listMedia)
                    {
                        int check = new MediaDAO().insertProductMedia(Convert.ToInt32(pId), item);
                    }

                    return Json(new { success=true,JsonRequestBehavior.AllowGet});
                }
                else
                {
                   
                    dao.updateProduct(product);
                    return Json(new { success = true, JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json("-1");
            }
        }
        [HttpPost]
        public JsonResult ChangeList(string cateId)
        {
            var subCateDao = new Sub_CategoryDAO();
            var lstSubCate = subCateDao.getSubCateByCateId(cateId);
            return new JsonResult { Data = lstSubCate, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }

}