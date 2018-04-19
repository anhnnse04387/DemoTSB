using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanLy.Models;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers
{
    public class LapPhieuController : Controller
    {
        // GET: QuanLy/LapPhieu
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
            model.deliveryQtt = 1;
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveOrder(OrderTotalModel model)
        {
            try
            {
                if (!String.IsNullOrEmpty(model.orderId) && model.customerId > 0
                    && !String.IsNullOrEmpty(model.deliveryAddress) && !String.IsNullOrEmpty(model.invoiceAddress)
                    && !String.IsNullOrEmpty(model.taxCode) && model.rate > 0 && model.items != null && model.items.Count > 0)
                {
                    var result = 0;
                    var session = (UserSession)Session[CommonConstants.USER_SESSION];
                    var orderDAO = new OrderTotalDAO();
                    var orderPartDAO = new OrderPartDAO();
                    var orderStatusDAO = new OrderDetailStatusDAO();
                    var orderItemDAO = new OrderItemDAO();
                    if (orderDAO.checkOrder(model.orderId) == 0)
                    {
                        orderDAO.createOrder(new Order_total
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
                            Status_ID = 1
                        });
                        result++;
                    }
                    else
                    {
                        orderDAO.updateOrder(new Order_total
                        {
                            Order_ID = model.orderId,
                            Address_delivery = model.deliveryAddress,
                            Address_invoice_issuance = model.invoiceAddress,
                            Customer_ID = model.customerId,
                            Rate = model.rate,
                            User_ID = session.user_id,
                            Sub_total = model.subTotal,
                            VAT = model.vat,
                            Total_price = model.total,
                            Order_discount = model.discount,
                            Status_ID = 1
                        });
                        orderDAO.deleteContent(model.orderId);
                        result++;
                    }
                    if (result > 0)
                    {
                        orderStatusDAO.createOrderStatus(new Order_detail_status
                        {
                            Order_ID = model.orderId,
                            Status_ID = 1,
                            Date_change = DateTime.Now,
                            User_ID = session.user_id
                        });
                        foreach (Order_items o in model.items)
                        {
                            o.Order_ID = model.orderId;
                            orderItemDAO.createOrderItem(o);
                        }
                        if (model.deliveryQtt > 1)
                        {
                            foreach (Order_part o in model.part)
                            {
                                o.Order_ID = model.orderId;
                                o.Date_created = DateTime.Now;
                                o.Status_ID = 1;
                                o.Customer_ID = model.customerId;
                                if (orderPartDAO.createOrderPart(o) != null)
                                {
                                    orderStatusDAO.createOrderStatus(new Order_detail_status
                                    {
                                        Order_ID = model.orderId,
                                        Order_part_ID = o.Order_part_ID,
                                        Status_ID = 1,
                                        Date_change = DateTime.Now,
                                        User_ID = session.user_id
                                    });
                                }
                            }
                        }
                        else
                        {
                            orderPartDAO.createOrderPart(new Order_part
                            {
                                Order_ID = model.orderId,
                                Part_ID = 1,
                                Order_part_ID = model.orderId + "-1",
                                Customer_ID = model.customerId,
                                Date_created = DateTime.Now,
                                Date_reveice_invoice = DateTime.Now,
                                VAT = model.vat,
                                Status_ID = 1,
                                Total_price = model.total
                            });
                            foreach (Order_items o in model.items)
                            {
                                o.Order_ID = model.orderId;
                                o.Order_part_ID = model.orderId + "-1";
                                orderItemDAO.createOrderItem(o);
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

        public ActionResult ChangeCustomer(int customerId)
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

        public ActionResult SuaPhieu(String orderId)
        {
            var ddl = new List<SelectListItem>();
            var dao = new OrderTotalDAO();
            var customerDAO = new CustomerDAO();
            var lstCustomer = customerDAO.getCustomer();
            lstCustomer.ForEach(x =>
            {
                ddl.Add(new SelectListItem { Text = x.Customer_name, Value = x.Customer_ID.ToString() });
            });
            var data = dao.getOrder(orderId);
            var statusDAO = new StatusDAO();
            var model = new OrderTotalModel();
            var productDAO = new ProductDAO();
            model.lstCustomer = ddl;
            model.orderId = orderId;
            ddl = new List<SelectListItem>();
            var lstStatus = statusDAO.getLstStatus();
            lstStatus.ForEach(x =>
            {
                ddl.Add(new SelectListItem { Text = x.Status_name, Value = x.Status_ID.ToString() });
            });
            model.lstStatus = ddl;
            model.status = statusDAO.getStatus(data.Status_ID);
            var customer = customerDAO.getCustomerById(data.Customer_ID);
            model.invoiceAddress = data.Address_invoice_issuance;
            model.deliveryAddress = data.Address_delivery;
            model.deliveryQtt = data.Order_part.Count;
            model.customerId = data.Customer_ID;
            model.rate = data.Rate;
            model.taxCode = customer.Tax_code;
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
                        priceBeforeDiscount = o.Discount > 0 ? (o.Price * 100 / (100 + o.Discount)) : o.Price,
                        productId = o.Product_ID,
                        qttBox = product.Quantity_in_carton,
                        qttInven = product.Quantities_in_inventory
                    };
                    items.Add(item);
                    model.qttTotal += o.Quantity;
                    model.boxTotal += o.Box;
                }
            }
            model.discount = data.Order_discount;
            model.subTotal = data.Sub_total;
            model.discountMoney = data.Order_discount > 0 ? (data.Sub_total * (100 - data.Order_discount) / 100) : 0;
            model.afterDiscountMoney = data.Order_discount > 0 ? data.Sub_total - model.discountMoney : data.Sub_total;
            model.vatMoney = data.VAT > 0 ? (model.afterDiscountMoney * (100 + data.VAT) / 100) : 0;
            model.total = data.Total_price;
            model.readItems = items;
            var parts = new List<OrderPartModel>();
            foreach (Order_part op in data.Order_part)
            {
                var partItems = new List<OrderItemModel>();
                int? qttTotal = 0;
                float? boxTotal = 0;
                decimal? subTotal = 0;
                foreach (Order_items o in op.Order_items)
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
                        priceBeforeDiscount = o.Discount > 0 ? (o.Price * 100 / (100 + o.Discount)) : o.Price,
                        productId = o.Product_ID,
                        qttBox = product.Quantity_in_carton
                    };
                    partItems.Add(item);
                    qttTotal += o.Quantity;
                    boxTotal += o.Box;
                    subTotal += o.Price;
                }
                var part = new OrderPartModel
                {
                    Order_part_ID = op.Order_part_ID,
                    dateShow = op.Date_reveice_invoice.ToString().Substring(0, 10),
                    items = partItems,
                    vat = op.VAT,
                    total = op.Total_price,
                    qttTotal = qttTotal,
                    boxTotal = boxTotal,
                    subTotal = subTotal,
                    discount = data.Order_discount,
                    discountMoney = data.Order_discount > 0 ? (subTotal * (100 - data.Order_discount) / 100) : 0,
                    afterDiscountMoney = data.Order_discount > 0 ? (subTotal - (subTotal * (100 - data.Order_discount) / 100)) : subTotal,
                    vatMoney = op.VAT > 0 ? ((subTotal - (subTotal * (100 - data.Order_discount) / 100)) * (100 + data.VAT) / 100) : 0
                };
                parts.Add(part);
            }
            model.readPart = parts;
            return View(model);
        }

        public ActionResult CheckOut(OrderTotalModel model)
        {
            try
            {
                if (!String.IsNullOrEmpty(model.orderId) && model.customerId > 0
                    && !String.IsNullOrEmpty(model.deliveryAddress) && !String.IsNullOrEmpty(model.invoiceAddress)
                    && !String.IsNullOrEmpty(model.taxCode) && model.rate > 0 && model.items != null && model.items.Count > 0)
                {
                    var result = 0;
                    var session = (UserSession)Session[CommonConstants.USER_SESSION];
                    var orderDAO = new OrderTotalDAO();
                    var orderPartDAO = new OrderPartDAO();
                    var orderStatusDAO = new OrderDetailStatusDAO();
                    var orderItemDAO = new OrderItemDAO();
                    if (orderDAO.checkOrder(model.orderId) == 0)
                    {
                        orderDAO.createOrder(new Order_total
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
                            Status_ID = 3
                        });
                        result++;
                    }
                    else
                    {
                        orderDAO.updateOrder(new Order_total
                        {
                            Order_ID = model.orderId,
                            Address_delivery = model.deliveryAddress,
                            Address_invoice_issuance = model.invoiceAddress,
                            Customer_ID = model.customerId,
                            Rate = model.rate,
                            User_ID = session.user_id,
                            Sub_total = model.subTotal,
                            VAT = model.vat,
                            Total_price = model.total,
                            Order_discount = model.discount,
                            Status_ID = 3
                        });
                        orderDAO.deleteContent(model.orderId);
                        result++;
                    }
                    if (result > 0)
                    {
                        orderStatusDAO.createOrderStatus(new Order_detail_status
                        {
                            Order_ID = model.orderId,
                            Status_ID = 3,
                            Date_change = DateTime.Now,
                            User_ID = session.user_id
                        });
                        foreach (Order_items o in model.items)
                        {
                            o.Order_ID = model.orderId;
                            orderItemDAO.createOrderItem(o);
                        }
                        if (model.deliveryQtt > 1)
                        {
                            foreach (Order_part o in model.part)
                            {
                                o.Order_ID = model.orderId;
                                o.Date_created = DateTime.Now;
                                o.Status_ID = 3;
                                o.Customer_ID = model.customerId;
                                if (orderPartDAO.createOrderPart(o) != null)
                                {
                                    orderStatusDAO.createOrderStatus(new Order_detail_status
                                    {
                                        Order_ID = model.orderId,
                                        Order_part_ID = o.Order_part_ID,
                                        Status_ID = 3,
                                        Date_change = DateTime.Now,
                                        User_ID = session.user_id
                                    });
                                }
                            }
                        }
                        else
                        {
                            orderPartDAO.createOrderPart(new Order_part
                            {
                                Order_ID = model.orderId,
                                Part_ID = 1,
                                Order_part_ID = model.orderId + "-1",
                                Customer_ID = model.customerId,
                                Date_created = DateTime.Now,
                                VAT = model.vat,
                                Status_ID = 3,
                                Date_reveice_invoice = DateTime.Now,
                                Total_price = model.total
                            });
                            foreach (Order_items o in model.items)
                            {
                                o.Order_ID = model.orderId;
                                o.Order_part_ID = model.orderId + "-1";
                                orderItemDAO.createOrderItem(o);
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

