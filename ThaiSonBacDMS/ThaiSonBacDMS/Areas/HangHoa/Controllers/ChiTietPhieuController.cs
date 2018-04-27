using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.HangHoa.Models;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.HangHoa.Controllers
{
    public class ChiTietPhieuController : HangHoaBaseController
    {
        // GET: PhanPhoi/ChiTietPhieu
        public ActionResult Index(String orderId)
        {
            var dao = new OrderPartDAO();
            var customerDAO = new CustomerDAO();
            var data = dao.getOrderPart(orderId);
            var statusDAO = new StatusDAO();
            var model = new OrderTotalModel();
            var productDAO = new ProductDAO();
            model.orderId = orderId;
            model.invoiceAddress = "Công ty TNHH Thái Sơn Bắc";
            model.deliveryAddress = data.Order_total.Address_delivery;
            model.invoiceNumber = data.Invoice_number;
            model.status = statusDAO.getStatus(data.Status_ID);
            var customer = customerDAO.getCustomerById(data.Customer_ID);
            model.customerName = customer.Customer_name;
            model.dateTakeInvoice = data.Date_take_invoice.ToString();
            model.dateTakeBallot = data.Date_take_ballot.ToString();
            model.taxCode = data.Order_total.Tax_code;
            var items = new List<OrderItemModel>();
            if (orderId.Contains("-"))
            {
                foreach (Order_items o in data.Order_total.Order_items.Where(x => x.Order_part_ID != null).ToList())
                {
                    if (o.Order_part_ID.Equals(data.Order_part_ID))
                    {
                        var product = productDAO.getProductById(o.Product_ID);
                        var item = new OrderItemModel
                        {
                            code = product.Product_code,
                            Box = o.Box,
                            Quantity = o.Quantity
                        };
                        items.Add(item);
                    }
                }
            }
            else
            {
                foreach (Order_items o in data.Order_total.Order_items.Where(x => x.Order_part_ID == null).ToList())
                {
                    var product = productDAO.getProductById(o.Product_ID);
                    var item = new OrderItemModel
                    {
                        code = product.Product_code,
                        Box = o.Box,
                        Quantity = o.Quantity
                    };
                    items.Add(item);
                }
            }
            model.readItems = items;
            return View(model);
        }

        [HttpGet]
        public ActionResult Delivery(String orderId)
        {
            var dao = new OrderPartDAO();
            var customerDAO = new CustomerDAO();
            var data = dao.getOrderPart(orderId);
            var statusDAO = new StatusDAO();
            var daoDelivery = new DeliveryMethodDAO();
            var shipper = new List<SelectListItem>();
            var delivery = new List<SelectListItem>();
            var driver = new List<SelectListItem>();
            var daoUser = new UserDAO();
            var model = new OrderTotalModel();
            var lstDelivery = daoDelivery.getLstDelivery();
            var lstShipper = daoUser.getLstShipper();
            var lstDriver = daoUser.getLstDriver();
            lstDelivery.ForEach(x =>
            {
                delivery.Add(new SelectListItem { Text = x.Method_name, Value = x.Method_ID.ToString() });
            });
            lstShipper.ForEach(x =>
            {
                shipper.Add(new SelectListItem { Text = x.User_name, Value = x.User_ID.ToString() });
            });
            lstDriver.ForEach(x =>
            {
                driver.Add(new SelectListItem { Text = x.User_name, Value = x.User_ID.ToString() });
            });
            model.orderId = orderId;
            model.invoiceAddress = "Công ty TNHH Thái Sơn Bắc";
            model.deliveryAddress = data.Order_total.Address_delivery;
            model.invoiceNumber = data.Invoice_number;
            String status = statusDAO.getStatus(data.Status_ID);
            String spanClass = "";
            if (data.Status_ID == 10 || data.Status_ID == 11)
            {
                status = status.Substring(0, status.IndexOf("warning") - 1);
                spanClass = "label-warning";
            }
            else
            {
                spanClass = "label-primary";
            }
            model.spanClass = spanClass;
            model.status = status;
            var customer = customerDAO.getCustomerById(data.Customer_ID);
            model.customerName = customer.Customer_name;
            model.taxCode = data.Order_total.Tax_code;
            model.shipper = shipper;
            model.deliveryMethod = delivery;
            model.dateReceiveInvoice = data.Date_reveice_invoice.ToString();
            model.dateReceiveBallot = data.Date_reveice_ballot.ToString();
            model.driver = driver;
            return View(model);
        }

        public ActionResult Full(String orderId)
        {
            var productDAO = new ProductDAO();
            var dao = new OrderPartDAO();
            var customerDAO = new CustomerDAO();
            var data = dao.getOrderPart(orderId);
            var detailStatusDAO = new OrderDetailStatusDAO();
            var statusDAO = new StatusDAO();
            var daoDelivery = new DeliveryMethodDAO();
            var shipper = new List<SelectListItem>();
            var delivery = new List<SelectListItem>();
            var driver = new List<SelectListItem>();
            var daoUser = new UserDAO();
            var model = new OrderTotalModel();
            var lstDelivery = daoDelivery.getLstDelivery();
            var lstShipper = daoUser.getLstShipper();
            var lstDriver = daoUser.getLstDriver();
            lstDelivery.ForEach(x =>
            {
                delivery.Add(new SelectListItem { Text = x.Method_name, Value = x.Method_ID.ToString() });
            });
            lstShipper.ForEach(x =>
            {
                shipper.Add(new SelectListItem { Text = x.User_name, Value = x.User_ID.ToString() });
            });
            lstDriver.ForEach(x =>
            {
                driver.Add(new SelectListItem { Text = x.User_name, Value = x.User_ID.ToString() });
            });
            model.Driver_ID = data.Driver_ID;
            model.DeliverMethod_ID = data.DeliverMethod_ID;
            model.Shiper_ID = data.Shiper_ID;
            model.orderId = orderId;
            model.invoiceAddress = "Công ty TNHH Thái Sơn Bắc";
            model.deliveryAddress = data.Order_total.Address_delivery;
            model.invoiceNumber = data.Invoice_number;
            String status = statusDAO.getStatus(data.Status_ID);
            String spanClass = "";
            if (data.Status_ID == 10 || data.Status_ID == 11)
            {
                status = status.Substring(0, status.IndexOf("warning") - 1);
                spanClass = "label-warning";
            }
            else
            {
                spanClass = "label-primary";
            }
            model.spanClass = spanClass;
            model.status = status;
            var customer = customerDAO.getCustomerById(data.Customer_ID);
            model.customerName = customer.Customer_name;
            model.taxCode = data.Order_total.Tax_code;
            model.shipper = shipper;
            model.deliveryMethod = delivery;
            model.dateReceiveBallot = data.Date_reveice_ballot.ToString();
            model.dateReceiveInvoice = data.Date_reveice_invoice.ToString();
            model.dateTakeInvoice = data.Date_take_invoice.ToString();
            model.dateTakeBallot = data.Date_take_ballot.ToString();
            model.dateExport = data.Request_stockout_date.Value.ToString("MM/dd/yyyy");
            if (data.Date_completed != null)
            {
                model.dateComplete = data.Date_completed.Value.ToString("MM/dd/yyyy");
            }
            var statusWh = detailStatusDAO.getStatus(orderId).Where(x => (x.Status_ID == 5 || x.Status_ID == 10)).FirstOrDefault();
            if (statusWh != null)
            {
                model.dateWarehouse = statusWh.Date_change.Value.ToString("MM/dd/yyyy");
            }
            model.driver = driver;
            var items = new List<OrderItemModel>();
            foreach (Order_items o in data.Order_total.Order_items)
            {
                if (o.Order_part_ID != null)
                {
                    if (o.Order_part_ID.Equals(data.Order_part_ID))
                    {
                        var product = productDAO.getProductById(o.Product_ID);
                        var item = new OrderItemModel
                        {
                            code = product.Product_code,
                            Box = o.Box,
                            Quantity = o.Quantity
                        };
                        items.Add(item);
                    }
                }
                else
                {
                    var product = productDAO.getProductById(o.Product_ID);
                    var item = new OrderItemModel
                    {
                        code = product.Product_code,
                        Box = o.Box,
                        Quantity = o.Quantity
                    };
                    items.Add(item);
                }
            }
            model.readItems = items;
            return View(model);
        }

        [HttpPost]
        public JsonResult DeliveryCheckOut(String orderId, byte? DeliverMethod_ID, string Driver_ID, int Shiper_ID, bool receiveInvoice, bool receiveBallot)
        {
            try
            {
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var dao = new OrderTotalDAO();
                dao.delivery_checkOut(orderId, session.user_id, DeliverMethod_ID, Driver_ID, Shiper_ID, receiveInvoice, receiveBallot);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CheckOut(String orderId, bool takeInvoice, bool takeBallot)
        {
            try
            {
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var dao = new OrderTotalDAO();
                dao.kho_checkOut(orderId, session.user_id, takeInvoice, takeBallot);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveOrder(String orderId, bool takeInvoice, bool takeBallot, bool receiveInvoice, bool receiveBallot)
        {
            try
            {
                var dao = new OrderTotalDAO();
                dao.kho_updateOrder(orderId, takeInvoice, takeBallot, receiveInvoice, receiveBallot);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CancelOrder(String orderId, String note)
        {
            try
            {
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var dao = new OrderTotalDAO();
                dao.cancelOrder(orderId, note, session.user_id, dao.getOrder(orderId).Order_part.Count);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExchangeOrder(String orderId, String note)
        {
            try
            {
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var dao = new OrderTotalDAO();
                dao.exchangeOrder(orderId, note, session.user_id);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Complete(String orderId)
        {
            try
            {
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var dao = new OrderTotalDAO();
                dao.completeOrder(orderId, session.user_id);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}