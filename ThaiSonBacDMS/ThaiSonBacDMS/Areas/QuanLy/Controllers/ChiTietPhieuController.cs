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
    public class ChiTietPhieuController : QuanLyBaseController
    {
        // GET: QuanLy/ChiTietPhieu
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

        public ActionResult OnetimeDelivery(String orderId)
        {
            var dao = new OrderTotalDAO();
            var customerDAO = new CustomerDAO();
            var data = dao.getOrder(orderId);
            var statusDAO = new StatusDAO();
            var model = new OrderTotalModel();
            var productDAO = new ProductDAO();
            var userDAO = new UserDAO();
            var count = 0;
            var lineStatus = "calc((40% -140px) * 0.5)";
            var dict = new Dictionary<int, StatusDetailModel>();
            var statusDetail = data.Order_detail_status.Where(x => (x.Status_ID == 1 || x.Status_ID == 2) && x.Order_part_ID == null).FirstOrDefault();
            dict.Add(1, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change });
            statusDetail = data.Order_detail_status.Where(x => x.Status_ID == 3 && x.Order_part_ID == null).FirstOrDefault();
            if (statusDetail != null)
            {
                dict.Add(3, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change, classAttr = "stepper__step-icon--finish" });
                lineStatus = "calc((90% -140px) * 0.5)";
                count = 1;
            }
            else
            {
                dict.Add(3, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
            }
            statusDetail = data.Order_detail_status.Where(x => (x.Status_ID == 5 || x.Status_ID == 10) && x.Order_part_ID == null).FirstOrDefault();
            if (count == 1)
            {
                if (statusDetail != null)
                {
                    dict.Add(5, new StatusDetailModel
                    {
                        name = statusDetail.Status_ID == 10 ? userDAO.getByID(statusDetail.User_ID).User_name + "\n( Chưa xuất phiếu )" : userDAO.getByID(statusDetail.User_ID).User_name,
                        date = statusDetail.Date_change,
                        classAttr = "stepper__step-icon--finish"
                    });
                    lineStatus = "calc((130% -140px) * 0.5)";
                    count = 2;
                }
                else
                {
                    dict.Add(5, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                }
            }
            else
            {
                dict.Add(5, new StatusDetailModel { });
            }
            statusDetail = data.Order_detail_status.Where(x => (x.Status_ID == 6 || x.Status_ID == 11) && x.Order_part_ID == null).FirstOrDefault();
            if (count == 2)
            {
                if (statusDetail != null)
                {
                    dict.Add(6, new StatusDetailModel
                    {
                        name = statusDetail.Status_ID == 11 ? userDAO.getByID(statusDetail.User_ID).User_name + "\n( Chưa xuất phiếu )" : userDAO.getByID(statusDetail.User_ID).User_name,
                        date = statusDetail.Date_change,
                        classAttr = "stepper__step-icon--finish"
                    });
                    lineStatus = "calc((170% -140px) * 0.5)";
                    count = 3;
                }
                else
                {
                    dict.Add(6, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                }
            }
            else
            {
                dict.Add(6, new StatusDetailModel { });
            }
            statusDetail = data.Order_detail_status.Where(x => x.Status_ID == 7 && x.Order_part_ID == null).FirstOrDefault();
            if (count == 3)
            {
                if (statusDetail != null)
                {
                    dict.Add(7, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change, classAttr = "stepper__step-icon--finish" });
                }
                else
                {
                    dict.Add(7, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                }
            }
            else
            {
                dict.Add(7, new StatusDetailModel { });
            }
            model.lineStatus = lineStatus;
            model.dict = dict;
            model.orderId = orderId;
            model.invoiceAddress = "Công ty TNHH Thái Sơn Bắc";
            model.deliveryAddress = data.Address_delivery;
            model.invoiceNumber = data.Order_part.FirstOrDefault().Invoice_number;
            model.status = statusDAO.getStatus(data.Status_ID);
            var customer = customerDAO.getCustomerById(data.Customer_ID);
            model.customerName = customer.Customer_name;
            model.taxCode = data.Tax_code;
            model.qttTotal = 0;
            model.boxTotal = 0;
            var items = new List<OrderItemModel>();
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

        public ActionResult DetailStatus(String orderId)
        {
            var model = new OrderTotalModel();
            var dict = new Dictionary<int, StatusDetailModel>();
            if (orderId.Contains("-"))
            {
                var detailStatusDAO = new OrderDetailStatusDAO();
                var dao = new OrderPartDAO();
                var customerDAO = new CustomerDAO();
                var data = dao.getOrderPart(orderId);
                var statusDAO = new StatusDAO();
                var userDAO = new UserDAO();
                var count = 0;
                model.orderId = orderId;
                model.deliveryAddress = data.Order_total.Address_delivery;
                var customer = customerDAO.getCustomerById(data.Customer_ID);
                model.customerName = customer.Customer_name;
                model.tel = customer.Phone;
                var lineStatus = "calc((40% -140px) * 0.5)";
                var lstStatus = detailStatusDAO.getStatus(orderId);
                var statusDetail = lstStatus.Where(x => x.Status_ID == 1 || x.Status_ID == 2).FirstOrDefault();
                dict.Add(1, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change });
                statusDetail = lstStatus.Where(x => x.Status_ID == 3).FirstOrDefault();
                if (statusDetail != null)
                {
                    dict.Add(3, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change, classAttr = "stepper__step-icon--finish" });
                    lineStatus = "calc((90% -140px) * 0.5)";
                    count = 1;
                }
                else
                {
                    dict.Add(3, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                }
                statusDetail = lstStatus.Where(x => x.Status_ID == 5 || x.Status_ID == 10).FirstOrDefault();
                if (count == 1)
                {
                    if (statusDetail != null)
                    {
                        dict.Add(5, new StatusDetailModel
                        {
                            name = statusDetail.Status_ID == 10 ? userDAO.getByID(statusDetail.User_ID).User_name + "\n( Chưa xuất phiếu )" : userDAO.getByID(statusDetail.User_ID).User_name,
                            date = statusDetail.Date_change,
                            classAttr = "stepper__step-icon--finish"
                        });
                        lineStatus = "calc((130% -140px) * 0.5)";
                        count = 2;
                    }
                    else
                    {
                        dict.Add(5, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                    }
                }
                else
                {
                    dict.Add(5, new StatusDetailModel { });
                }
                statusDetail = lstStatus.Where(x => x.Status_ID == 6 || x.Status_ID == 11).FirstOrDefault();
                if (count == 2)
                {
                    if (statusDetail != null)
                    {
                        dict.Add(6, new StatusDetailModel
                        {
                            name = statusDetail.Status_ID == 11 ? userDAO.getByID(statusDetail.User_ID).User_name + "\n( Chưa xuất phiếu )" : userDAO.getByID(statusDetail.User_ID).User_name,
                            date = statusDetail.Date_change,
                            classAttr = "stepper__step-icon--finish"
                        });
                        lineStatus = "calc((170% -140px) * 0.5)";
                        count = 3;
                    }
                    else
                    {
                        dict.Add(6, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                    }
                }
                else
                {
                    dict.Add(6, new StatusDetailModel { });
                }
                statusDetail = lstStatus.Where(x => x.Status_ID == 7).FirstOrDefault();
                if (count == 3)
                {
                    if (statusDetail != null)
                    {
                        dict.Add(7, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change, classAttr = "stepper__step-icon--finish" });
                    }
                    else
                    {
                        dict.Add(7, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                    }
                }
                else
                {
                    dict.Add(7, new StatusDetailModel { });
                }
                model.lineStatus = lineStatus;
                if (!String.IsNullOrEmpty(data.Driver_ID))
                {
                    model.driverName = userDAO.getByID(int.Parse(data.Driver_ID)).User_name;
                }
                model.dateExport = data.Request_stockout_date;
                if (data.Date_completed != null)
                {
                    model.dateComplete = data.Date_completed.Value.ToString("MM/dd/yyyy");
                }
                model.dict = dict;
            }
            else
            {
                var dao = new OrderTotalDAO();
                var customerDAO = new CustomerDAO();
                var data = dao.getOrder(orderId);
                var statusDAO = new StatusDAO();
                var userDAO = new UserDAO();
                var count = 0;
                model.orderId = orderId;
                model.deliveryAddress = data.Address_delivery;
                var customer = customerDAO.getCustomerById(data.Customer_ID);
                model.customerName = customer.Customer_name;
                model.tel = customer.Phone;
                var lineStatus = "calc((40% -140px) * 0.5)";
                var statusDetail = data.Order_detail_status.Where(x => (x.Status_ID == 1 || x.Status_ID == 2) && x.Order_part_ID == null).FirstOrDefault();
                dict.Add(1, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change });
                statusDetail = data.Order_detail_status.Where(x => x.Status_ID == 3 && x.Order_part_ID == null).FirstOrDefault();
                if (statusDetail != null)
                {
                    dict.Add(3, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change, classAttr = "stepper__step-icon--finish" });
                    lineStatus = "calc((90% -140px) * 0.5)";
                    count = 1;
                }
                else
                {
                    dict.Add(3, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                }
                statusDetail = data.Order_detail_status.Where(x => (x.Status_ID == 5 || x.Status_ID == 10) && x.Order_part_ID == null).FirstOrDefault();
                if (count == 1)
                {
                    if (statusDetail != null)
                    {
                        dict.Add(5, new StatusDetailModel
                        {
                            name = statusDetail.Status_ID == 10 ? userDAO.getByID(statusDetail.User_ID).User_name + "\n( Chưa xuất phiếu )" : userDAO.getByID(statusDetail.User_ID).User_name,
                            date = statusDetail.Date_change,
                            classAttr = "stepper__step-icon--finish"
                        });
                        lineStatus = "calc((130% -140px) * 0.5)";
                        count = 2;
                    }
                    else
                    {
                        dict.Add(5, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                    }
                }
                else
                {
                    dict.Add(5, new StatusDetailModel { });
                }
                statusDetail = data.Order_detail_status.Where(x => (x.Status_ID == 6 || x.Status_ID == 11) && x.Order_part_ID == null).FirstOrDefault();
                if (count == 2)
                {
                    if (statusDetail != null)
                    {
                        dict.Add(6, new StatusDetailModel
                        {
                            name = statusDetail.Status_ID == 11 ? userDAO.getByID(statusDetail.User_ID).User_name + "\n( Chưa xuất phiếu )" : userDAO.getByID(statusDetail.User_ID).User_name,
                            date = statusDetail.Date_change,
                            classAttr = "stepper__step-icon--finish"
                        });
                        lineStatus = "calc((170% -140px) * 0.5)";
                        count = 3;
                    }
                    else
                    {
                        dict.Add(6, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                    }
                }
                else
                {
                    dict.Add(6, new StatusDetailModel { });
                }
                statusDetail = data.Order_detail_status.Where(x => x.Status_ID == 7 && x.Order_part_ID == null).FirstOrDefault();
                if (count == 3)
                {
                    if (statusDetail != null)
                    {
                        dict.Add(7, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change, classAttr = "stepper__step-icon--finish" });
                    }
                    else
                    {
                        dict.Add(7, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                    }
                }
                else
                {
                    dict.Add(7, new StatusDetailModel { });
                }
                model.lineStatus = lineStatus;
                model.driverName = userDAO.getByID(int.Parse(data.Order_part.SingleOrDefault().Driver_ID)).User_name;
                model.dateExport = data.Order_part.SingleOrDefault().Request_stockout_date;
                if (data.Date_completed != null)
                {
                    model.dateComplete = data.Date_completed.Value.ToString("MM/dd/yyyy");
                }
                model.dict = dict;
            }
            return View(model);
        }

        public ActionResult MultipleDelivery(String orderId)
        {
            var detailStatusDAO = new OrderDetailStatusDAO();
            var userDAO = new UserDAO();
            var dao = new OrderTotalDAO();
            var customerDAO = new CustomerDAO();
            var data = dao.getOrder(orderId);
            var statusDAO = new StatusDAO();
            var model = new OrderTotalModel();
            var productDAO = new ProductDAO();
            model.orderId = orderId;
            model.invoiceAddress = "Công ty TNHH Thái Sơn Bắc";
            var customer = customerDAO.getCustomerById(data.Customer_ID);
            model.taxCode = data.Tax_code;
            model.deliveryAddress = data.Address_delivery;
            model.customerName = customer.Customer_name;
            model.invoiceNumber = data.Order_part.FirstOrDefault().Invoice_number;
            var items = new List<OrderItemModel>();
            var leftItems = new List<OrderItemModel>();
            model.qttTotal = 0;
            model.boxTotal = 0;
            model.leftQttTotal = 0;
            model.leftBoxTotal = 0;
            model.leftSubTotal = 0;
            model.leftVatMoney = 0;
            var lstItemToGet = new List<Order_items>();
            var lstItemToSet = data.Order_items.Where(x => x.Order_part_ID == null).ToList();
            foreach (Order_items o in data.Order_items.Where(x => x.Order_part_ID == null).ToList())
            {
                foreach (Order_items i in data.Order_items.Where(x => x.Order_part_ID != null).ToList())
                {
                    if (o.Product_ID == i.Product_ID && i.Quantity == o.Quantity)
                    {
                        lstItemToSet.Remove(o);
                    }
                    if (o.Product_ID == i.Product_ID && i.Quantity < o.Quantity)
                    {
                        o.Quantity = o.Quantity - i.Quantity;
                        lstItemToGet.Add(o);
                    }
                }
            }
            foreach (Order_items i in lstItemToSet)
            {
                lstItemToGet.Add(i);
            }
            foreach (Order_items i in lstItemToGet)
            {
                var product = productDAO.getProductById(i.Product_ID);
                var item = new OrderItemModel
                {
                    code = product.Product_code,
                    param = product.Product_parameters,
                    Box = i.Box,
                    Discount = i.Discount,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    per = product.Price_before_VAT_VND * (100 + product.VAT) / 100,
                    priceBeforeDiscount = i.Discount > 0 ? (i.Price * 100 / (100 + i.Discount)) : i.Price,
                    productId = i.Product_ID,
                    qttBox = product.Quantity_in_carton,
                    qttInven = product.Quantities_in_inventory
                };
                leftItems.Add(item);
                model.leftVatMoney += product.Price_before_VAT_VND * product.VAT / 100;
                model.leftQttTotal += i.Quantity;
                model.leftBoxTotal += i.Box;
                model.leftSubTotal += i.Price;
            }
            model.leftDiscountMoney = data.Order_discount > 0 ? (model.leftSubTotal * (100 - data.Order_discount) / 100) : 0;
            model.leftAfterDiscountMoney = data.Order_discount > 0 ? model.leftSubTotal - model.leftDiscountMoney : model.leftSubTotal;
            model.leftTotal = data.VAT > 0 ? model.leftAfterDiscountMoney + model.leftVatMoney : model.leftAfterDiscountMoney;
            model.leftItems = leftItems;
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
            model.vat = data.VAT;
            model.subTotal = data.Sub_total;
            model.discountMoney = data.Order_discount > 0 ? (data.Sub_total * (100 - data.Order_discount) / 100) : 0;
            model.afterDiscountMoney = data.Order_discount > 0 ? data.Sub_total - model.discountMoney : data.Sub_total;
            model.total = data.Total_price;
            model.readItems = items;
            var parts = new List<OrderPartModel>();
            foreach (Order_part op in data.Order_part)
            {
                var count = 0;
                var lineStatus = "calc((40% -140px) * 0.5)";
                var dict = new Dictionary<int, StatusDetailModel>();
                var lstStatus = detailStatusDAO.getStatus(op.Order_part_ID);
                var statusDetail = lstStatus.Where(x => x.Status_ID == 1 || x.Status_ID == 2).FirstOrDefault();
                dict.Add(1, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change });
                statusDetail = lstStatus.Where(x => x.Status_ID == 3).FirstOrDefault();
                if (statusDetail != null)
                {
                    dict.Add(3, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change, classAttr = "stepper__step-icon--finish" });
                    lineStatus = "calc((90% -140px) * 0.5)";
                    count = 1;
                }
                else
                {
                    dict.Add(3, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                }
                statusDetail = lstStatus.Where(x => x.Status_ID == 5 || x.Status_ID == 10).FirstOrDefault();
                if (count == 1)
                {
                    if (statusDetail != null)
                    {
                        dict.Add(5, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change, classAttr = "stepper__step-icon--finish" });
                        lineStatus = "calc((130% -140px) * 0.5)";
                        count = 2;
                    }
                    else
                    {
                        dict.Add(5, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                    }
                }
                else
                {
                    dict.Add(5, new StatusDetailModel { });
                }
                statusDetail = lstStatus.Where(x => x.Status_ID == 6 || x.Status_ID == 11).FirstOrDefault();
                if (count == 2)
                {
                    if (statusDetail != null)
                    {
                        dict.Add(6, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change, classAttr = "stepper__step-icon--finish" });
                        lineStatus = "calc((170% -140px) * 0.5)";
                        count = 3;
                    }
                    else
                    {
                        dict.Add(6, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                    }
                }
                else
                {
                    dict.Add(6, new StatusDetailModel { });
                }
                statusDetail = lstStatus.Where(x => x.Status_ID == 7).FirstOrDefault();
                if (count == 3)
                {
                    if (statusDetail != null)
                    {
                        dict.Add(7, new StatusDetailModel { name = userDAO.getByID(statusDetail.User_ID).User_name, date = statusDetail.Date_change, classAttr = "stepper__step-icon--finish" });
                    }
                    else
                    {
                        dict.Add(7, new StatusDetailModel { classAttr = "stepper__step-icon--pending" });
                    }
                }
                else
                {
                    dict.Add(7, new StatusDetailModel { });
                }
                var partItems = new List<OrderItemModel>();
                int? qttTotal = 0;
                float? boxTotal = 0;
                decimal? subTotal = 0;
                foreach (Order_items o in data.Order_items.Where(x => x.Order_part_ID != null && x.Order_part_ID.Equals(op.Order_part_ID)).ToList())
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
                    dateShow = op.Date_created.ToString("dd/MM/yyyy"),
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
                part.lineStatus = lineStatus;
                part.dict = dict;
                parts.Add(part);
            }
            model.dayCreated = data.Date_created.ToString("dd/MM/yyyy");
            model.readPart = parts;
            return View(model);
        }

        public JsonResult CheckOut(String orderId)
        {
            try
            {
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var dao = new OrderTotalDAO();
                dao.checkOut(orderId, session.user_id, dao.getOrder(orderId).Order_part.Count);
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