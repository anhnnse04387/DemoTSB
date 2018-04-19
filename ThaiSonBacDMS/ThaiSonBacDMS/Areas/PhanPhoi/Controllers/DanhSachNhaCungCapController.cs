using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class DanhSachNhaCungCapController : Controller
    {
        // GET: PhanPhoi/DanhSachNhaCungCap
        [HttpGet]
        public ActionResult Index()
        {
            DanhSachCungCapModel model = new DanhSachCungCapModel();
            model.lstSupp = new List<Supplier>();
            model.lstSupp = new SupplierDAO().getSupplier();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string supp_name, string dateFrom, string dateTo)
        {
            DanhSachCungCapModel model = new DanhSachCungCapModel();
            model.lstSupp = new List<Supplier>();
            List<Supplier> sup = new SupplierDAO().getSupplier();
            try
            { 
                DateTime? dFrom = string.IsNullOrEmpty(dateFrom) ?
                    dFrom = null : dFrom = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime? dTo = string.IsNullOrEmpty(dateTo) ?
                    dTo = null : dTo = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(supp_name))
                {
                    sup = sup.Where(x=> x.Supplier_name.Contains(supp_name)).ToList();
                }
                if(dFrom >= dTo)
                {
                    model.error = "Ngày từ không được lớn hơn ngày tới";
                    return View(model);
                }
                if(dFrom!=null)
                {
                    sup = sup.Where(x => x.Date_Created >= dFrom).ToList();
                }
                if (dTo != null)
                {
                    sup = sup.Where(x => x.Date_Created <= dTo).ToList();
                }

            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                model.error = "Sai định dạng ngày tháng";
                return View(model);
            }

            model.lstSupp = sup;
            return View(model);
        }

        public ActionResult TaoMoi()
        {
            return View();
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

                    var supplierName = Request.Form.GetValues("supplierName")[0];
                    var supplierPhone = Request.Form.GetValues("supplierPhone")[0];
                    var address = Request.Form.GetValues("address")[0];
                    var email = Request.Form.GetValues("email")[0];
                    var mst = Request.Form.GetValues("mst")[0];
                    Regex phoneRegex = new Regex(CommonConstants.PHONE_REGEX, RegexOptions.IgnoreCase);
                    Regex mstRegex = new Regex(CommonConstants.MST_REGEX, RegexOptions.IgnoreCase);
                    MailAddress m = new MailAddress(email);
                    if (string.IsNullOrEmpty(supplierName))
                    {
                        throw new Exception("Có lỗi xảy ra khi tạo");
                    }
                    else if (!phoneRegex.Match(supplierPhone).Success)
                    {
                        throw new Exception("Có lỗi xảy ra khi tạo");
                    }
                    else if (string.IsNullOrEmpty(address))
                    {
                        throw new Exception("Có lỗi xảy ra khi tạo");
                    }
                    else if (!mstRegex.Match(mst).Success)
                    {
                        throw new Exception("Có lỗi xảy ra khi tạo");
                    }

                    int? lastIDMedia = null;
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
                        lastIDMedia = new MediaDAO().insertMedia(newFname, "/Assets/dist/img/Resource/" + newFname, session.accountID.ToString());

                    }
                    if (!new SupplierDAO().addSupplier(supplierName, lastIDMedia, address, supplierPhone, email, mst))
                    {
                        throw new Exception("Có lỗi xảy ra khi tạo");
                    }
                    return Json("1");
                }
                else
                {
                    throw new Exception("Chưa chọn ảnh");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json("Có lỗi xảy ra khi tạo");
            }
        }
    }
}