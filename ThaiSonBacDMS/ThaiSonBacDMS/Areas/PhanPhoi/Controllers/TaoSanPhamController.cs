using Models.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using ThaiSonBacDMS.Common;
using ThaiSonBacDMS.Controllers;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class TaoSanPhamController : BaseController
    {
        // GET: PhanPhoi/TaoSanPham
        public ActionResult Index()
        {
            TaoSanPhamModel model = new TaoSanPhamModel();
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            model.userID = session.user_info.User_ID;
            model.supplierList = new SupplierDAO().getLstSupplier().Select(x => new SelectListItem
            {
                Text = x.Supplier_name,
                Value =  x.Supplier_ID.ToString()
            }).ToList();
            model.categoryList = new CategoryDAO().getLstCate().Select(x => new SelectListItem
            {
                Text = x.Category_name,
                Value = x.Category_ID.ToString()
            }).ToList();
            model.subCategoryList = new CategoryDAO().getSubCategory().Select(x => new SelectListItem
            {
                Text = x.Sub_category_name,
                Value = x.Sub_category_ID.ToString()
            }).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            try
            {
                // Checking no of files injected in Request object  
                if (Request.Files.Count > 0)
                {
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
                    var userID = Request.Form.GetValues("userID")[0];

                    if (string.IsNullOrEmpty(pCode))
                    {
                        return Json("-1");
                    }
                    else if (string.IsNullOrEmpty(pName))
                    {
                        return Json("-1");
                    }
                    else if (string.IsNullOrEmpty(pParam))
                    {
                        return Json("-1");
                    }

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
                    int pID = new ProductDAO().insertProduct(pCode, pName, pParam,
                        pSupplier, pCategory, pSubCategory, int.Parse(pNumberCarton),
                        pDescription, pDetail, decimal.Parse(cifVND), decimal.Parse(cifUSD), decimal.Parse(pBeforeVatVND), decimal.Parse(pBeforeVatUSD), int.Parse(vat));
                    foreach(var item in listMedia)
                    {
                        int check = new MediaDAO().insertProductMedia(pID, item);
                    }

                    return Json(pID);
                }
                else
                {
                    return Json("-2");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json("-1");
            }
        }
    }
}