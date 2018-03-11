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
    public class LapPhieuController : Controller
    {
        // GET: PhanPhoi/LapPhieu
        public ActionResult Index()
        {
            var ddl = new List<SelectListItem>();
            var dao = new CustomerDAO();
            var model = new OrderTotalModel();
            var lstCustomer = dao.getCustomer();
            lstCustomer.ForEach(x =>
            {
                ddl.Add(new SelectListItem { Text = x.Customer_name, Value = x.Customer_ID.ToString() });
            });
            model.lstCustomer = ddl;
            model.deliveryQtt = 1;
            model.rate = 2016;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(OrderTotalModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult ChangeCustomer(String customerId)
        {
            try
            {
                var model = new OrderTotalModel();
                var dao = new CustomerDAO();
                var customer = dao.getCustomerById(customerId);
                model.deliveryAddress = customer.Delivery_address;
                model.taxCode = customer.Tax_code;
                model.invoiceAddress = customer.Export_invoice_address;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult ChooseProduct(String input)
        {
            try
            {
                var dao = new ProductDAO();
                return Json(dao.getProduct(input), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}

