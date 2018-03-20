using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class LapPhieuController : Controller
    {
        // GET: PhanPhoi/LapPhieu
        public ActionResult Index()
        {
            var ddl = new List<SelectListItem>();
            var dao = new CustomerDAO();
            var orderDAO = new OrderTotalDAO();
            var model = new OrderTotalModel();
            var lstCustomer = dao.getCustomer();
            lstCustomer.ForEach(x =>
            {
                ddl.Add(new SelectListItem { Text = x.Customer_name, Value = x.Customer_ID.ToString() });
            });
            model.lstCustomer = ddl;
            model.orderId = "O" + orderDAO.getNextValueID();
            model.deliveryQtt = 0;
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveOrder(OrderTotalModel model)
        {
            try
            {
                if (!String.IsNullOrEmpty(model.orderId) && !String.IsNullOrEmpty(model.customerId)
                    && !String.IsNullOrEmpty(model.deliveryAddress) && !String.IsNullOrEmpty(model.invoiceAddress)
                    && !String.IsNullOrEmpty(model.taxCode) && model.rate > 0 && model.items != null && model.items.Count > 0)
                {
                    var session = (UserSession)Session[CommonConstants.USER_SESSION];
                    var orderDAO = new OrderTotalDAO();
                    var orderPartDAO = new OrderPartDAO();
                    var orderStatusDAO = new OrderDetailStatusDAO();
                    var orderItemDAO = new OrderItemDAO();
                    var result = orderDAO.createOrder(new Order_total
                    {
                        Order_ID = model.orderId,
                        Address_delivery = model.deliveryAddress,
                        Address_invoice_issuance = model.invoiceAddress,
                        Customer_ID = model.customerId,
                        Date_created = DateTime.Now,
                        Rate = model.rate,
                        User_ID = session.user_id,
                        Sub_total = model.subTotal,
                        VAT = model.vat,
                        Total_price = model.total,
                        Order_discount = model.discount,
                        Status_ID = 0
                    });
                    if (result != null)
                    {
                        orderStatusDAO.createOrderStatus(new Order_detail_status
                        {
                            Order_ID = model.orderId,
                            Status_ID = 0,
                            Date_change = DateTime.Now,
                            User_ID = session.user_id
                        });
                        foreach (Order_items o in model.items)
                        {
                            o.Order_ID = model.orderId;
                            orderItemDAO.createOrderItem(o);
                        }
                        if (model.deliveryQtt > 0)
                        {
                            foreach (Order_part o in model.part)
                            {
                                o.Order_ID = model.orderId;
                                o.Date_created = DateTime.Now;
                                o.Status_ID = 0;
                                o.Customer_ID = model.customerId;
                                if (orderPartDAO.createOrderPart(o) != null)
                                {
                                    orderStatusDAO.createOrderStatus(new Order_detail_status
                                    {
                                        Order_ID = model.orderId,
                                        Order_part_ID = o.Order_part_ID,
                                        Status_ID = 0,
                                        Date_change = DateTime.Now,
                                        User_ID = session.user_id
                                    });
                                }
                            }
                        }
                    }
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
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
    }
}

