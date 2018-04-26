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
    public class DanhSachCongNoCungCapController : PhanPhoiBaseController
    {
        // GET: PhanPhoi/DanhSachCongNoCungCap
        public ActionResult Index()
        {
            DanhSachCongNoCungCapModel model = new DanhSachCongNoCungCapModel();
            Supplier_transactionDAO dao = new Supplier_transactionDAO();
            model.lstDisplay = new List<DanhSachNoCungCap>();

            model.lstDisplay = dao.listDebtSupplier();


            return View(model);
        }

        [HttpPost]
        public ActionResult InsertData(string supplierId, string nhapTrongKy, string thanhToan, string conNo, string noDauKy, string dienGiai)
        {
            Supplier_transactionDAO dao = new Supplier_transactionDAO();
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            int userId = session.accountID;
            dao.insertNewSupplierDebt(supplierId, nhapTrongKy, thanhToan, conNo, noDauKy, dienGiai, userId);
            return Json(new { success = true, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public JsonResult autoComplete(string searchValue)
        {
            Supplier_transactionDAO dao = new Supplier_transactionDAO();
            DanhSachCongNoCungCapModel model = new DanhSachCongNoCungCapModel();
            var lstAll = dao.autoCompleteNameSearch(searchValue);
            model.lstAutoComplete = lstAll.Select(x => new Autocomplete
            {
                key = x.key,
                value = x.value
            }).ToList();
            return new JsonResult { Data = model.lstAutoComplete, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public ActionResult Index(string supplierId, string conNoTu, string conNoDen)
        {
            try
            {

                Supplier_transactionDAO dao = new Supplier_transactionDAO();
                conNoTu = conNoTu.Replace(",", "");
                conNoDen = conNoDen.Replace(",", "");
                DanhSachCongNoCungCapModel model = new DanhSachCongNoCungCapModel();
                model.lstDisplay = dao.lstSearchDebtSupplier(supplierId, conNoTu, conNoDen);
                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }
    }

}