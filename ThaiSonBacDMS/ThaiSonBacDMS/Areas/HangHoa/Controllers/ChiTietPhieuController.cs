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
    public class ChiTietPhieuController : Controller
    {
        // GET: PhanPhoi/ChiTietPhieu
        public ActionResult Index(String orderId)
        {
            var dao = new OrderTotalDAO();
            var customerDAO = new CustomerDAO();
            var data = dao.getOrder(orderId);
            var statusDAO = new StatusDAO();
            var model = new OrderTotalModel();
            var productDAO = new ProductDAO();
            model.orderId = orderId;
            model.invoiceAddress = "Công ty TNHH Thái Sơn Bắc";
            model.deliveryAddress = data.Address_delivery;
            model.deliveryQtt = data.Order_part.Count;
            model.status = statusDAO.getStatus(data.Status_ID);
            var customer = customerDAO.getCustomerById(data.Customer_ID);
            model.customerName = customer.Customer_name;
            model.taxCode = customer.Tax_code;
            var items = new List<OrderItemModel>();
            foreach (Order_items o in data.Order_items)
            {
                if (o.Order_part_ID == null)
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
            var dao = new OrderTotalDAO();
            var customerDAO = new CustomerDAO();
            var data = dao.getOrder(orderId);
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
            model.deliveryAddress = data.Address_delivery;
            model.deliveryQtt = data.Order_part.Count;
            model.status = statusDAO.getStatus(data.Status_ID);
            var customer = customerDAO.getCustomerById(data.Customer_ID);
            model.customerName = customer.Customer_name;
            model.taxCode = customer.Tax_code;
            model.shipper = shipper;
            model.deliveryMethod = delivery;
            model.driver = driver;
            return View(model);
        }

        [HttpPost]
        public JsonResult DeliveryCheckOut(String orderId, byte? DeliverMethod_ID, string Driver_ID, string Shiper_ID)
        {
            try
            {
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var dao = new OrderTotalDAO();
                dao.delivery_checkOut(orderId, session.user_id, DeliverMethod_ID, Driver_ID, Shiper_ID);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CheckOut(String orderId)
        {
            try
            {
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var dao = new OrderTotalDAO();
                dao.kho_checkOut(orderId, session.user_id);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult cancelOrder(String orderId, String note)
        {
            try
            {
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var dao = new OrderTotalDAO();
                dao.cancelOrder(orderId, note, session.user_id);
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