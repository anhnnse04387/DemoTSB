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
            var model = new OrderDeliveryModel();
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
            model.status = statusDAO.getStatus(data.Status_ID);
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

    }
}