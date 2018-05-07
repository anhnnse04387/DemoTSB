﻿using Models.DAO;
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
    public class StockController : QuanLyBaseController
    {
        // GET: QuanLy/Stock
        public ActionResult Index()
        {
            var ddlOrder = new List<SelectListItem>();
            var daoOrder = new OrderTotalDAO();
            var lstOrder = daoOrder.getLstOrder();
            lstOrder.ForEach(x =>
            {
                ddlOrder.Add(new SelectListItem { Text = x.Order_ID, Value = x.Order_ID });
            });
            var ddlPI = new List<SelectListItem>();
            var model = new StockModel();
            var dao = new PIDAO();
            var lstPI = dao.getLstPI();
            lstPI.ForEach(x =>
            {
                ddlPI.Add(new SelectListItem { Text = x.Purchase_invoice_no, Value = x.Purchase_invoice_ID.ToString() });
            });
            model.lstLo = ddlOrder;
            model.lstPi = ddlPI;
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            try
            {
                var model = new StockModel();
                var orderDAO = new OrderTotalDAO();
                var supplierDAO = new SupplierDAO();
                var piDAO = new PIDAO();
                var productDAO = new ProductDAO();
                var dao = new StockInDAO();
                var customerDAO = new CustomerDAO();
                var data = dao.getStockInById(id);
                var ddlOrder = new List<SelectListItem>();
                var lstOrder = orderDAO.getLstOrder();
                lstOrder.ForEach(x =>
                {
                    ddlOrder.Add(new SelectListItem { Text = x.Order_ID, Value = x.Order_ID });
                });
                var ddlPI = new List<SelectListItem>();
                var lstPI = piDAO.getLstPI();
                lstPI.ForEach(x =>
                {
                    ddlPI.Add(new SelectListItem { Text = x.Purchase_invoice_no, Value = x.Purchase_invoice_ID.ToString() });
                });
                model.lstLo = ddlOrder;
                model.lstPi = ddlPI;
                if (!String.IsNullOrEmpty(data.Order_part_ID))
                {
                    var order = orderDAO.getOrder(data.Order_part_ID.Substring(0, data.Order_part_ID.IndexOf("-")));
                    var customer = customerDAO.getCustomerById(order.Customer_ID);
                    var list = new List<StockItemModel>();
                    model.address = order.Address_delivery;
                    model.customer = customer.Customer_name;
                    model.dateImportedToShow = data.Date_import.Value.ToString("dd/MM/yyyy");
                    model.dateRequested = order.Date_created.ToString("dd/MM/yyyy");
                    model.no = data.Order_part_ID;
                    model.tel = customer.Phone;
                    foreach (Detail_stock_in i in data.Detail_stock_in)
                    {
                        var product = productDAO.getProductById(i.Product_ID);
                        var item = new StockItemModel
                        {
                            productName = product.Product_name,
                            Quantities = order.Order_items.Where(x => x.Product_ID == i.Product_ID).SingleOrDefault().Quantity,
                            orderQtt = i.Quantities,
                            Note = i.Note
                        };
                        list.Add(item);
                    }
                    model.items = list;
                }
                else
                {
                    var pi = piDAO.getPI(data.Purchase_invoice_ID);
                    var supplier = supplierDAO.getSupplierById(pi.Supplier_ID);
                    var list = new List<StockItemModel>();
                    model.address = supplier.Supplier_address;
                    model.customer = supplier.Supplier_name;
                    model.dateImportedToShow = data.Date_import.Value.ToString("dd/MM/yyyy");
                    model.dateRequested = pi.Date_requested.Value.ToString("dd/MM/yyyy");
                    model.no = data.Purchase_invoice_ID + "";
                    model.tel = supplier.Phone;
                    foreach (Detail_stock_in i in data.Detail_stock_in)
                    {
                        var product = productDAO.getProductById(i.Product_ID);
                        var item = new StockItemModel
                        {
                            productName = product.Product_name,
                            Quantities = pi.Purchase_invoice_Items.Where(x => x.Product_ID == i.Product_ID).SingleOrDefault().Quantity,
                            orderQtt = i.Quantities,
                            Note = i.Note
                        };
                        list.Add(item);
                    }
                    model.items = list;
                }
                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ChooseNo(String no, bool status)
        {
            try
            {
                var model = new StockModel();
                var daoSup = new SupplierDAO();
                var daoCus = new CustomerDAO();
                var daoItem = new OrderItemDAO();
                var daoPI = new PIDAO();
                var daoOrder = new OrderTotalDAO();
                var daoProduct = new ProductDAO();
                var lstItem = new List<StockItemModel>();
                if (status)
                {
                    int id = int.Parse(no);
                    var pi = daoPI.getPI(id);
                    var supplier = daoSup.getSupplierById(pi.Supplier_ID);
                    model.address = supplier.Supplier_address;
                    model.customer = supplier.Supplier_name;
                    model.dateRequested = pi.Date_requested.ToString().Substring(0, 10);
                    model.tel = supplier.Phone;
                    foreach (Purchase_invoice_Items p in pi.Purchase_invoice_Items)
                    {
                        var product = daoProduct.getProductById(p.Product_ID);
                        var item = new StockItemModel
                        {
                            Product_ID = p.Product_ID,
                            productName = product.Product_name,
                            Note = p.NOTE,
                            orderQtt = p.Quantity
                        };
                        lstItem.Add(item);
                    }
                    model.items = lstItem;
                }
                else
                {
                    var order = daoOrder.getOrder(no);
                    var customer = daoCus.getCustomerById(order.Customer_ID);
                    model.address = customer.Delivery_address;
                    model.customer = customer.Customer_name;
                    model.dateRequested = order.Date_created.ToString().Substring(0, 10);
                    model.tel = customer.Phone;
                    foreach (Order_items i in daoItem.getReturnOrderItem(no))
                    {
                        var product = daoProduct.getProductById(i.Product_ID);
                        var item = new StockItemModel
                        {
                            Product_ID = i.Product_ID,
                            productName = product.Product_name,
                            Note = i.Note,
                            orderQtt = i.Quantity
                        };
                        lstItem.Add(item);
                    }
                    model.items = lstItem;
                }
                return PartialView("_chooseNoPartial", model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult CheckOut(StockModel model)
        {
            try
            {
                if (!String.IsNullOrEmpty(model.no) && !String.IsNullOrEmpty(model.dateImported.ToString()) &&
                    model.items != null)
                {
                    var session = (UserSession)Session[CommonConstants.USER_SESSION];
                    var dao = new StockInDAO();
                    var stockIn = new Stock_in();
                    var lstItem = new List<Detail_stock_in>();
                    if (model.status)
                    {
                        stockIn.Date_import = model.dateImported;
                        stockIn.Purchase_invoice_ID = int.Parse(model.no);
                        stockIn.User_ID = session.user_id;
                        foreach (StockItemModel i in model.items)
                        {
                            var item = new Detail_stock_in
                            {
                                Product_ID = i.Product_ID,
                                Note = i.Note,
                                Quantities = i.Quantities
                            };
                            lstItem.Add(item);
                        }
                        stockIn.Detail_stock_in = lstItem;
                    }
                    else
                    {
                        stockIn.Date_import = model.dateImported;
                        stockIn.Order_part_ID = model.no;
                        stockIn.User_ID = session.user_id;
                        foreach (StockItemModel i in model.items)
                        {
                            var item = new Detail_stock_in
                            {
                                Product_ID = i.Product_ID,
                                Note = i.Note,
                                Quantities = i.Quantities
                            };
                            lstItem.Add(item);
                        }
                        stockIn.Detail_stock_in = lstItem;
                    }
                    dao.createStockIn(stockIn);
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
    }
}