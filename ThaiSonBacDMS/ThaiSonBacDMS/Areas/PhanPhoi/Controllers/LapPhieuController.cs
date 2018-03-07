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
            model.lstItem = new List<OrderItemModel>();
            return View(model);
        }

        [HttpPost]
        public PartialViewResult CreateItem(OrderTotalModel model)
        {
            model.lstItem.Add(new OrderItemModel());
            return PartialView("Items", model);
        }

        [HttpPost]
        public ActionResult Index(OrderTotalModel model)
        {
            // Save model to DB
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult changeCustomer(OrderTotalModel model)
        {
            var dao = new CustomerDAO();
            var customer = dao.getCustomerById(model.customerId);
            model.deliveryAddress = customer.Delivery_address;
            model.taxCode = customer.Tax_code;
            model.invoiceAddress = customer.Export_invoice_address;
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
