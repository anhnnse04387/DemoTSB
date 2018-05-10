using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.HangHoa.Models;
using ThaiSonBacDMS.Common;
using ThaiSonBacDMS.Controllers;

namespace ThaiSonBacDMS.Areas.HangHoa.Controllers
{
    public class HomeController : HangHoaBaseController
    {
        // GET: HangHoa/Home
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
                    salesUserName = x.Sales_user_ID == null ? "" : new UserDAO().getByID(x.Sales_user_ID).User_name,
                    invoiceNumber = x.Invoice_number == null ? "" : x.Invoice_number,
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
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return View(model);
        }

        public ActionResult Completed(OrderListModel model)
        {
            try
            {
                //setup data
                model.listMethod = new DeliveryMethodDAO().getLstDelivery().Select(x => new SelectListItem
                {
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
                    invoiceNumber = x.Invoice_number == null ? "" : x.Invoice_number,
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
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return View(model);
        }

        public ActionResult ReadyToShip(OrderListModel model)
        {
            try
            {
                //setup data
                model.listMethod = new DeliveryMethodDAO().getLstDelivery().Select(x => new SelectListItem
                {
                    Text = x.Method_name,
                    Value = x.Method_ID.ToString()
                }).ToList();
                model.listDriver = new UserDAO().getLstDriver().Select(x => new SelectListItem
                {
                    Text = x.User_name,
                    Value = x.User_ID.ToString()
                }).ToList();
                model.listShipper = new UserDAO().getLstShipper().Select(x => new SelectListItem
                {
                    Text = x.User_name,
                    Value = x.User_ID.ToString()
                }).ToList();
                //prepare data list
                List<Order_part> lstOrder = new OrderPartDAO().getAllOrderPart().Where(x => x.Status_ID == 5 || x.Status_ID == 10).ToList();
                if (model.statusId > 0)
                {
                    lstOrder = lstOrder.Where(x => x.Status_ID == model.statusId).ToList();
                }
                model.listOrderPending = lstOrder.Select((x, index) => new OrderListPending
                {
                    indexOf = index + 1,
                    orderID = x.Order_part_ID,
                    customerName = new CustomerDAO().getCustomerById(x.Customer_ID).Customer_name,
                    invoiceNumber = x.Invoice_number == null ? "" : x.Invoice_number,
                    spanClass = x.Status_ID == 5 ? "label-primary" : "label-warning",
                    takeInvoice = x.Date_take_invoice == null ? false : true,
                    takeBallot = x.Date_take_ballot == null ? false : true,
                    statusName = x.Status_ID == 5 ? new StatusDAO().getStatus((byte)x.Status_ID) : new StatusDAO().getStatus((byte)x.Status_ID).Substring(0, new StatusDAO().getStatus((byte)x.Status_ID).IndexOf("warning")),
                    dateExport = (DateTime)x.Request_stockout_date,
                    dateCompleted = x.Date_completed,
                    note = x.Note,
                    Driver_ID = x.Driver_ID,
                    Shiper_ID = x.Shiper_ID,
                    DeliverMethod_ID = x.DeliverMethod_ID,

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
                if (model.takeInvoice)
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.takeInvoice == true).ToList();
                }
                if (model.takeBallot)
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.takeBallot == true).ToList();
                }
                model.listOrderPending = model.listOrderPending.OrderByDescending(x => x.dateExport).ToList();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return View(model);
        }

        public ActionResult Delivery(OrderListModel model)
        {
            try
            {
                //setup data
                model.listMethod = new DeliveryMethodDAO().getLstDelivery().Select(x => new SelectListItem
                {
                    Text = x.Method_name,
                    Value = x.Method_ID.ToString()
                }).ToList();
                model.listDriver = new UserDAO().getLstDriver().Select(x => new SelectListItem
                {
                    Text = x.User_name,
                    Value = x.User_ID.ToString()
                }).ToList();
                model.listShipper = new UserDAO().getLstShipper().Select(x => new SelectListItem
                {
                    Text = x.User_name,
                    Value = x.User_ID.ToString()
                }).ToList();
                //prepare data list
                List<Order_part> lstOrder = new OrderPartDAO().getAllOrderPart().Where(x => x.Status_ID == 6 || x.Status_ID == 11).ToList();
                if (model.statusId > 0)
                {
                    lstOrder = lstOrder.Where(x => x.Status_ID == model.statusId).ToList();
                }
                model.listOrderPending = lstOrder.Select((x, index) => new OrderListPending
                {
                    indexOf = index + 1,
                    orderID = x.Order_part_ID,
                    customerName = new CustomerDAO().getCustomerById(x.Customer_ID).Customer_name,
                    invoiceNumber = x.Invoice_number == null ? "" : x.Invoice_number,
                    spanClass = x.Status_ID == 6 ? "label-primary" : "label-warning",
                    reveiceBallot = x.Date_reveice_ballot == null ? false : true,
                    reveiceInvoice = x.Date_reveice_invoice == null ? false : true,
                    takeInvoice = x.Date_take_invoice == null ? false : true,
                    takeBallot = x.Date_take_ballot == null ? false : true,
                    statusName = x.Status_ID == 6 ? new StatusDAO().getStatus((byte)x.Status_ID) : new StatusDAO().getStatus((byte)x.Status_ID).Substring(0, new StatusDAO().getStatus((byte)x.Status_ID).IndexOf("warning")),
                    dateExport = (DateTime)x.Request_stockout_date,
                    dateCompleted = x.Date_completed,
                    note = x.Note,
                    Driver_ID = x.Driver_ID,
                    Shiper_ID = x.Shiper_ID,
                    DeliverMethod_ID = x.DeliverMethod_ID,

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
                if (!string.IsNullOrEmpty(model.Driver_ID))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.Driver_ID.Equals(model.Driver_ID)).ToList();
                }
                if (model.Shiper_ID != null)
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.Shiper_ID == model.Shiper_ID).ToList();
                }
                if (model.DeliverMethod_ID != null)
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.DeliverMethod_ID == model.DeliverMethod_ID).ToList();
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
                if (model.takeInvoice)
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.takeInvoice == true).ToList();
                }
                if (model.takeBallot)
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.takeBallot == true).ToList();
                }
                if (model.reveiceInvoice)
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.reveiceInvoice == true).ToList();
                }
                if (model.reveiceBallot)
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.reveiceBallot == true).ToList();
                }
                model.listOrderPending = model.listOrderPending.OrderByDescending(x => x.dateExport).ToList();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return View(model);
        }

        public ActionResult Exchange(OrderListModel model)
        {
            try
            {
                //setup data
                model.listMethod = new DeliveryMethodDAO().getLstDelivery().Select(x => new SelectListItem
                {
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
                List<Order_part> lstOrder = new OrderPartDAO().getAllOrderPartByStatus(9);
                model.listOrderPending = lstOrder.Select((x, index) => new OrderListPending
                {
                    indexOf = index + 1,
                    orderID = x.Order_part_ID,
                    customerName = new CustomerDAO().getCustomerById(x.Customer_ID).Customer_name,
                    invoiceNumber = x.Invoice_number == null ? "" : x.Invoice_number,
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
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult SaveDelivery(OrderListPending model)
        {
            try
            {
                var dao = new OrderPartDAO();
                dao.updateOrderPart(new Order_part
                {
                    Order_part_ID = model.orderID,
                    Driver_ID = model.Driver_ID,
                    DeliverMethod_ID = model.DeliverMethod_ID,
                    Shiper_ID = model.Shiper_ID
                }, model.takeInvoice, model.takeBallot);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DoneDelivery(OrderListPending model)
        {
            try
            {
                var dao = new OrderTotalDAO();
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                dao.delivery_checkOut(model.orderID, session.user_id, model.DeliverMethod_ID, model.Driver_ID,
                    model.Shiper_ID.Value, model.takeInvoice, model.takeBallot);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ExchangeOrder(String orderId)
        {
            try
            {
                var dao = new OrderTotalDAO();
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                dao.exchangeOrder(orderId, "", session.user_id);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveOrder(OrderListPending model)
        {
            try
            {
                var dao = new OrderTotalDAO();
                dao.kho_updateOrder(model.orderID, model.takeInvoice, model.takeBallot, model.reveiceInvoice, model.reveiceBallot);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DoneOrder(OrderListPending model)
        {
            try
            {
                var dao = new OrderTotalDAO();
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                dao.completeOrder(model.orderID, session.user_id, model.reveiceInvoice, model.reveiceBallot, model.takeInvoice, model.takeBallot);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [ChildActionOnly]
        public PartialViewResult NotificationHeader()
        {
            try
            {
                var notiDAO = new NotificationDAO();
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                List<Notification> listNoti = new List<Notification>();
                listNoti.AddRange(notiDAO.getByUserID(session.user_id));
                listNoti.AddRange(notiDAO.getByRoleID((int)session.roleSelectedID));
                listNoti = listNoti.OrderByDescending(x => x.Status).ToList();
                listNoti = listNoti.OrderByDescending(x => x.Status).ToList();
                return PartialView(listNoti);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                RedirectToAction("Index");
            }
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult NoteEdit()
        {
            var noteDAO = new NoteDAO();
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            var content = noteDAO.getNotebyAccount(session.accountID);

            return PartialView(content);
        }

        [HttpPost]
        public JsonResult NoteEdit(int accID, string content)
        {
            var noteDAO = new NoteDAO();
            noteDAO.editNotebyAccount(accID, content);
            return Json(content, JsonRequestBehavior.AllowGet);
        }


        [ChildActionOnly]
        public PartialViewResult NoteHeader()
        {
            var noteDAO = new NoteDAO();
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            var content = noteDAO.getNotebyAccount(session.accountID).Contents;
            string[] lines = new string[] { };
            if (!string.IsNullOrEmpty(content.Trim()))
            {
                lines = content.Split(Environment.NewLine.ToCharArray());
            }
            else
            {
                lines = new string[] { "Hãy điền ghi chú vào đây" };
            }
            return PartialView(lines);
        }

        public ActionResult ChangeStatusNote(string link, int notiID)
        {
            link = "/ChiTietPhieu/Index?orderId=O1";
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            new NotificationDAO().changeStatus(notiID);
            switch (session.roleSelectedID)
            {
                case 1:
                    link = "/QuanTri" + link;
                    break;
                case 2:
                    link = "/QuanLy" + link;
                    break;
                case 3:
                    link = "/PhanPhoi" + link;
                    break;
                case 4:
                    link = "/HangHoa" + link;
                    break;
                case 5:
                    link = "/KeToan" + link;
                    break;
                default:
                    break;
            }
            return Redirect(link);
        }
        public ActionResult outConfirmRole()
        {
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            session.roleSelectedID = 0;
            return RedirectToAction("ConfirmRole", "ConfirmRole", new { area = "" });
        }
    }
}