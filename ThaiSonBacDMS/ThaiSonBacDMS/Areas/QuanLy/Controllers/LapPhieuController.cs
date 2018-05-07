using Models.DAO;
using Models.DAO_Model;
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
    public class LapPhieuController : QuanLyBaseController
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
                    var historyDAO = new EditHistoryDAO();
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
                            Tax_code = model.taxCode,
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
                        var lstCompare = model.items.Where(x => x.Order_part_ID == null).ToList();
                        foreach (Order_items o in lstCompare)
                        {
                            foreach (Order_items i in orderDAO.getOrder(model.orderId).Order_items)
                            {
                                if (i.Product_ID == o.Product_ID)
                                {
                                    if (i.Quantity != o.Quantity)
                                    {
                                        historyDAO.createHistory(new Edit_history
                                        {
                                            Date_change = DateTime.Now,
                                            Order_ID = model.orderId,
                                            Product_ID = i.Product_ID,
                                            Quantity_change = o.Quantity - i.Quantity,
                                            User_ID = session.user_id,
                                            Edit_code = (byte)(historyDAO.getEditCode(i.Product_ID) + 1)
                                        });
                                    }
                                }
                            }
                        }
                        foreach (Order_items i in lstCompare)
                        {
                            if (orderDAO.getOrder(model.orderId).Order_items.Where(x => x.Product_ID == i.Product_ID).ToList().Count == 0)
                            {
                                historyDAO.createHistory(new Edit_history
                                {
                                    Date_change = DateTime.Now,
                                    Order_ID = model.orderId,
                                    Product_ID = i.Product_ID,
                                    Quantity_change = i.Quantity,
                                    User_ID = session.user_id,
                                    Edit_code = (byte)(historyDAO.getEditCode(i.Product_ID) + 1)
                                });
                            }
                        }
                        orderDAO.updateOrder(new Order_total
                        {
                            Order_ID = model.orderId,
                            Address_delivery = model.deliveryAddress,
                            Address_invoice_issuance = model.invoiceAddress,
                            Customer_ID = model.customerId,
                            Rate = model.rate,
                            Tax_code = model.taxCode,
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
                        foreach (Order_items o in model.items)
                        {
                            if (o.Order_part_ID == null)
                            {
                                orderItemDAO.createOrderItem(o);
                            }
                        }
                        orderStatusDAO.createOrderStatus(new Order_detail_status
                        {
                            Order_ID = model.orderId,
                            Status_ID = 1,
                            Date_change = DateTime.Now,
                            User_ID = session.user_id
                        });
                        if (model.deliveryQtt > 1)
                        {
                            foreach (Order_part o in model.part)
                            {
                                o.Order_ID = model.orderId;
                                o.Date_created = DateTime.Now;
                                o.Status_ID = 1;
                                o.Customer_ID = model.customerId;
                                orderPartDAO.createOrderPart(o);
                                orderStatusDAO.createOrderStatus(new Order_detail_status
                                {
                                    Order_ID = model.orderId,
                                    Order_part_ID = o.Order_part_ID,
                                    Status_ID = 1,
                                    Date_change = DateTime.Now,
                                    User_ID = session.user_id
                                });
                                foreach (Order_items i in model.items.Where(x => x.Order_part_ID != null && x.Order_part_ID.Equals(o.Order_part_ID)).ToList())
                                {
                                    i.Order_ID = model.orderId;
                                    i.Order_part_ID = o.Order_part_ID;
                                    orderItemDAO.createOrderItem(i);
                                }
                            }
                        }
                        else
                        {
                            orderPartDAO.createOrderPart(new Order_part
                            {
                                Order_ID = model.orderId,
                                Order_part_ID = model.orderId,
                                Customer_ID = model.customerId,
                                Date_created = DateTime.Now,
                                Request_stockout_date = model.dateExport,
                                VAT = model.vat,
                                Status_ID = 1,
                                Total_price = model.total
                            });
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

        [ValidateInput(false)]
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
            model.status = statusDAO.getStatus(data.Status_ID);
            var customer = customerDAO.getCustomerById(data.Customer_ID);
            model.invoiceAddress = data.Address_invoice_issuance;
            model.deliveryAddress = data.Address_delivery;
            model.deliveryQtt = data.Order_part.Count;
            model.customerId = data.Customer_ID;
            model.rate = data.Rate;
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
                        code = product.Product_name,
                        param = product.Product_parameters,
                        Box = o.Box,
                        Discount = o.Discount,
                        Price = o.Price,
                        Quantity = o.Quantity,
                        per = product.Price_before_VAT_VND,
                        priceBeforeDiscount = o.Discount > 0 ? (o.Price * 100 / (100 + o.Discount)) : o.Price,
                        priceBeforeVat = product.Price_before_VAT_VND * product.VAT / 100,
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
            model.vat = data.VAT;
            model.subTotal = data.Sub_total;
            model.discountMoney = data.Order_discount > 0 ? (data.Sub_total * (100 - data.Order_discount) / 100) : 0;
            model.afterDiscountMoney = data.Order_discount > 0 ? data.Sub_total - model.discountMoney : data.Sub_total;
            model.total = data.Total_price;
            model.readItems = items;
            var parts = new List<OrderPartModel>();
            if (data.Order_part.Count > 1)
            {
                foreach (Order_part op in data.Order_part)
                {
                    var partItems = new List<OrderItemModel>();
                    int? qttTotal = 0;
                    float? boxTotal = 0;
                    decimal? subTotal = 0;
                    foreach (Order_items o in data.Order_items.Where(x => x.Order_part_ID != null && x.Order_part_ID.Equals(op.Order_part_ID)).ToList())
                    {
                        var product = productDAO.getProductById(o.Product_ID);
                        var item = new OrderItemModel
                        {
                            code = product.Product_name,
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
                        dateShow = op.Request_stockout_date.Value.ToString("MM/dd/yyyy"),
                        items = partItems,
                        vat = op.VAT,
                        total = op.Total_price,
                        qttTotal = qttTotal,
                        boxTotal = boxTotal,
                        subTotal = subTotal,
                        discount = data.Order_discount,
                        discountMoney = data.Order_discount > 0 ? (subTotal * (100 - data.Order_discount) / 100) : 0,
                        afterDiscountMoney = data.Order_discount > 0 ? (subTotal - (subTotal * (100 - data.Order_discount) / 100)) : subTotal
                    };
                    parts.Add(part);
                }
            }
            else
            {
                model.dateExport = data.Order_part.FirstOrDefault().Request_stockout_date;
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
                    var historyDAO = new EditHistoryDAO();
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
                            Tax_code = model.taxCode,
                            User_ID = session.user_id,
                            Sub_total = model.subTotal,
                            VAT = model.vat,
                            Total_price = model.total,
                            Order_discount = model.discount,
                            Status_ID = 2
                        });
                        result++;
                    }
                    else
                    {
                        foreach (Order_items o in model.items.Where(x => x.Order_part_ID == null).ToList())
                        {
                            foreach (Order_items i in orderDAO.getOrder(model.orderId).Order_items.Where(x => x.Order_part_ID == null).ToList())
                            {
                                if (i.Product_ID == o.Product_ID)
                                {
                                    if (i.Quantity != o.Quantity)
                                    {
                                        historyDAO.createHistory(new Edit_history
                                        {
                                            Date_change = DateTime.Now,
                                            Order_ID = model.orderId,
                                            Product_ID = i.Product_ID,
                                            Quantity_change = o.Quantity - i.Quantity,
                                            User_ID = session.user_id,
                                            Edit_code = (byte)(historyDAO.getEditCode(i.Product_ID) + 1)
                                        });
                                    }
                                }
                                if (orderDAO.getOrder(model.orderId).Order_items.Where(x => x.Order_part_ID == null).ToList().Where(x => x.Product_ID == o.Product_ID).ToList().Count == 0)
                                {
                                    historyDAO.createHistory(new Edit_history
                                    {
                                        Date_change = DateTime.Now,
                                        Order_ID = model.orderId,
                                        Product_ID = o.Product_ID,
                                        Quantity_change = o.Quantity,
                                        User_ID = session.user_id,
                                        Edit_code = (byte)(historyDAO.getEditCode(i.Product_ID) + 1)
                                    });
                                }
                            }
                        }
                        orderDAO.updateOrder(new Order_total
                        {
                            Order_ID = model.orderId,
                            Address_delivery = model.deliveryAddress,
                            Address_invoice_issuance = model.invoiceAddress,
                            Customer_ID = model.customerId,
                            Rate = model.rate,
                            Tax_code = model.taxCode,
                            User_ID = session.user_id,
                            Sub_total = model.subTotal,
                            VAT = model.vat,
                            Total_price = model.total,
                            Order_discount = model.discount,
                            Status_ID = 2
                        });
                        orderDAO.deleteContent(model.orderId);
                        result++;
                    }
                    if (result > 0)
                    {
                        foreach (Order_items o in model.items)
                        {
                            if (o.Order_part_ID == null)
                            {
                                orderItemDAO.createOrderItem(o);
                            }
                        }
                        orderStatusDAO.createOrderStatus(new Order_detail_status
                        {
                            Order_ID = model.orderId,
                            Status_ID = 2,
                            Date_change = DateTime.Now,
                            User_ID = session.user_id
                        });
                        if (model.deliveryQtt > 1)
                        {
                            foreach (Order_part o in model.part)
                            {
                                o.Order_ID = model.orderId;
                                o.Date_created = DateTime.Now;
                                o.Status_ID = 2;
                                o.Customer_ID = model.customerId;
                                orderPartDAO.createOrderPart(o);
                                orderStatusDAO.createOrderStatus(new Order_detail_status
                                {
                                    Order_ID = model.orderId,
                                    Order_part_ID = o.Order_part_ID,
                                    Status_ID = 2,
                                    Date_change = DateTime.Now,
                                    User_ID = session.user_id
                                });
                                foreach (Order_items i in model.items.Where(x => x.Order_part_ID != null && x.Order_part_ID.Equals(o.Order_part_ID)).ToList())
                                {
                                    i.Order_ID = model.orderId;
                                    i.Order_part_ID = o.Order_part_ID;
                                    orderItemDAO.createOrderItem(i);
                                }
                            }
                        }
                        else
                        {
                            orderPartDAO.createOrderPart(new Order_part
                            {
                                Order_ID = model.orderId,
                                Order_part_ID = model.orderId,
                                Customer_ID = model.customerId,
                                Date_created = DateTime.Now,
                                Request_stockout_date = model.dateExport,
                                VAT = model.vat,
                                Status_ID = 2,
                                Total_price = model.total
                            });
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
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckQtt(int productId)
        {
            try
            {
                var dao = new OrderItemDAO();
                return Json(dao.getLstByProductId(productId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Steal(List<CustomOrderItem> lst)
        {
            try
            {
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var dao = new OrderItemDAO();
                dao.stealProduct(lst, session.user_id);
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

