using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.KeToan.Models;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.KeToan.Controllers
{
    public class ChiTietPhieuController : KeToanBaseController
    {
        // GET: KeToan/ChiTietPhieu
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
            model.invoiceNumber = data.Order_part.FirstOrDefault().Invoice_number;
            model.status = statusDAO.getStatus(data.Status_ID);
            model.statusId = data.Status_ID;
            var customer = customerDAO.getCustomerById(data.Customer_ID);
            model.customerName = customer.Customer_name;
            model.taxCode = data.Tax_code;
            var items = new List<OrderItemModel>();
            model.qttTotal = 0;
            model.boxTotal = 0;
            foreach (Order_items o in data.Order_items)
            {
                if (o.Order_part_ID == null)
                {
                    var product = productDAO.getProductById(o.Product_ID);
                    var item = new OrderItemModel
                    {
                        code = product.Product_code,
                        param = product.Product_parameters,
                        Box = o.Box,
                        Discount = o.Discount,
                        Price = o.Price,
                        Quantity = o.Quantity,
                        per = product.Price_before_VAT_VND * (100 + product.VAT) / 100,
                        priceBeforeDiscount = o.Discount > 0 ? (o.Price * 100 / (100 + o.Discount)) : o.Price
                    };
                    items.Add(item);
                    model.qttTotal += o.Quantity;
                    model.boxTotal += o.Box;
                }
            }
            model.discount = data.Order_discount;
            model.vat = data.VAT;
            model.subTotal = data.Sub_total;
            model.discountMoney = data.Order_discount > 0 ? (data.Sub_total * data.Order_discount / 100) : 0;
            model.afterDiscountMoney = data.Order_discount > 0 ? data.Sub_total - model.discountMoney : data.Sub_total;
            model.total = data.Total_price;
            model.readItems = items;
            return View(model);
        }

        [HttpPost]
        public JsonResult CheckOut(String orderId, decimal? vat, String invoiceNumber)
        {
            try
            {
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var dao = new OrderTotalDAO();
                dao.keToan_checkOut(orderId, session.user_id, vat, invoiceNumber);
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