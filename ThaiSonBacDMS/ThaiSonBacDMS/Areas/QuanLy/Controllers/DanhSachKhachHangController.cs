using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanLy.Models;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers
{
    public class DanhSachKhachHangController : QuanLyBaseController
    {
        // GET: QuanLy/DanhSachKhachHang
        public ActionResult Index()
        {
            DanhSachKhachHangModel model = new DanhSachKhachHangModel();
            model.lstCus = new List<Customer>();
            model.lstCus = new CustomerDAO().getCustomer();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string cus_name, string cus_rank)
        {
            DanhSachKhachHangModel model = new DanhSachKhachHangModel();
            model.lstCus = new List<Customer>();
            List<Customer> cus = new CustomerDAO().getCustomer();
            try
            {
                if (!string.IsNullOrEmpty(cus_name))
                {
                    cus = cus.Where(x => x.Customer_name.Contains(cus_name)).ToList();
                }

                if (cus_rank != "0")
                {
                    cus = cus.Where(x => x.Rank.Contains(cus_rank)).ToList();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                model.error = "Có lỗi xảy ra khi tìm kiếm";
                return View(model);
            }

            model.lstCus = cus;
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
                    var deliver_address = Request.Form.GetValues("deliver_address")[0];
                    var email = Request.Form.GetValues("email")[0];
                    var mst = Request.Form.GetValues("mst")[0];
                    Regex phoneRegex = new Regex(CommonConstants.PHONE_REGEX, RegexOptions.IgnoreCase);
                    Regex mstRegex = new Regex(CommonConstants.MST_REGEX, RegexOptions.IgnoreCase);
                    MailAddress m = new MailAddress(email);
                    if (string.IsNullOrEmpty(supplierName))
                    {
                        return Json("-1");
                    }
                    else if (!phoneRegex.Match(supplierPhone).Success)
                    {
                        return Json("-1");
                    }
                    else if (string.IsNullOrEmpty(address))
                    {
                        return Json("-1");
                    }
                    else if (string.IsNullOrEmpty(deliver_address))
                    {
                        return Json("-1");
                    }
                    else if (!mstRegex.Match(mst).Success)
                    {
                        return Json("-1");
                    }
                    else if (new CustomerDAO().checkExitsMail(email.Trim()))
                    {
                        return Json("-3");
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
                        lastIDMedia = new MediaDAO().insertMedia(newFname, "/Assets/dist/img/Resource/" + newFname, session.accountID);

                    }
                    int cusID = new CustomerDAO().addCustomer(supplierName, lastIDMedia, address, deliver_address, supplierPhone, email, mst);
                    if (cusID == -1)
                    {
                        return Json("-1");
                    }

                    return Json(cusID);
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

        public ActionResult ChiTiet(int customerID)
        {
            ChiTietKhachHangModel model = new ChiTietKhachHangModel();
            //get customer information
            model.customer = new Customer();
            model.customer = new CustomerDAO().getCustomerById(customerID);
            model.avatar_str = new MediaDAO().getMediaByID((int)model.customer.Media_ID).Location;
            //get order of customer
            model.lstTotal = new List<Order_total>();
            model.lstTotal = new OrderTotalDAO().getListByCustomerID(customerID);
            //get Data LineChart
            int currentMonth = DateTime.Now.Month;
            DateTime beginDate = new DateTime(DateTime.Now.Year, currentMonth, 1);
            DateTime endDate = beginDate.AddMonths(1).AddDays(-1);
            model.dataLineChart = new Dictionary<string, decimal>();
            for (var i = 0; i < 6; i++)
            {
                model.dataLineChart.Add(beginDate.ToString("MM/yyyy"), new OrderTotalDAO().getDataKhachHangByID(beginDate, endDate, customerID));
                beginDate = beginDate.AddMonths(-1);
                endDate = endDate.AddMonths(-1);
            }
            model.dataLineChart = model.dataLineChart.Reverse().ToDictionary(x => x.Key, x => x.Value);
            //get current order
            model.numberOrder = new OrderTotalDAO().getOrderByCustomerID(DateTime.Now.AddMonths(-6), DateTime.Now, customerID).Count;
            //get current order total price
            model.priceOrder = (decimal)new OrderTotalDAO().getOrderByCustomerID(DateTime.Now.AddMonths(-6), DateTime.Now, customerID).Sum(x => x.Total_price);
            //get current dept
            model.currentDebt = (decimal)new CustomerDAO().getCustomerById(customerID).Current_debt;
            //get Data Donut Chart
            Dictionary<string, int?> dataDonutChart = new OrderItemDAO().getProductCustomerBought(customerID);
            model.displayDonutChart = dataDonutChart.Take(5).ToDictionary(x => x.Key, x => x.Value);
            if (model.displayDonutChart.Count != 0)
            {
                int otherQuantity = (int)dataDonutChart.Sum(x => x.Value) - (int)model.displayDonutChart.Sum(x => x.Value);
                model.displayDonutChart.Add("Khác", otherQuantity);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateCustomer()
        {
            try
            {
                var session = (UserSession)Session[CommonConstants.USER_SESSION];

                var supplierName = Request.Form.GetValues("supplierName")[0];
                var supplierPhone = Request.Form.GetValues("supplierPhone")[0];
                var address = Request.Form.GetValues("address")[0];
                var deliver_address = Request.Form.GetValues("deliver_address")[0];
                var email = Request.Form.GetValues("email")[0];
                var mst = Request.Form.GetValues("mst")[0];
                var acronym = Request.Form.GetValues("acronym")[0];
                var customerID = Request.Form.GetValues("customerID")[0];
                Regex phoneRegex = new Regex(CommonConstants.PHONE_REGEX, RegexOptions.IgnoreCase);
                Regex mstRegex = new Regex(CommonConstants.MST_REGEX, RegexOptions.IgnoreCase);
                MailAddress m = new MailAddress(email);
                if (string.IsNullOrEmpty(supplierName))
                {
                    return Json("-1");
                }
                else if (!phoneRegex.Match(supplierPhone).Success)
                {
                    return Json("-1");
                }
                else if (string.IsNullOrEmpty(address))
                {
                    return Json("-1");
                }
                else if (string.IsNullOrEmpty(deliver_address))
                {
                    return Json("-1");
                }
                else if (!mstRegex.Match(mst).Success)
                {
                    return Json("-1");
                }
                else if (new CustomerDAO().checkExitsMail(email.Trim()))
                {
                    return Json("-3");
                }

                // Checking no of files injected in Request object  
                if (Request.Files.Count > 0)
                {

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
                        lastIDMedia = new MediaDAO().insertMedia(newFname, "/Assets/dist/img/Resource/" + newFname, session.accountID);

                    }
                    int cusID = new CustomerDAO().editCustomer(supplierName, lastIDMedia, address, deliver_address, supplierPhone, email, mst, int.Parse(customerID), acronym);
                    if (cusID == -1)
                    {
                        return Json("-1");
                    }

                    return Json(cusID);
                }
                else
                {
                    var lastIDMedia = new CustomerDAO().getCustomerById(int.Parse(customerID)).Media_ID;
                    int cusID = new CustomerDAO().editCustomer(supplierName, lastIDMedia, address, deliver_address, supplierPhone, email, mst, int.Parse(customerID), acronym);
                    if (cusID == -1)
                    {
                        return Json("-1");
                    }
                    return Json(cusID);
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