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
    public class DanhSachCongNoKhachHangController : Controller
    {
        // GET: PhanPhoi/DanhSachCongNoKhachHang
        public ActionResult Index()
        {
            try
            {
                DanhSachCongNoKhachHangModel model = new DanhSachCongNoKhachHangModel();
                Customer_transactionDAO dao = new Customer_transactionDAO();

                model.lstDisplay = new List<DanhSachNoKhachHang>();
                model.lstDisplay = dao.danhSachKhachHang();

                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public JsonResult autoComplete(string searchValue)
        {
            Customer_transactionDAO dao = new Customer_transactionDAO();
            DanhSachCongNoKhachHangModel model = new DanhSachCongNoKhachHangModel();
            var lstAll = dao.getListAuto(searchValue);
            model.lstAutoComplete = lstAll.Select(x => new Autocomplete()
            {
                key = x.key,
                value = x.value
            }).ToList();
            return new JsonResult { Data = model.lstAutoComplete, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public ActionResult Index(string customerName, string noTu, string noDen)
        {
            try
            {
                DanhSachCongNoKhachHangModel model = new DanhSachCongNoKhachHangModel();
                Customer_transactionDAO dao = new Customer_transactionDAO();

                model.lstDisplay = new List<DanhSachNoKhachHang>();

                model.lstDisplay = dao.getListSearchCustomer(customerName, noTu, noDen);

                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult UpdateData(int customerId, string noCu, string nhapTrongKy, string vat, string thanhToan, string conNo, string dienGiai, string tongCong)
        {
            try
            {
                Customer_transactionDAO dao = new Customer_transactionDAO();
                //var session = (UserSession)Session[CommonConstants.USER_SESSION];
                //int userId = session.accountID;
                dao.insertData(customerId, nhapTrongKy, vat, thanhToan, conNo, dienGiai, 1.ToString(), tongCong);
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