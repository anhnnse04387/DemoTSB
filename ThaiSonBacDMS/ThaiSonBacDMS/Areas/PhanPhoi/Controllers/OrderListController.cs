using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class OrderListController : PhanPhoiBaseController
    {
        // GET: PhanPhoi/OrderList
        public ActionResult Index()
        {
            try
            {
                var orderDAO = new OrderTotalDAO();
                var customerDao = new CustomerDAO();
                var statusDAO = new StatusDAO();
                var model = new ListModel();
                var lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID == 1).OrderBy(x => x.Order_ID).ToList();
                var items = new List<ListItemModel>();
                foreach (Order_total o in lstOrder)
                {
                    var item = new ListItemModel
                    {
                        orderId = o.Order_ID,
                        customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                        date = o.Date_created,
                        delivery = o.Order_part.Count,
                        note = o.Note,
                        total = o.Total_price,
                        status = statusDAO.getStatus(o.Status_ID),
                        spanClass = "label-info"
                    };
                    items.Add(item);
                }
                model.statusId = 1;
                model.items = items;
                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ChooseCustomer(String name)
        {
            try
            {
                var dao = new CustomerDAO();
                var customer = dao.getCustomer().Where(x => x.Customer_name.Trim().ToLower().Contains(name.Trim().ToLower())).Select(x => x.Customer_name).ToList();
                return Json(customer, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction(this.ControllerContext.RouteData.Values["action"].ToString());
            }
        }

        public ActionResult Complete()
        {
            try
            {
                var orderDAO = new OrderTotalDAO();
                var customerDao = new CustomerDAO();
                var statusDAO = new StatusDAO();
                var model = new ListModel();
                var lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID == 7).OrderBy(x => x.Order_ID).ToList();
                var items = new List<ListItemModel>();
                foreach (Order_total o in lstOrder)
                {
                    var item = new ListItemModel
                    {
                        orderId = o.Order_ID,
                        customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                        date = Convert.ToDateTime(o.Date_created.ToString("MM/dd/yyyy")),
                        delivery = o.Order_part.Count,
                        note = o.Note,
                        total = o.Total_price,
                        status = statusDAO.getStatus(o.Status_ID),
                        spanClass = "label-success"
                    };
                    items.Add(item);
                }
                model.statusId = 7;
                model.items = items;
                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Cancel()
        {
            try
            {
                var orderDAO = new OrderTotalDAO();
                var customerDao = new CustomerDAO();
                var statusDAO = new StatusDAO();
                var model = new ListModel();
                var lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID == 8).OrderBy(x => x.Order_ID).ToList();
                var items = new List<ListItemModel>();
                foreach (Order_total o in lstOrder)
                {
                    var item = new ListItemModel
                    {
                        orderId = o.Order_ID,
                        customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                        date = Convert.ToDateTime(o.Date_created.ToString("MM/dd/yyyy")),
                        delivery = o.Order_part.Count,
                        note = o.Note,
                        total = o.Total_price,
                        status = statusDAO.getStatus(o.Status_ID),
                        spanClass = "label-danger"
                    };
                    items.Add(item);
                }
                model.statusId = 8;
                model.items = items;
                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Store()
        {
            try
            {
                var orderDAO = new OrderPartDAO();
                var customerDao = new CustomerDAO();
                var statusDAO = new StatusDAO();
                var model = new ListModel();
                var lstOrder = orderDAO.getAllOrderPart().Where(x => x.Status_ID == 2).ToList();
                var items = new List<ListItemModel>();
                foreach (Order_part o in lstOrder)
                {
                    var item = new ListItemModel
                    {
                        orderId = o.Order_part_ID,
                        customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                        date = o.Request_stockout_date,
                        delivery = 1,
                        note = o.Note,
                        total = o.Total_price,
                        status = statusDAO.getStatus(o.Status_ID),
                        spanClass = "label-warning"
                    };
                    items.Add(item);
                }
                model.statusId = 2;
                model.items = items.OrderByDescending(x => x.date).ToList();
                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Processing()
        {
            try
            {
                var orderDAO = new OrderTotalDAO();
                var customerDao = new CustomerDAO();
                var statusDAO = new StatusDAO();
                var model = new ListModel();
                var lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID != 1 && x.Status_ID != 2 && x.Status_ID != 7 && x.Status_ID != 8).OrderBy(x => x.Order_ID).ToList();
                var items = new List<ListItemModel>();
                foreach (Order_total o in lstOrder)
                {
                    var spanClass = "";
                    var status = statusDAO.getStatus(o.Status_ID);
                    if (o.Status_ID == 10 || o.Status_ID == 11)
                    {
                        status = status.Substring(0, status.IndexOf("warning") - 1);
                        spanClass = "label-warning";
                    }
                    if (o.Status_ID == 3 || o.Status_ID == 4 || o.Status_ID == 5 || o.Status_ID == 6)
                    {
                        spanClass = "label-primary";
                    }
                    if (o.Status_ID == 9)
                    {
                        spanClass = "label-danger";
                    }
                    var item = new ListItemModel
                    {
                        orderId = o.Order_ID,
                        customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                        date = Convert.ToDateTime(o.Date_created.ToString("MM/dd/yyyy")),
                        delivery = o.Order_part.Count,
                        note = o.Note,
                        total = o.Total_price,
                        status = status,
                        spanClass = spanClass
                    };
                    items.Add(item);
                }
                model.items = items;
                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SearchStore(ListModel model)
        {
            try
            {
                var orderDAO = new OrderPartDAO();
                var customerDao = new CustomerDAO();
                var statusDAO = new StatusDAO();
                var lstOrder = orderDAO.getAllOrderPart().Where(x => x.Status_ID == 2).ToList();
                if (!String.IsNullOrEmpty(model.orderId))
                {
                    lstOrder = lstOrder.Where(x => x.Order_ID.ToLower().Contains(model.orderId.ToLower())).ToList();
                }
                if (!String.IsNullOrEmpty(model.fromDate.ToString()))
                {
                    lstOrder = lstOrder.Where(x => x.Request_stockout_date > model.fromDate).ToList();
                }
                if (!String.IsNullOrEmpty(model.toDate.ToString()))
                {
                    lstOrder = lstOrder.Where(x => x.Request_stockout_date < model.toDate).ToList();
                }
                if (model.fromTotal > 0)
                {
                    lstOrder = lstOrder.Where(x => x.Total_price > model.fromTotal).ToList();
                }
                if (model.toTotal > 0)
                {
                    lstOrder = lstOrder.Where(x => x.Total_price < model.toTotal).ToList();
                }
                var items = new List<ListItemModel>();
                foreach (Order_part o in lstOrder)
                {
                    var item = new ListItemModel
                    {
                        orderId = o.Order_part_ID,
                        customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                        date = o.Request_stockout_date,
                        delivery = 1,
                        note = o.Note,
                        total = o.Total_price,
                        status = statusDAO.getStatus(o.Status_ID),
                        spanClass = "label-warning"
                    };
                    items.Add(item);
                }
                if (!String.IsNullOrEmpty(model.customer))
                {
                    items = items.Where(x => x.customer.ToLower().Contains(model.customer.ToLower())).ToList();
                }
                if (model.orderType > 0)
                {
                    if (model.orderType == 1)
                    {
                        items = items.Where(x => x.delivery == 1).ToList();
                    }
                    else
                    {
                        items = items.Where(x => x.delivery > 1).ToList();
                    }
                }
                model.items = items.OrderByDescending(x => x.date).ToList();
                return PartialView("_searchPartialNormal", model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction(this.ControllerContext.RouteData.Values["action"].ToString());
            }
        }

        public ActionResult Search(ListModel model)
        {
            try
            {
                var orderDAO = new OrderTotalDAO();
                var lstOrder = new List<Order_total>();
                if (model.statusId == 12)
                {
                    lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID != 1 && x.Status_ID != 2 && x.Status_ID != 7 && x.Status_ID != 8).ToList();
                }
                else
                {
                    lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID == model.statusId).ToList();
                }
                if (!String.IsNullOrEmpty(model.orderId))
                {
                    lstOrder = lstOrder.Where(x => x.Order_ID.ToLower().Contains(model.orderId.ToLower())).ToList();
                }
                if (!String.IsNullOrEmpty(model.fromDate.ToString()))
                {
                    lstOrder = lstOrder.Where(x => x.Date_created > model.fromDate).ToList();
                }
                if (!String.IsNullOrEmpty(model.toDate.ToString()))
                {
                    lstOrder = lstOrder.Where(x => x.Date_created < model.toDate).ToList();
                }
                if (model.fromTotal > 0)
                {
                    lstOrder = lstOrder.Where(x => x.Total_price > model.fromTotal).ToList();
                }
                if (model.toTotal > 0)
                {
                    lstOrder = lstOrder.Where(x => x.Total_price < model.toTotal).ToList();
                }
                var items = new List<ListItemModel>();
                var customerDao = new CustomerDAO();
                var statusDAO = new StatusDAO();
                foreach (Order_total o in lstOrder)
                {
                    var spanClass = "";
                    var status = statusDAO.getStatus(o.Status_ID);
                    if (o.Status_ID == 1)
                    {
                        spanClass = "label-info";
                    }
                    else if (o.Status_ID == 3 || o.Status_ID == 4 || o.Status_ID == 5 || o.Status_ID == 6)
                    {
                        spanClass = "label-primary";
                    }
                    else if (o.Status_ID == 7)
                    {
                        spanClass = "label-success";
                    }
                    else if (o.Status_ID == 8 || o.Status_ID == 9)
                    {
                        spanClass = "label-danger";
                    }
                    else
                    {
                        status = status.Substring(0, status.IndexOf("warning") - 1);
                        spanClass = "label-warning";
                    }
                    var item = new ListItemModel
                    {
                        orderId = o.Order_ID,
                        customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                        date = Convert.ToDateTime(o.Date_created.ToString("MM/dd/yyyy")),
                        delivery = o.Order_part.Count,
                        note = o.Note,
                        total = o.Total_price,
                        status = status,
                        spanClass = spanClass
                    };
                    items.Add(item);
                }
                if (!String.IsNullOrEmpty(model.customer))
                {
                    items = items.Where(x => x.customer.ToLower().Contains(model.customer.ToLower())).ToList();
                }
                if (model.orderType > 0)
                {
                    if (model.orderType == 1)
                    {
                        items = items.Where(x => x.delivery == 1).ToList();
                    }
                    else
                    {
                        items = items.Where(x => x.delivery > 1).ToList();
                    }
                }
                model.items = items.OrderBy(x => x.orderId).ToList();
                if (model.statusId == 1 || model.statusId == 8 || model.statusId == 3 || model.statusId == 9)
                {
                    return PartialView("_searchPartialNormal", model);
                }
                else
                {
                    return PartialView("_searchPartialSpecial", model);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction(this.ControllerContext.RouteData.Values["action"].ToString());
            }
        }

        public JsonResult DoneOrder(String orderId)
        {
            try
            {
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var dao = new OrderTotalDAO();
                dao.checkOutOnTime(orderId, session.user_id);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
        }

    }

}