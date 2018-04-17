using Models.DAO;
using Models.DAO_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class ChiTietNoKhachHangController : Controller
    {
        // GET: PhanPhoi/ChiTietNoKhachHang
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                ChiTietNoKhachHangModel model = new ChiTietNoKhachHangModel();
                Customer_transactionDAO dao = new Customer_transactionDAO();
                CustomerDAO daoCustomer = new CustomerDAO();

                model.lstDisplay = new List<ChiTietNoKhachHang>();
                model.lstDisplay = dao.getDetailTransaction(1);
                model.customerName = daoCustomer.getCustomerById(1).Customer_name;
                model.customerId = daoCustomer.getCustomerById(1).Customer_ID.ToString();
                model.lastedDebt = dao.getLastestDebt(1);

                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Index(string customerId, string dateFrom, string dateTo)
        {

            ChiTietNoKhachHangModel model = new ChiTietNoKhachHangModel();
            model.dateFrom = dateFrom;
            model.dateTo = dateTo;
            Customer_transactionDAO dao = new Customer_transactionDAO();
            CustomerDAO daoCustomer = new CustomerDAO();

            model.lstDisplay = new List<ChiTietNoKhachHang>();
            if (!String.IsNullOrEmpty(customerId))
            {
                model.lstDisplay = dao.getSearchData(Convert.ToInt32(customerId), dateFrom, dateTo);
            }
            model.lastedDebt = dao.getLastestDebt(1);
            model.customerName = daoCustomer.getCustomerById(1).Customer_name;
            model.customerId = daoCustomer.getCustomerById(1).Customer_ID.ToString();
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertData(string customerId, string tienHang, string vat, string thanhToan, string duNo, string dienGiai,string tongCong)
        {
            try
            {
                ChiTietNoKhachHang model = new ChiTietNoKhachHang();
                Customer_transactionDAO dao = new Customer_transactionDAO();
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                int userId = session.accountID;
                int rowInserted = dao.insertData(Convert.ToInt32(customerId), tienHang, vat, thanhToan, duNo, dienGiai, userId,tongCong);
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