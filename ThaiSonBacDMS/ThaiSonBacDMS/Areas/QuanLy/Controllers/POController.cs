using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanLy.Models;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers
{
    public class POController : QuanLyBaseController
    {
        // GET: PhanPhoi/PO
        public ActionResult Index()
        {
            var ddl = new List<SelectListItem>();
            var dao = new SupplierDAO();
            var model = new POModel();
            var lstSupplier = dao.getSupplier();
            lstSupplier.ForEach(x =>
            {
                ddl.Add(new SelectListItem { Text = x.Supplier_name, Value = x.Supplier_ID.ToString() });
            });
            model.lstSupplier = ddl;
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var poDAO = new PODAO();
            var productDAO = new ProductDAO();
            var supplierDAO = new SupplierDAO();
            var data = poDAO.getPO(id);
            var model = new POModel();
            model.PO_no = data.PO_no;
            var supplier = supplierDAO.getSupplierById(data.Supplier_ID);
            model.supplier = supplier.Supplier_name;
            model.address = supplier.Supplier_address;
            model.tel = supplier.Phone;
            model.email = supplier.Mail;
            model.Payment = data.Payment;
            model.Date_create = data.Date_create;
            model.Date_request_ex_work = data.Date_request_ex_work;
            model.Total_price = data.Total_price;
            int? totalQtt = 0;
            var readItems = new List<POItemModel>();
            foreach (PO_Items i in data.PO_Items)
            {
                var product = productDAO.getProductById(i.Product_ID);
                var item = new POItemModel
                {
                    product = product.Product_code,
                    NOTE = i.NOTE,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    per = product.Price_before_VAT_VND * (100 + product.VAT) / 100
                };
                totalQtt += i.Quantity;
                readItems.Add(item);
            }
            model.readItems = readItems;
            model.totalQtt = totalQtt;
            return View(model);
        }

        public ActionResult ChangeSupplier(int? supplierId)
        {
            try
            {
                var model = new POModel();
                var dao = new SupplierDAO();
                var supplier = dao.getSupplierById(supplierId);
                model.address = supplier.Supplier_address;
                model.tel = supplier.Phone;
                model.email = supplier.Mail;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }

        public ActionResult ChooseProduct(String input)
        {
            try
            {
                var dao = new ProductDAO();
                var lst = dao.getProduct(input);
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult CheckOut(POModel model)
        {
            try
            {
                if (!String.IsNullOrEmpty(model.Date_create.ToString()) &&
                    !String.IsNullOrEmpty(model.Date_request_ex_work.ToString()) &&
                    !String.IsNullOrEmpty(model.Payment) && !String.IsNullOrEmpty(model.PO_no)
                    && model.Supplier_ID > 0)
                {
                    var session = (UserSession)Session[CommonConstants.USER_SESSION];
                    var dao = new PODAO();
                    var itemDAO = new POItemDAO();
                    var result = dao.createPO(new PO
                    {
                        PO_no = model.PO_no,
                        Supplier_ID = model.Supplier_ID,
                        Date_request_ex_work = model.Date_request_ex_work,
                        Date_create = model.Date_create,
                        Payment = model.Payment,
                        User_ID = session.user_id,
                        Total_price = model.Total_price
                    });
                    if (result > 0)
                    {
                        foreach (PO_Items i in model.items)
                        {
                            i.PO_ID = result;
                            itemDAO.createPOItem(i);
                        }
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}