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
    public class ChiTietNoCungCapController : PhanPhoiBaseController
    {
        // GET: PhanPhoi/ChiTietNoCungCap
        public ActionResult Index(string supplierId)
        {
            try
            {
                ChiTietNoCungCapModel model = new ChiTietNoCungCapModel();
                Supplier_transactionDAO dao = new Supplier_transactionDAO();
                SupplierDAO supplierDao = new SupplierDAO();

                model.lstDisplay = new List<ChiTietNoCungCap>();
                model.lstDisplay = dao.getLst(4);
                model.supplierName = supplierDao.getSupplierName(4);
                model.supplierId = 4;
                model.lastedDebt = dao.getLastestDebt(4);

                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Index(string supplierId, string fromDate, string toDate)
        {
            ChiTietNoCungCapModel model = new ChiTietNoCungCapModel();
            Supplier_transactionDAO dao = new Supplier_transactionDAO();
            SupplierDAO supplierDao = new SupplierDAO();

            model.supplierName = supplierDao.getSupplierName(4);
            model.supplierId = 4;
            model.lastedDebt = dao.getLastestDebt(4);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertData(string supplierId,string ngay, string dienGiai, string noCu, string tienHang, string thanhToan, string duNo, string ghiChu)
        {
            try
            {
                Supplier_transactionDAO dao = new Supplier_transactionDAO();
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                int userId = session.accountID;
                dao.insertData(supplierId,ngay, dienGiai, noCu, tienHang, thanhToan, duNo, ghiChu,userId);
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