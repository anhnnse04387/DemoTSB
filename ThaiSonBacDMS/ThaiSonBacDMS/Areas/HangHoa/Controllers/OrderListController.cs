﻿using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.HangHoa.Models;

namespace ThaiSonBacDMS.Areas.HangHoa.Controllers
{
    public class OrderListController : HangHoaBaseController
    {
        // GET: HangHoa/OrderList
        public ActionResult Index(OrderListModel model)
        {
            try
            {
                List<Order_part> lstOrder = new OrderPartDAO().getAllOrderPartByStatus(3);
                lstOrder.AddRange(new OrderPartDAO().getAllOrderPartByStatus(4));
                model.listOrderPending = lstOrder.Select((x, index) => new OrderListPending
                {
                    indexOf = index + 1,
                    orderID = x.Order_part_ID,
                    customerName = new CustomerDAO().getCustomerById(x.Customer_ID).Customer_name,
                    salesUserName = new UserDAO().getByID(x.Sales_user_ID).User_name,
                    invoiceNumber = x.Invoice_number,
                    statusName = new StatusDAO().getStatus((byte)x.Status_ID),
                    takeInvoice = x.Date_take_invoice == null ? false : true,
                    takeBallot = x.Date_take_ballot == null ? false : true,
                    dateExport = (DateTime)x.Request_stockout_date,
                    note = x.Note,
                }).ToList();
                if (!string.IsNullOrEmpty(model.orderID))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.orderID.Contains(model.orderID)).ToList();
                }
                if (!string.IsNullOrEmpty(model.customerName))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.customerName.Contains(model.customerName)).ToList();
                }
                if (!string.IsNullOrEmpty(model.invoice_number))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.invoiceNumber.Contains(model.invoice_number)).ToList();
                }
                if (!string.IsNullOrEmpty(model.statusName))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.statusName.Contains(model.statusName)).ToList();
                }
                if (!string.IsNullOrEmpty(model.fromDate))
                {
                    DateTime fromDate = DateTime.ParseExact(model.fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    model.listOrderPending = model.listOrderPending.Where(x => x.dateExport >= fromDate).ToList();
                }
                if (!string.IsNullOrEmpty(model.toDate))
                {
                    DateTime toDate = DateTime.ParseExact(model.fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    model.listOrderPending = model.listOrderPending.Where(x => x.dateExport <= toDate).ToList();
                }
                model.listOrderPending = model.listOrderPending.OrderByDescending(x => x.dateExport).ToList();
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
            
            return View(model);
        }

        public ActionResult Completed(OrderListModel model)
        {
            try
            {
                //setup data
                model.listMethod = new DeliveryMethodDAO().getLstDelivery().Select(x => new SelectListItem {
                    Text = x.Method_name,
                    Value = x.Method_name
                }).ToList();
                model.listDriver = new UserDAO().getLstDriver().Select(x => new SelectListItem
                {
                    Text = x.User_name,
                    Value = x.User_name
                }).ToList();
                model.listShipper = new UserDAO().getLstShipper().Select(x => new SelectListItem
                {
                    Text = x.User_name,
                    Value = x.User_name
                }).ToList();
                //prepare data list
                List<Order_part> lstOrder = new OrderPartDAO().getAllOrderPartByStatus(7);
                model.listOrderPending = lstOrder.Select((x, index) => new OrderListPending
                {
                    indexOf = index + 1,
                    orderID = x.Order_part_ID,
                    customerName = new CustomerDAO().getCustomerById(x.Customer_ID).Customer_name,
                    invoiceNumber = x.Invoice_number,
                    reveiceBallot = x.Date_reveice_ballot == null ? false : true,
                    reveiceInvoice = x.Date_reveice_invoice == null ? false : true,
                    takeInvoice = x.Date_take_invoice == null ? false : true,
                    takeBallot = x.Date_take_ballot == null ? false : true,
                    statusName = new StatusDAO().getStatus((byte)x.Status_ID),
                    dateExport = (DateTime)x.Request_stockout_date,
                    dateCompleted = (DateTime)x.Date_completed,
                    note = x.Note,
                    devilerMethod = new DeliveryMethodDAO().getByID((byte)x.DeliverMethod_ID).Method_name,
                    shipperName = new UserDAO().getByID(x.Shiper_ID).User_name,
                    driverName = new UserDAO().getByID(int.Parse(x.Driver_ID)).User_name,

                }).ToList();
                //search data
                if (!string.IsNullOrEmpty(model.orderID))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.orderID.Contains(model.orderID)).ToList();
                }
                if (!string.IsNullOrEmpty(model.customerName))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.customerName.Contains(model.customerName)).ToList();
                }
                if (!string.IsNullOrEmpty(model.invoice_number))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.invoiceNumber.Contains(model.invoice_number)).ToList();
                }
                if (!string.IsNullOrEmpty(model.statusName))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.statusName.Contains(model.statusName)).ToList();
                }
                if (!string.IsNullOrEmpty(model.fromDate))
                {
                    DateTime fromDate = DateTime.ParseExact(model.fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    model.listOrderPending = model.listOrderPending.Where(x => x.dateExport >= fromDate).ToList();
                }
                if (!string.IsNullOrEmpty(model.toDate))
                {
                    DateTime toDate = DateTime.ParseExact(model.fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    model.listOrderPending = model.listOrderPending.Where(x => x.dateExport <= toDate).ToList();
                }
                if (!string.IsNullOrEmpty(model.fromCompletedDate))
                {
                    DateTime fromDate = DateTime.ParseExact(model.fromCompletedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    model.listOrderPending = model.listOrderPending.Where(x => x.dateExport >= fromDate).ToList();
                }
                if (!string.IsNullOrEmpty(model.toCompletedDate))
                {
                    DateTime toDate = DateTime.ParseExact(model.toCompletedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    model.listOrderPending = model.listOrderPending.Where(x => x.dateExport <= toDate).ToList();
                }
                if (!string.IsNullOrEmpty(model.deliverMethod))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.devilerMethod.Contains(model.deliverMethod)).ToList();
                }
                if (!string.IsNullOrEmpty(model.deliverMethod))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.devilerMethod.Contains(model.deliverMethod)).ToList();
                }
                if (!string.IsNullOrEmpty(model.driverName))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.driverName.Contains(model.driverName)).ToList();
                }
                if (!string.IsNullOrEmpty(model.shipperName))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.shipperName.Contains(model.shipperName)).ToList();
                }
                model.listOrderPending = model.listOrderPending.OrderByDescending(x => x.dateExport).ToList();
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Completed");
            }
            return View(model);
        }

        public ActionResult Exchange()
        {
            return View();
        }
    }
}